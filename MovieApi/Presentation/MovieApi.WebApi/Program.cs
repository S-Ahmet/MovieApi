using Microsoft.OpenApi.Models;
using MoviApi.Aplication.Features.CQRSDesingPattern.Handlers.CategoryHandlers;
using MoviApi.Aplication.Features.CQRSDesingPattern.Handlers.MovieHandlers;
using MovieApi.Persistence.Context;

using MovieApi.Application.Features.CQRSDesignPattern.Handlers.MovieHandlers;
using MovieApi.Application.Features.CQRSDesignPattern.Queries.CategoryQueries;
using MovieApi.Persistence.Context;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MovieContext>();

builder.Services.AddScoped<GetCategoryQueryHandler>();
builder.Services.AddScoped<GetCategoryByIdQueryHandler>();
builder.Services.AddScoped<MovieApi.Application.Features.CQRSDesignPattern.Queries.CategoryQueries.CreateCategoryCommandHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();

builder.Services.AddScoped<GetMovieQueryHandler>();
builder.Services.AddScoped<GetMovieByIdQueryHandler>();
builder.Services.AddScoped<MovieApi.Application.Features.CQRSDesignPattern.Handlers.MovieHandlers.CreateMovieCommandHandler>();
builder.Services.AddScoped<MovieApi.Application.Features.CQRSDesignPattern.Handlers.MovieHandlers.UpdateMovieCommandHandler>();
builder.Services.AddScoped<RemoveMovieCommandHandler>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
