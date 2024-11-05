using Entity;
using Entity.Common;
using Entity.Common.Exceptions;
using Microsoft.Extensions.Logging;
using Repository;

namespace Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUnitOfWork unitOfWork, ILogger<UsuarioService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Usuario> GetUsuarioAsync(Usuario usuario)
        {
            var _usuario = await _unitOfWork.Usuarios.GetUsuarioAsync(usuario);

            return _usuario == null
                ? throw new NotFoundException($"La contraseña o el mail son incorrectos")
                : _usuario;
        }
    }
}