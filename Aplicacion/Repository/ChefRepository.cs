using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Application.Repositories;
public sealed class ChefRepository : GenericRepository<Chef>, IChefRepository{
    public ChefRepository(DbAppContext context) : base(context){}
}