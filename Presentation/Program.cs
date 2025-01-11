using Microsoft.EntityFrameworkCore;
using Presentation.Extension;
using Repositories.AutoMapper;
using Repositories.Contracts;
using Repositories.EFCore;
using Serilog;
using Services.Abstract;
using Services.Concrete;

var builder = WebApplication.CreateBuilder(args);
//Log.Logger = new LoggerConfiguration()
//    .Enrich.FromLogContext().WriteTo.Console()
//    .WriteTo.File("logs/log-.txt",rollingInterval:RollingInterval.Day)
//    .CreateLogger();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurationSQLContext(builder.Configuration);
builder.Services.ConfiguerRepositoryManager();
builder.Services.ConfiguerServiceManager();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
var app = builder.Build();
//**** app.UseSerilogRequestLogging(); http isteklerini loglar *****

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("v1/swagger.json", "Presentation V1");
        c.RoutePrefix = "swagger";
    });


app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
