using Dominio.Entities;
using Dominio.Interfaces;

namespace Dominio.Interfaces;
public interface IUsuarioRepository: IGenericRepository<Usuario>{
    Task<Usuario> GetByUsernameAsync(string nombre);
}