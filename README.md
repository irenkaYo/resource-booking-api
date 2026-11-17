Resource Booking API

API for booking resources (rooms, equipment, etc.). Written in ASP.NET Core 8 with PostgreSQL.

Stack
.NET 8 / ASP.NET Core
Entity Framework Core 8
PostgreSQL
FluentValidation
Swagger
Project Structure

The project is divided into 4 layers:

API — controllers and entry point
Service — business logic and DTOs
Infrastructure — database interactions (EF Core, repositories)
Domain — models and enumerations
Startup
What you need to install
.NET 8 SDK
PostgreSQL 13+
EF Core CLI: dotnet tool install --global dotnet-ef
Setup

The database connection string is stored in User Secrets:

bash
dotnet user-secrets init --project API
Migrations
bash
dotnet ef database update --project ResourceBooking/Infrastructure --startup-project ResourceBooking/API
Startup
bash
cd ResourceBooking/API
dotnet run
Basic Endpoints

Instead of authentication tokens, the X-User-Id header is used—the user's Guid from the database is passed to it.

Users

POST /api/auth/register — registration
GET /api/auth/{userId} — get a user

Resources

GET /api/resources — list all resources
GET /api/resources/{id} — a single resource
POST /api/resources — create (Admin only)
PATCH /api/resources/{id} — update (Admin only)
DELETE /api/resources/{id} — delete (Admin only)
POST /api/resources/filter — filter by location, category, capacity
GET /api/resources/{id}/availability?startDate=...&endDate=... — check availability

Bookings

POST /api/bookings — create a booking
GET /api/bookings/{id} — get a booking
GET /api/bookings/my/{userId} — all user reservations
PUT /api/bookings/{id}/cancel — cancel a reservation

Other (Admin only): locations, categories, features — CRUD via /api/locations, /api/categories, /api/features.

Booking Logic

When creating a reservation, the following is checked:

Dates in the future
endTime is later than startTime
Resource is active
No overlap with other reservations

Statuses: Active, Canceled, Completed. A reservation is automatically upgraded to Completed by a background service once per minute.

Concurrency

When updating a resource, optimistic locking is used via xmin (a PostgreSQL system column). If someone else changes the record before it does, an error will be returned.
