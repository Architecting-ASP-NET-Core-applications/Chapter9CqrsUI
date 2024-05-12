using System.Net.Mime;
using System.Text.Json;
using System.Text;
using Chapter9CQRS_API.Models;
using System.Net.Http;

namespace Chapter9CqrsUI.HttpServices;

public class ProductService : AbstractService, IProductService
{
    public ProductService(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory) { }


    public async Task<Product> Get(int id, CancellationToken cancellationToken = default)
        => await PerformGetAsync<Product>($"https://localhost:7146/GetProduct/{id}") ?? new();

    public async Task<List<Product>> GetProductList()
        => await PerformGetAsync<List<Product>>($"https://localhost:7146/GetProductList") ?? new();

    public async Task<HttpResponseMessage> Post(Product product)
    {
        var content = JsonSerializer.Serialize(product);
        var requestContent = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);
        var requestAddr = $"https://localhost:7146/PostProduct";
        return await PerformPostAsync(requestAddr, content);
    }
}
