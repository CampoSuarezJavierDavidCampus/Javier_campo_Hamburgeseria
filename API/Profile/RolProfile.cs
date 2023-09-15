using API.Dtos;
using AutoMapper;
using Dominio.Entities;
namespace Api.Profiles;
public class MappingRolProfile: Profile{
   public MappingRolProfile(){
       CreateMap<RolDto,Rol>()
           .ReverseMap();
    }
}