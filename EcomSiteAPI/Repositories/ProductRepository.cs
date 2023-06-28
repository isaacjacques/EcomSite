using Dapper;
using EcomAPI.Dto;
using EcomAPI.Interfaces;
using EcomAPI.Models;
using System.Data;

namespace EcomAPI.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var query = "SELECT * FROM dbo.Products";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Product>(query);
                return companies.ToList();
            }
        }
        public async Task<Product> GetProduct(long productID)
        {
            var query = "SELECT * FROM dbo.Products WHERE ProductID = @ProductID";

            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QuerySingleOrDefaultAsync<Product>(query, new { productID });

                return product;
            }
        }
        public async Task<Product> CreateProduct(ProductForCreationDto product)
        {
            var query = "INSERT INTO dbo.Products (SKU, UPC, ColorID, SizeID, BrandID, PackSize, Description, CreationTime) OUTPUT (inserted.ProductID) VALUES (@SKU, @UPC, @ColorID, @SizeID, @BrandID, @PackSize, @Description, @CreationTime)";

            var creationTime = DateTime.Now;
            var parameters = new DynamicParameters();
            parameters.Add("SKU", product.SKU, DbType.String);
            parameters.Add("UPC", product.UPC, DbType.String);
            parameters.Add("ColorID", product.ColorID, DbType.Int16);
            parameters.Add("SizeID", product.SizeID, DbType.Int16);
            parameters.Add("BrandID", product.BrandID, DbType.Int16);
            parameters.Add("PackSize", product.PackSize, DbType.Int16);
            parameters.Add("Description", product.Description, DbType.String);
            parameters.Add("CreationTime", creationTime, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var productID = await connection.QuerySingleAsync<long>(query, parameters);

                var createdProduct = new Product
                {
                    ProductID = productID,
                    SKU = product.SKU,
                    UPC = product.UPC,
                    ColorID = product.ColorID,
                    SizeID = product.SizeID,
                    BrandID = product.BrandID,
                    PackSize = product.PackSize,
                    Description = product.Description,
                    CreationTime = creationTime
                };
                return createdProduct;
            }
        }
        public async Task UpdateProduct(ProductForUpdateDto product)
        {
            var query = "UPDATE dbo.Products SET SKU = @SKU, UPC = @UPC, ColorID = @ColorID, SizeID = @SizeID, BrandID = @BrandID, PackSize = @PackSize, Description = @Description WHERE ProductID = @ProductID";

            var parameters = new DynamicParameters();
            parameters.Add("ProductID", product.ProductID, DbType.Int64);
            parameters.Add("SKU", product.SKU, DbType.String);
            parameters.Add("UPC", product.UPC, DbType.String);
            parameters.Add("ColorID", product.ColorID, DbType.Int16);
            parameters.Add("SizeID", product.SizeID, DbType.Int16);
            parameters.Add("BrandID", product.BrandID, DbType.Int16);
            parameters.Add("PackSize", product.PackSize, DbType.Int16);
            parameters.Add("Description", product.Description, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteProduct(long productID)
        {
            var query = "DELETE FROM dbo.Products WHERE ProductID = @ProductID";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { productID });
            }
        }
    }
}
