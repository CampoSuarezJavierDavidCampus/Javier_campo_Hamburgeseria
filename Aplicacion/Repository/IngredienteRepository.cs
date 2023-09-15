using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Application.Repositories;
public sealed class IngredienteRepository : GenericRepository<Ingrediente>, IIngredienteRepository{
    public IngredienteRepository(DbAppContext context) : base(context){}
}