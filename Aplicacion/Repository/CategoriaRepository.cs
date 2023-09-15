using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Application.Repositories;
public sealed class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository{
    public CategoriaRepository(DbAppContext context) : base(context){}
}