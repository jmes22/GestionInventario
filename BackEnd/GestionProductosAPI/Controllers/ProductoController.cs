using Entity;
using Entity.Common;
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
        public async Task<ActionResult<Result<Producto>>> GetProducto(int id)
        {
            var result = await _productoService.GetProductoByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<Producto>>>> GetProductos()
        {
            var result = await _productoService.GetAllProductosAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("search")]  // Corregido el path
        public async Task<ActionResult<Result<IEnumerable<Producto>>>> GetProductos([FromQuery] string? nombre = null)
        {
            var result = await _productoService.GetProductosByNombre(nombre);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Result<Producto>>> CreateProducto(Producto producto)
        {
            var result = await _productoService.CreateProductoAsync(producto);
            return Created($"api/Producto/{result.Data.ProductoId}", result);  
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Result<Producto>>> UpdateProducto(int id, Producto producto)
        {
            var result = await _productoService.UpdateProductoAsync(id, producto);
            return Ok(result); 
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteProducto(int id)
        {
            var result = await _productoService.DeleteProductoAsync(id);
            return Ok(result); 
        }
    }
}