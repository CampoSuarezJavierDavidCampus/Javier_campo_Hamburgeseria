using API.Dtos;
using AutoMapper;
using Dominio.Entities;
namespace Api.Profiles;
public class MappingChefProfile: Profile{
   public MappingChefProfile(){
       CreateMap<ChefDto,Chef>()
           .ReverseMap();
    }
}