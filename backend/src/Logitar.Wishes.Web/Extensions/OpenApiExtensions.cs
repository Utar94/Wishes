﻿using Logitar.Wishes.Contracts.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Logitar.Wishes.Extensions;

public static class OpenApiExtensions
{
  private const string Title = "Wishes API";

  public static IServiceCollection AddOpenApi(this IServiceCollection services, Version version)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(config =>
    {
      config.AddSecurity();
      config.SwaggerDoc(name: $"v{version.Major}", new OpenApiInfo
      {
        Contact = new OpenApiContact
        {
          Email = "francispion@hotmail.com",
          Name = "Logitar Team",
          Url = new Uri("https://github.com/Utar94/Wishes", UriKind.Absolute)
        },
        Description = "Gift wishlist Web application.",
        License = new OpenApiLicense
        {
          Name = "Use under MIT",
          Url = new Uri("https://github.com/Utar94/Wishes/blob/main/LICENSE", UriKind.Absolute)
        },
        Title = Title,
        Version = $"v{version}"
      });
    });

    return services;
  }

  public static void UseOpenApi(this IApplicationBuilder builder, Version version)
  {
    builder.UseSwagger();
    builder.UseSwaggerUI(config => config.SwaggerEndpoint(
      url: $"/swagger/v{version.Major}/swagger.json",
    name: $"{Title} v{version}"
    ));
  }

  private static void AddSecurity(this SwaggerGenOptions options)
  {
    options.AddSecurityDefinition(Schemes.ApiKey, new OpenApiSecurityScheme
    {
      Description = "Enter your API key in the input below:",
      In = ParameterLocation.Header,
      Name = Headers.XApiKey,
      Scheme = Schemes.ApiKey,
      Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Name = Headers.XApiKey,
          Reference = new OpenApiReference
          {
            Id = Schemes.ApiKey,
            Type = ReferenceType.SecurityScheme
          },
          Scheme = Schemes.ApiKey,
          Type = SecuritySchemeType.ApiKey
        },
        new List<string>()
      }
    });

    options.AddSecurityDefinition(Schemes.Basic, new OpenApiSecurityScheme
    {
      Description = "Enter your credentials in the inputs below:",
      In = ParameterLocation.Header,
      Name = Headers.Authorization,
      Scheme = Schemes.Basic,
      Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Name = Headers.Authorization,
          Reference = new OpenApiReference
          {
            Id = Schemes.Basic,
            Type = ReferenceType.SecurityScheme
          },
          Scheme = Schemes.Basic,
          Type = SecuritySchemeType.Http
        },
        new List<string>()
      }
    });
  }
}
