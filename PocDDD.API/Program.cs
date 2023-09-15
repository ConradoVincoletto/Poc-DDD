using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PocDDD.API;
using PocDDD.Application.Interfaces;
using PocDDD.Application.Mappings;
using PocDDD.Application.Services;
using PocDDD.Domain.Interfaces;
using PocDDD.Infra.Data.Context;
using PocDDD.Infra.Data.Repositories;
using PocDDD.Infra.IoC;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInsfraStructure(builder.Configuration);
builder.Services.AddDataBase(builder.Configuration);
builder.Services.AddUnitOfWork();

builder.Services.AddAutoMapper(typeof(UserMapping));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };

});
var app = builder.Build();

using (IServiceScope _serviceScope = app.Services.CreateScope())
{
    AppDbContext _appDbContext = _serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    await _appDbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
