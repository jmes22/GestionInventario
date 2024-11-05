using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.ComponentModel.DataAnnotations;

namespace GestionProductosAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var result = await _productoService.GetProductoByIdAsync(id);
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var result = await _productoService.GetAllProductosAsync();
            return Ok(new
            {
                Data = result.Data,
                TotalRecords = result.TotalRecords
            });
        }

        [Authorize]
        [HttpGet("pricerange")]
        public async Task<IActionResult> GetProductosByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var result = await _productoService.GetProductosByPriceRangeAsync(minPrice, maxPrice);
            return Ok(new
            {
                Data = result.Data,
                TotalRecords = result.TotalRecords
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProducto(Producto producto)
        {
            var result = await _productoService.CreateProductoAsync(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = result.Data.ProductoId }, result.Data);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto producto)
        {
            var result = await _productoService.UpdateProductoAsync(id, producto);
            return Ok(result.Data);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await _productoService.DeleteProductoAsync(id);
            return NoContent();
        }
    }
}