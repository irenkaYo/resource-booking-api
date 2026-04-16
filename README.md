# Resource Booking API

A RESTful API for managing and booking shared resources such as rooms, equipment, or any other reservable assets. Built with ASP.NET Core 8, Entity Framework Core, and PostgreSQL.

## Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Configuration](#configuration)
  - [Running Migrations](#running-migrations)
  - [Running the Application](#running-the-application)
- [API Reference](#api-reference)
  - [Auth](#auth)
  - [Resources](#resources)
  - [Bookings](#bookings)
  - [Locations](#locations)
  - [Categories](#categories)
  - [Features](#features)
- [Background Services](#background-services)
- [Validation](#validation)
- [Concurrency Handling](#concurrency-handling)

---

## Overview

The API allows users to browse available resources, check their availability, and create time-slot bookings. Administrators can manage the resource catalog — create, update, deactivate, and assign features to resources. A background service automatically marks expired bookings as completed.

## Tech Stack

- **Runtime:** .NET 8
- **Framework:** ASP.NET Core Web API
- **ORM:** Entity Framework Core 8
- **Database:** PostgreSQL (via Npgsql)
- **Validation:** FluentValidation
- **API Docs:** Swagger / OpenAPI (Swashbuckle)

## Architecture

The solution follows a layered architecture split into four projects:

| Project | Responsibility |
|---|---|
| `API` | Controllers, DI composition root, application entry point |
| `Service` | Business logic, DTOs, validators, service interfaces |
| `Infrastructure` | EF Core DbContext, repository implementations, migrations |
| `Domain` | Domain models and enums — no external dependencies |

Dependencies flow inward: `API` → `Service` → `Domain`; `Infrastructure` depends on `Domain` and implements interfaces defined in `Service`.

## Project Structure

```
ResourceBooking/
├── API/
│   ├── Controllers/
│   │   ├── BookingController.cs
│   │   ├── CategoryController.cs
│   │   ├── FeatureController.cs
│   │   ├── LocationController.cs
│   │   ├── ResourceController.cs
│   │   └── UserController.cs
│   ├── Program.cs
│   └── appsettings.json
├── Domain/
│   └── Models/
│       ├── Booking/
│       │   ├── Booking.cs
│       │   └── BookingStatus.cs
│       ├── Resource/
│       │   ├── Resource.cs
│       │   ├── Category.cs
│       │   ├── Feature.cs
│       │   ├── Location.cs
│       │   └── ResourceFeature.cs
│       └── User/
│           ├── User.cs
│           └── UserRole.cs
├── Infrastructure/
│   ├── Migrations/
│   ├── Persistance/
│   │   ├── Configurations/
│   │   ├── ResourceBookingContext.cs
│   │   └── UnitOfWork.cs
│   └── Repositories/
└── Service/
    ├── DTO/
    ├── Interfaces/
    ├── Services/
    │   ├── BookingService.cs
    │   ├── BookingStatusBackgroundService.cs
    │   └── ...
    └── Validators/
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (version 13 or later)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) — install with `dotnet tool install --global dotnet-ef`

### Configuration

The application uses **User Secrets** for storing sensitive configuration such as the database connection string.

Initialize user secrets (if not already initialized):

```bash
dotnet user-secrets init --project API

### Running Migrations

From the repository root, apply the database migrations:

```bash
dotnet ef database update --project ResourceBooking/Infrastructure --startup-project ResourceBooking/API
```

### Running the Application

```bash
cd ResourceBooking/API
dotnet run
```

## API Reference

User identity is passed via the `X-User-Id` request header on endpoints that require it. There is no token-based authentication — the header value must be a valid user `Guid` that exists in the database.

### Auth

| Method | Path | Description |
|---|---|---|
| `POST` | `/api/auth/register` | Create a new user |
| `GET` | `/api/auth/{userId}` | Get a user by ID |

**POST /api/auth/register**

```json
{
  "name": "string",
  "email": "string",
  "password": "string",
  "role": "Admin | User"
}
```

---

### Resources

Resource management operations require the caller to be an `Admin` (identified by `X-User-Id`).

| Method | Path | Description |
|---|---|---|
| `GET` | `/api/resources` | List all resources |
| `GET` | `/api/resources/{resourceId}` | Get a resource by ID |
| `POST` | `/api/resources` | Create a resource (Admin) |
| `PATCH` | `/api/resources/{resourceId}` | Update a resource (Admin) |
| `DELETE` | `/api/resources/{resourceId}` | Delete a resource (Admin) |
| `POST` | `/api/resources/filter` | Filter resources by criteria |
| `GET` | `/api/resources/{resourceId}/availability` | Check availability for a time range |
| `POST` | `/api/resources/{resourceId}/feature` | Assign a feature to a resource |
| `POST` | `/api/resources/{resourceId}/active` | Deactivate a resource (Admin) |

**POST /api/resources** — requires `X-User-Id` header (Admin)

```json
{
  "name": "string",
  "description": "string",
  "locationId": "guid",
  "categoryId": "guid",
  "capacity": 10
}
```

**POST /api/resources/filter**

```json
{
  "locationId": "guid | null",
  "categoryId": "guid | null",
  "capacity": 0,
  "featureId": "guid | null"
}
```

**GET /api/resources/{resourceId}/availability**

Query parameters: `startDate` (ISO 8601), `endDate` (ISO 8601).

Returns a boolean indicating whether the resource is free for the requested period.

### Bookings

| Method | Path | Description |
|---|---|---|
| `POST` | `/api/bookings` | Create a booking |
| `GET` | `/api/bookings/{bookingId}` | Get a booking by ID |
| `GET` | `/api/bookings/my/{userId}` | List all bookings for a user |
| `PUT` | `/api/bookings/{bookingId}/cancel` | Cancel a booking |

**POST /api/bookings**

```json
{
  "resourceId": "guid",
  "userId": "guid",
  "startTime": "2026-05-01T09:00:00+00:00",
  "endTime": "2026-05-01T11:00:00+00:00"
}
```

Booking creation enforces the following rules:
- `startTime` and `endTime` must be in the future.
- `endTime` must be greater than `startTime`.
- The resource must be active.
- The requested time slot must not overlap with any existing active or completed booking for the same resource.

**PUT /api/bookings/{bookingId}/cancel** — requires `X-User-Id` header

A user can only cancel their own bookings. An Admin can cancel any booking.

#### Booking statuses

| Status | Description |
|---|---|
| `Active` | Booking is current or upcoming |
| `Canceled` | Booking was manually canceled |
| `Completed` | End time has passed; set automatically by the background service |

---

### Locations

| Method | Path | Description |
|---|---|---|
| `POST` | `/api/locations` | Create a location (Admin) |
| `GET` | `/api/locations/{locationId}` | Get a location by ID |

---

### Categories

| Method | Path | Description |
|---|---|---|
| `POST` | `/api/categories` | Create a category (Admin) |
| `GET` | `/api/categories/{categoryId}` | Get a category by ID |

---

### Features

| Method | Path | Description |
|---|---|---|
| `POST` | `/api/features` | Create a feature (Admin) |
| `GET` | `/api/features/{featureId}` | Get a feature by ID |

---

## Background Services

`BookingStatusBackgroundService` runs as a hosted service and polls every minute. On each tick it queries for bookings whose `EndTime` has passed and whose status is still `Active`, and bulk-updates them to `Completed` using EF Core's `ExecuteUpdateAsync`.

## Validation

Input validation is handled by FluentValidation with automatic integration via `AddFluentValidationAutoValidation()`. The following validators are registered:

- `CreateUserValidator` — name, email, and password rules
- `CreateBookingValidator` — `StartTime` and `EndTime` are required; `EndTime` must be greater than `StartTime`
- `CreateResourceValidator` — name and capacity rules
- `NameDtoValidator<T>` — generic validator reused for `CreateLocationDto`, `CreateCategoryDto`, and `CreateFeatureDto`

Validation errors are returned automatically as `400 Bad Request` with a structured error body before the request reaches the controller.

## Concurrency Handling

Resource updates use PostgreSQL's system column `xmin` as an optimistic concurrency token. The client must include the current `xmin` value when sending a `PATCH` request. If the row was modified by another transaction since the value was read, EF Core raises a `DbUpdateConcurrencyException` and the update is rejected.
