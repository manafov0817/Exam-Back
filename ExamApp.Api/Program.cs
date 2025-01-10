using ExamApp.Api;
using ExamApp.Business;  // Add this namespace


var builder = WebApplication.CreateBuilder(args); 


builder.Services.AddPresentationLayer()
                .AddBusinessLayer(builder.Configuration);
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Allow Angular app
              .AllowAnyHeader() // Allow any headers
              .AllowAnyMethod() // Allow any HTTP methods
              .AllowCredentials(); // Allow cookies/credentials if needed
    });
});

var app = builder.Build();
app.UseCors("AllowAngularApp");

app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExamAPI V1");
    c.RoutePrefix = string.Empty;
});
app.Run();
