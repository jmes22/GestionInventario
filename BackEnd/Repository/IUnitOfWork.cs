﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductoRepository Productos { get; }
        IUsuarioRepository Usuarios { get; }
        Task<int> CompleteAsync();
    }
}
