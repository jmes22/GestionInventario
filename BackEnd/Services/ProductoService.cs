using Entity;
using Entity.Common;
using Repository;

namespace Services
{
    public class ProductoService : IProductoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Producto>> GetProductoByIdAsync(int id)
        {
            try
            {
                var producto = await _unitOfWork.Productos.GetByIdAsync(id);
                if (producto == null)
                    return Result<Producto>.Failure($"No se encontró el producto con ID: {id}", 404);

                return Result<Producto>.Success(producto);
            }
            catch (Exception ex)
            {
                return Result<Producto>.Failure($"Error al obtener el producto: {ex.Message}", 500);
            }
        }

        public async Task<ResultList<Producto>> GetAllProductosAsync()
        {
            try
            {
                var productos = await _unitOfWork.Productos.GetAllAsync();
                var list = productos.ToList();
                return ResultList<Producto>.Success(list, list.Count);
            }
            catch (Exception ex)
            {
                return ResultList<Producto>.Failure($"Error al obtener los productos: {ex.Message}", 500);
            }
        }

        public async Task<ResultList<Producto>> GetProductosByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            try
            {
                var productos = await _unitOfWork.Productos.GetProductosByPriceRangeAsync(minPrice, maxPrice);
                var list = productos.ToList();
                return ResultList<Producto>.Success(list, list.Count);
            }
            catch (Exception ex)
            {
                return ResultList<Producto>.Failure($"Error al obtener los productos por rango de precio: {ex.Message}", 500);
            }
        }

        public async Task<Result<Producto>> CreateProductoAsync(Producto producto)
        {
            try
            {
                await _unitOfWork.Productos.AddAsync(producto);
                await _unitOfWork.CompleteAsync();
                return Result<Producto>.Success(producto, 201);
            }
            catch (Exception ex)
            {
                return Result<Producto>.Failure($"Error al crear el producto: {ex.Message}", 500);
            }
        }

        public async Task<Result<Producto>> UpdateProductoAsync(Producto producto)
        {
            try
            {
                var existingProducto = await _unitOfWork.Productos.GetByIdAsync(producto.ProductoId);
                if (existingProducto == null)
                    return Result<Producto>.Failure($"No se encontró el producto con ID: {producto.ProductoId}", 404);

                await _unitOfWork.Productos.UpdateAsync(producto);
                await _unitOfWork.CompleteAsync();
                return Result<Producto>.Success(producto);
            }
            catch (Exception ex)
            {
                return Result<Producto>.Failure($"Error al actualizar el producto: {ex.Message}", 500);
            }
        }

        public async Task<Result<bool>> DeleteProductoAsync(int id)
        {
            try
            {
                var producto = await _unitOfWork.Productos.GetByIdAsync(id);
                if (producto == null)
                    return Result<bool>.Failure($"No se encontró el producto con ID: {id}", 404);

                await _unitOfWork.Productos.DeleteAsync(producto);
                await _unitOfWork.CompleteAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar el producto: {ex.Message}", 500);
            }
        }
    }
}