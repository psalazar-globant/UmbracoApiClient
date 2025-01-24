using Microsoft.OpenApi.Models;
using UmbracoBridgeApi.Configuration;
using UmbracoBridgeApi.Middlewares;
using UmbracoBridgeApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();

builder.Services.Configure<UmbracoBridgeSettings>(builder.Configuration.GetSection("UmbracoBridgeSettings"));


builder.Services.AddSingleton<IAuthService, AuthService>();

// Configurar Swagger (OpenAPI)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UmbracoBridge Api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger(); 

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UmbracoBridge Api");
        c.RoutePrefix = "umbracoBridge/swagger"; 
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
