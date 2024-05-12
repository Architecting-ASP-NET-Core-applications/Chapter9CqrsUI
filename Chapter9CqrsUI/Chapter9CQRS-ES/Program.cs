using Chapter9CQRS_API.Events.EventStore;
using Chapter9CQRS_API.Features.Commands;
using Chapter9CQRS_API.Features.Queries;
using Chapter9CQRS_API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<IEventStore, InMemoryEventStore>();
builder.Services.AddMediatR(cfg => cfg
    .RegisterServicesFromAssembly(
        typeof(CreateProductHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg
    .RegisterServicesFromAssembly(
        typeof(GetProductByIdHandler).Assembly));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
// Get endpoint
app.MapGet("/GetProduct/{id}", async (int id, IMediator mediator) =>
{
    var product = await mediator.Send(new GetProductByIdQuery { Id = id });
    return Results.Ok(product);
})
.WithName("GetProduct")
.WithOpenApi();

// Post endpoint for product creation
app.MapPost("/PostProduct", async ([FromBody] Product product, IMediator mediator) =>
{
    CreateProductCommand command = new CreateProductCommand()
    {
        Name = product.Name,
        Price = product.Price,
    };
    var productId = await mediator.Send(command);
    return Results.Ok(productId);
})
.WithName("PostProduct")
.WithOpenApi();

// GetProductList
app.MapGet("/GetProductList", async (IMediator mediator) =>
{
    var products = await mediator.Send(new GetAllProductsQuery());
    return Results.Ok(products);
})
.WithName("GetProductList")
.WithOpenApi();



app.Run();
