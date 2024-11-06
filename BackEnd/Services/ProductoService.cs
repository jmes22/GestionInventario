using Entity;
using Entity.Common;
using Entity.Common.Exceptions;
using Microsoft.Extensions.Logging;
using Repository;

namespace Services
{
    public class ProductoService : IProductoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductoService> _logger;

        public ProductoService(IUnitOfWork unitOfWork, ILogger<ProductoService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Producto>> GetProductoByIdAsync(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto == null)
                throw new NotFoundException($"No se encontró el producto con ID: {id}");

            return Result<Producto>.Success(producto);
        }

        public async Task<ResultList<Producto>> GetAllProductosAsync()
        {
            var productos = await _unitOfWork.Productos.GetAllAsync();
            var list = productos.ToList();
            return ResultList<Producto>.Success(list, list.Count);
        }

        public async Task<ResultList<Producto>> GetProductosByNombre(string nombre)
        {
            var productos = await _unitOfWork.Productos.GetProductosByNombre(nombre);
            var list = productos.ToList();
            return ResultList<Producto>.Success(list, list.Count);
        }

        public async Task<Result<Producto>> CreateProductoAsync(Producto producto)
        {
            if (string.IsNullOrEmpty(producto.Nombre))
                throw new ValidationException("El nombre del producto es requerido");

            if (producto.Precio <= 0)
                throw new ValidationException("El precio del producto debe ser mayor a 0");

            await _unitOfWork.Productos.AddAsync(producto);
            await _unitOfWork.CompleteAsync();
            return Result<Producto>.Success(producto, 201);
        }

        public async Task<Result<Producto>> UpdateProductoAsync(int id, Producto producto)
        {
            if (id != producto.ProductoId)
                throw new ValidationException("El ID de la ruta no coincide con el ID del producto");

            var existingProducto = await _unitOfWork.Productos.GetByIdAsync(producto.ProductoId);
            if (existingProducto == null)
                throw new NotFoundException($"No se encontró el producto con ID: {producto.ProductoId}");

            existingProducto.Nombre = producto.Nombre;
            existingProducto.Precio = producto.Precio;
            existingProducto.Cantidad = producto.Cantidad;

            await _unitOfWork.Productos.UpdateAsync(existingProducto);
            await _unitOfWork.CompleteAsync();
            return Result<Producto>.Success(producto);
        }

        public async Task<Result<bool>> DeleteProductoAsync(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto == null)
                throw new NotFoundException($"No se encontró el producto con ID: {id}");

            await _unitOfWork.Productos.DeleteAsync(producto);
            await _unitOfWork.CompleteAsync();
            return Result<bool>.Success(true);
        }
    }
}