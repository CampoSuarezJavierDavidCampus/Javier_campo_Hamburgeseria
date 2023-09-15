using API.Dtos;
using AutoMapper;
using Dominio.Entities;
namespace Api.Profiles;
public class MappingCategoriaProfile: Profile{
   public MappingCategoriaProfile(){
       CreateMap<CategoriaDto,Categoria>()
           .ReverseMap();
    }
}