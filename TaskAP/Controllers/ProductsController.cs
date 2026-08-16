using Microsoft.AspNetCore.Mvc;
using TaskAP.Model;
using TaskAP.Service.Interface;
/*

namespace UserNew.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET /api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();

            return Ok(products);
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST /api/products
        [HttpPost]
        public IActionResult Create(Product product)
        {
            var createdProduct = _productService.Create(product);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdProduct.Id },
                createdProduct
            );
        }

        // PUT /api/products/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var updatedProduct = _productService.Update(id, product);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }

        // DELETE /api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _productService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PATCH /api/products/{id}
        [HttpPatch("{id}")]
        public IActionResult UpdateName(int id, [FromBody] ProductNameRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new
                {
                    message = "Name is required."
                });
            }

            var updatedProduct = _productService.UpdateName(id, request.Name);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }
    }

    public class ProductNameRequest
    {
        public string? Name { get; set; }
    }
}*/