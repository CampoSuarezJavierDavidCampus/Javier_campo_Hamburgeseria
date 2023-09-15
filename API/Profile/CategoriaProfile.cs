using Api.Dtos;
using AutoMapper;
using Domain.Entities;
namespace Api.Profiles;
public class MappingCategoriaProfile: Profile{
   public MappingCategoriaProfile(){
       CreateMap<EntityDto,Categoria>()
           .ReverseMap();
    }
}