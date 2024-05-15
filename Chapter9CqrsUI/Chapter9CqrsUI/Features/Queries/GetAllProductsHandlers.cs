using Chapter9CQRS_API.Models;
using MediatR;

namespace Chapter9CQRS_API.Features.Queries;

public class GetAllProductsHandlers : IRequestHandler<GetAllProductsQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var products = new List<Product>();

        //for simplicity we will return 10 random products
        for (int i = 1; i <= 10; i++)
        {
            products.Add(new Product
            {
                Id = i,
                Name = $"Product {i}",
                Price = 10.00m * i 
            });
        }
        return products;
    }
}
