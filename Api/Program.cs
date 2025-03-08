using Api.Application;
using Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddControllers()
    .AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });

services.AddEndpointsApiExplorer();

services.AddSwaggerGen(
    c =>
    {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Employee Benefit Cost Calculation Api",
            Description = "Api to support employee benefit cost calculations"
        });
    });

const string allowLocalhost = "allow localhost";

services.AddCors(
    options =>
    {
        options.AddPolicy(
            allowLocalhost,
            policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
    });

services.AddPersistence();
services.AddApplicationServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<Api.Middleware.ExceptionHandlingMiddleware>();
app.UseCors(allowLocalhost);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

namespace Api
{
    public abstract class Program;
}