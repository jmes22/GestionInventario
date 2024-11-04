using Entity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace GestionProductosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _productoService.GetAllProductosAsync();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto(Producto producto)
        {
            await _productoService.CreateProductoAsync(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.ProductoId }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto producto)
        {
            if (id != producto.ProductoId)
                return BadRequest();

            await _productoService.UpdateProductoAsync(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            await _productoService.DeleteProductoAsync(id);
            return NoContent();
        }
    }
}