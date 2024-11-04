using Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Producto>> GetProductosByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet.Where(p => p.Precio >= minPrice && p.Precio <= maxPrice).ToListAsync();
        }
    }
}
