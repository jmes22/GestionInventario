using Entity;
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

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _unitOfWork.Productos.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Producto>> GetAllProductosAsync()
        {
            return await _unitOfWork.Productos.GetAllAsync();
        }

        public async Task<IEnumerable<Producto>> GetProductosByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _unitOfWork.Productos.GetProductosByPriceRangeAsync(minPrice, maxPrice);
        }

        public async Task CreateProductoAsync(Producto producto)
        {
            await _unitOfWork.Productos.AddAsync(producto);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            await _unitOfWork.Productos.UpdateAsync(producto);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProductoAsync(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto != null)
            {
                await _unitOfWork.Productos.DeleteAsync(producto);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}