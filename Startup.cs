using System.Reflection;
using Orchestrator.Contracts.Interfaces;
using Orchestrator.Contracts.Models;
using Orchestrator.Handlers;
using Orchestrator.Mappers;
using Orchestrator.Services;
using Orchestrator.Stores;

namespace Orchestrator;

public class Startup
{
    public static WebApplication IntitializeApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);
        var app = builder.Build();
        Configure(app);
        return app;
    }

    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        Configure(builder.Services);
    }

    public static void Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }

    private static IServiceCollection Configure(IServiceCollection services)
    {
        services.AddSingleton<IStore, InMemoryStore>();
        services.AddHttpClient<IOrchestratorHandler, OrchestratorHandler>();
        services.AddScoped<IOrchestratorHandler, OrchestratorHandler>();
        services.AddSingleton<IProduceEventService, ProduceEventService>();
        services.AddSingleton<IEventMapper<BookingRequest, BookingRequestEvent>, EventMapper>();
        return services;
    }
}