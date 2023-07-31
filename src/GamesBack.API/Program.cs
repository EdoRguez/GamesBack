using GamesBack.API;
using GamesBack.Application;
using GamesBack.Infrastructure;
using Carter;
using GamesBack.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation(builder.Configuration)
        .AddApplication()
        .AddInfrastructure();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.MapCarter();
    app.Run();
}
