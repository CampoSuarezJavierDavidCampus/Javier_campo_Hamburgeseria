using API.Dtos;
using AutoMapper;
using Dominio.Entities;
namespace Api.Profiles;
public class MappingHamburguesaProfile: Profile{
   public MappingHamburguesaProfile(){
       CreateMap<HamburguesaSimpleDto,Hamburguesa>()
           .ReverseMap();
    }
}