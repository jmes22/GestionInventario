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
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Usuario> GetUsuarioAsync(Usuario usuario)
        {
            return await _dbSet.Where(p => p.Email.Equals(usuario.Email) && p.Password.Equals(usuario.Password)).FirstOrDefaultAsync();
        }
    }
}
