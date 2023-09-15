using API.Dtos;
using AutoMapper;
using Dominio.Entities;
namespace Api.Profiles;
public class MappingIngredienteProfile: Profile{
   public MappingIngredienteProfile(){
       CreateMap<IngredienteDto,Ingrediente>()
           .ReverseMap();
    }
}