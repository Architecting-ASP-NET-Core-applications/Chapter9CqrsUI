using Chapter9CQRS_API.Models;

namespace Chapter9CqrsUI.HttpServices;
public interface IProductService
{
    Task<Product> Get(int id, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> Post(Product product);
    Task<List<Product>> GetProductList();
}