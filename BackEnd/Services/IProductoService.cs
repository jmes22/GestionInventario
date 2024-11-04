using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductoService
    {
        Task<Producto> GetProductoByIdAsync(int id);
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<IEnumerable<Producto>> GetProductosByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task CreateProductoAsync(Producto producto);
        Task UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
    }
}
