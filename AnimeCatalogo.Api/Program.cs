using AnimeCatalogo.Application.IService;
using AnimeCatalogo.Application.Service;
using AnimeCatalogo.Application.Validators.Anime;
using AnimeCatalogo.Infrastructure.IRepository;
using AnimeCatalogo.Infrastructure.Persistence;
using AnimeCatalogo.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API - Catálogo de Animes", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
