global using Contracts;
global using LoggerService;
global using NLog.Extensions.Logging;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddTransient<ILoggerManager, LoggerManager>();


ConfigureLogging(builder.Logging);

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
    options.CacheProfiles.Add("120SecondsDuration", new CacheProfile(){Duration = 120});
});
// configure versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
});

// configuring globally caching
builder.Services.AddResponseCaching();
builder.Services.AddHttpCacheHeaders(expirationOpt =>
{
    expirationOpt.MaxAge = 65;
    expirationOpt.CacheLocation = CacheLocation.Private;
},
    (validationOpt) =>
    {
        validationOpt.MustRevalidate = true;
    }
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

void ConfigureLogging(ILoggingBuilder loggingBuilder)
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddNLog();
};
app.UseResponseCaching();

app.UseHttpCacheHeaders();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
