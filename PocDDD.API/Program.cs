using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PocDDD.Application.Interfaces;
using PocDDD.Application.Mappings;
using PocDDD.Application.Services;
using PocDDD.Domain.Interfaces;
using PocDDD.Infra.Data.Context;
using PocDDD.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRespository, UserRepository>();

builder.Services.AddDbContext<AppDbContext>
    (x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(UserMapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
