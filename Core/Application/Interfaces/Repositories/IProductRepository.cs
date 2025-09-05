using konsume_v1.Core.Domain.Entities;

namespace konsume_v1.Core.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GenerateProductAsync(Product scannedProduct);
        Task<ICollection<Product>> GetProductByProfileIdAsync(int profileId);
        Task<Product> GetProductByIdAsync(int productId);
    }
}
