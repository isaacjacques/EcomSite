using EcomAPI.Dto;
using EcomAPI.Models;

namespace EcomAPI.Interfaces
{
    public interface IProduct
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(long productID);
        public Task<Product> CreateProduct(ProductForCreationDto product);
        public Task UpdateProduct(ProductForUpdateDto product);
        public Task DeleteProduct(long productID);
    }
}
