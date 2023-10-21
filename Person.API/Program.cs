using AutoMapper;
using Microsoft.OpenApi.Models;
using Person.API;
using Person.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Persons.Api", Version = "v1" });

    // To Enable authorization using Swagger (JWT)    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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


var prjSettings = builder.Configuration.GetSection(nameof(PrjSettings)).Get<PrjSettings>();

builder.Services.AddDatabaseContext(prjSettings.ConnectionString, prjSettings.RunMode, prjSettings);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



string CORS_POLICY = "CorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CORS_POLICY);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
