using MisTrace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using MisTrace.Application.Interfaces;
using MisTrace.Infrastructure.Services.Notification;
using MisTrace.Domain.Interfaces.Repos;
using MisTrace.Infrastructure.Repos;
using MisTrace.Infrastructure.Services;
using MisTrace.Application.Settings;


namespace MisTrace.ApiService.Extensions;

public static class ServiceDefaults
{

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment enviroment)
    {
        services.ValidateEnviromentVariables();

        services.AddAuthKraken(enviroment);

        services.AddStores(configuration, enviroment);

        services.AddEndpointsApiExplorer();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MistraceAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\nExample: \"Bearer asdfaweasdadaeswaegasdfnbg\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                if (enviroment.IsProduction())
                {
                    c.AddServer(new OpenApiServer
                    {
                        Url = "/MisTrace"
                    });
                }
            });

        services.AddAppServices(configuration);

        return services;
    }

    public static IServiceCollection AddAuthKraken(this IServiceCollection services, IWebHostEnvironment env)
    {
        string publicKeyPath = Environment.GetEnvironmentVariable("AUTH_KRAKEN_PUBLIC_KEY")
            ?? throw new ArgumentNullException("PublicKey Path not found in environment variables");

        RSA rsa = RSA.Create();
        rsa.ImportFromPem(File.ReadAllText(publicKeyPath));

        var rsaKey = new RsaSecurityKey(rsa);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Stare-Y",
                    ValidAudience = "AuthKrakenClient",
                    IssuerSigningKey = new RsaSecurityKey(rsa)
                };
            }
            );

        services.AddAuthorization();

        return services;
    }

    private static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TwillioSettings>(configuration.GetSection("Twilio"));

        services.AddScoped<ITwillioService, TwilioService>();
        services.AddScoped<ITraceRepo, TraceRepo>();
        services.AddScoped<IMilestoneRepo, MilestoneRepo>();
        services.AddScoped<ITraceService, TraceService>();
        services.AddScoped<IMilestoneService, MilestoneService>();

        return services;
    }

    private static IServiceCollection AddStores(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment enviroment)
    {
        string connectionString = Environment.GetEnvironmentVariable("MISTRACE_DB")
            ?? throw new InvalidOperationException("Environment variable 'MISTRACE_DB' not set.");

        services.AddDbContext<MisTraceDbContext>(options =>
        {
            options.UseLazyLoadingProxies().UseNpgsql(connectionString);
        });

        return services;
    }

    private static IServiceCollection ValidateEnviromentVariables(this IServiceCollection services)
    {
        string[] requiredVariables =
        {
            "MISTRACE_DB",
            "AUTH_KRAKEN_PUBLIC_KEY",
            "TWILIO_ACCOUNTSID",
            "TWILIO_AUTHTOKEN",
            "TWILIO_FROMWHATSAPP"
        };

        foreach (string variable in requiredVariables)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(variable)))
            {
                throw new ArgumentNullException(variable, $"Environment variable '{variable}' not found.");
            }
        }

        string authKrakenKey = Environment.GetEnvironmentVariable("AUTH_KRAKEN_PUBLIC_KEY")
            ?? throw new ArgumentNullException("PublicKey Path not found in environment variables");

        if (!File.Exists(authKrakenKey))
            throw new FileNotFoundException($"PublicKey file not found at {authKrakenKey}");

        services.AddSingleton<RsaSecurityKey>(provider =>
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(File.ReadAllText(authKrakenKey));
            return new RsaSecurityKey(rsa);
        });

        return services;
    }
}
