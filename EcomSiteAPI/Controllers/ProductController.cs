using Microsoft.AspNetCore.Mvc;
using EcomAPI.Dto;
using EcomAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EcomAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _IProduct;

        public ProductController(IProduct iProduct)
        {
            _IProduct = iProduct;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var product = await _IProduct.GetProducts();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{productID}", Name = "ProductById")]
        public async Task<IActionResult> GetProduct(long productID)
        {
            try
            {
                var product = await _IProduct.GetProduct(productID);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductForCreationDto product)
        {
            try
            {
                var createdProduct = await _IProduct.CreateProduct(product);
                return CreatedAtRoute("ProductById", new { productID = createdProduct.ProductID }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{productID}")]
        public async Task<IActionResult> UpdateProduct(ProductForUpdateDto product)
        {
            try
            {
                var dbProduct = await _IProduct.GetProduct(product.ProductID);
                if (dbProduct == null)
                    return NotFound();

                await _IProduct.UpdateProduct(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{productID}")]
        public async Task<IActionResult> DeleteProduct(long productID)
        {
            try
            {
                var dbProduct = await _IProduct.GetProduct(productID);
                if (dbProduct == null)
                    return NotFound();

                await _IProduct.DeleteProduct(productID);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}