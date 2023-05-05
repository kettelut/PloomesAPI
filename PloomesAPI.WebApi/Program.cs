using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PloomesAPI.Domain.Entities;
using PloomesAPI.Infra.IoC;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDependencies();
builder.Services.AddHealthChecks();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = async (contexto, resultadoHealthCheck) =>
    {
        string respostaHealthCheck = RespostaHealthCheck.GerarRespostaHealthCheck(resultadoHealthCheck, "Ploomes - API");
        contexto.Response.ContentType = MediaTypeNames.Application.Json;
        await contexto.Response.WriteAsync(respostaHealthCheck);
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();
