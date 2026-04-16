using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Service.Common.Security;
using Service.DTO.Category;
using Service.DTO.Feature;
using Service.DTO.Location;
using Service.Interfaces.Persistance;
using Service.Interfaces.Repositories;
using Service.Interfaces.Services;
using Service.Services;
using Service.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new Exception("Connection string not found");

builder.Services.AddDbContext<ResourceBookingContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddHostedService<BookingStatusBackgroundService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<IResourceFeatureRepository, ResourceFeatureRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IHashService, PasswordHashService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateResourceValidator>();
builder.Services.AddScoped<IValidator<CreateLocationDto>, NameDtoValidator<CreateLocationDto>>();
builder.Services.AddScoped<IValidator<CreateCategoryDto>, NameDtoValidator<CreateCategoryDto>>();
builder.Services.AddScoped<IValidator<CreateFeatureDto>, NameDtoValidator<CreateFeatureDto>>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();