using Entity;
using Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductoService
    {
        Task<Result<Producto>> GetProductoByIdAsync(int id);
        Task<ResultList<Producto>> GetAllProductosAsync();
        Task<ResultList<Producto>> GetProductosByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<Result<Producto>> CreateProductoAsync(Producto producto);
        Task<Result<Producto>> UpdateProductoAsync(int id, Producto producto);
        Task<Result<bool>> DeleteProductoAsync(int id);
    }
}
