﻿using MapsTracking;
using MapsTracking.Models;
using Microsoft.AspNetCore.Mvc;
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
    //context.Database.EnsureCreated(); // tạo nếu chwua có
}


// routes
app.MapGet("/api/devices", async (Context db) =>
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

app.MapGet("/api/locations/", async (HttpContext context, Context db) =>
{
    try
    {
        var deviceID = context.Request.Query["deviceID"].ToString();
        var date = context.Request.Query["date"].ToString();
        var types = context.Request.Query["types"].ToList();
 
        if (DateTime.TryParse(date, out var parsedDate))
        {

            var device = await db.Devices.Include(d => d.TrackingEvents)
                .FirstOrDefaultAsync(d => d.DeviceID == deviceID);
            if (device == null)
            {
                return Results.NotFound();
            }
            // tìm các device location theo deviceId và ngày
            var results = device.TrackingEvents.Where(dl =>
            dl.RecordDate.Date == parsedDate.Date && types.Contains(dl.Type.ToString())).OrderBy(dl => dl.RecordDate).ToList();

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
