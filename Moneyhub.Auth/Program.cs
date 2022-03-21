using Appmilla.Moneyhub.Refit.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Moneyhub.ApiClient.Config;
using Refit;
using System;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        //options.Authority = "https://localhost:5001";
        options.Authority = "https://identityserverhost20220225150440.azurewebsites.net";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddRefitClient<IAccessToken>().ConfigureHttpClient(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://identity.moneyhub.co.uk");

    var configuration = new MoneyhubConfiguration
    {
        ClientId = builder.Configuration["ClientId"],
        ClientSecret = builder.Configuration["ClientSecret"]
    };

    var authToken = configuration.GetAuthorization();

    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
}); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
