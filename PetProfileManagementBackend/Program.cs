using Microsoft.EntityFrameworkCore;
using PetProfileManagementBackend; // Add your project namespace
using PetProfileManagementBackend.Data;
using PetProfileManagementBackend.Services; // Add the namespace for your services

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Set up the DbContext to use SQL Server (ensure to add your connection string in appsettings.json)
builder.Services.AddDbContext<PetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the PetService (Service Layer) with Dependency Injection
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
// Add controllers as services
builder.Services.AddControllers();

// Configure Swagger for API documentation (optional, but recommended)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable Swagger if you want to see API documentation in the browser
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use routing to map incoming HTTP requests
app.UseRouting();

// Map API controllers to routes
app.MapControllers();

// Run the application
app.Run();
