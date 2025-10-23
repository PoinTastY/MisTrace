using MisTrace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;


namespace MisTrace.ApiService.Extensions;

public static class ServiceDefaults
{

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment enviroment)
    {
        services.ValidateEnviromentVariables();

        services.AddAuthKraken(enviroment);

        services.AddStores(configuration, enviroment);

        services.AddEndpointsApiExplorer();

        services.AddControllers();

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

            });

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
                options.IncludeErrorDetails = true;
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
                // 👇 log all token validation errors
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var rawHeader = context.Request.Headers["Authorization"].ToString();
                        Console.WriteLine("Raw Authorization header: " + rawHeader);
                        Console.WriteLine("Token validation failed: " + context.Exception);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine($"OnChallenge error: {context.Error}, description: {context.ErrorDescription}");
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        Console.WriteLine("Token was valid but access was forbidden.");
                        return Task.CompletedTask;
                    }
                };
            }
            );

        services.AddAuthorization();

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
            "AUTH_KRAKEN_ENCRYPTION_KEY"
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
