using MapsTracking;
using MapsTracking.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ddocj connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// add context
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));


// add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowFrontend");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    context.Database.EnsureCreated(); // tạo nếu chwua có
}


// routes
app.MapGet("/maps-tracking/api/devices", async (Context db) =>
{
    try
    {
        var devices = await db.Devices.ToListAsync();
        return Results.Ok(devices);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/maps-tracking/api/device-location/{id:int}/{date}", async (int id, String date, Context db) =>
{
    try
    {
        if (DateTime.TryParse(date, out var parsedDate))
        {
            var device = await db.Devices.Include(d => d.Locations)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (device == null)
            {
                return Results.NotFound();
            }
            // tìm các device location theo deviceId và ngày
            var results = device.Locations.Where(dl =>
            dl.Timestamp.Date == parsedDate.Date).Select(dl => new
            {
                dl.Id,
                dl.Latitude,
                dl.Longitude,
                dl.Timestamp
            }).OrderBy(dl => dl.Timestamp).ToList();

            return Results.Ok(results);
        }
        else
            return Results.BadRequest("Invalid date format.");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("maps-tracking/api/device-location", async (DeviceLocation request, Context db) =>
{
    try
    {
        db.DeviceLocations.Add(request);
        await db.SaveChangesAsync();
        return Results.Ok(request);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();
