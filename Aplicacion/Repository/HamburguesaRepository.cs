using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Application.Repositories;
public sealed class HamburguesaRepository : GenericRepository<Hamburguesa>, IHamburguesaRepository{
    public HamburguesaRepository(DbAppContext context) : base(context){}
}