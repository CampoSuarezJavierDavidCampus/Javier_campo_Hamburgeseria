using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Application.Repositories;
public sealed class RolRepository : GenericRepository<Rol>, IRolRepository{
    public RolRepository(DbAppContext context) : base(context){}
}