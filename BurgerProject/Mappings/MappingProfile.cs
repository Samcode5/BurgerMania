using AutoMapper;
using BurgerProject.DTOs;
using BurgerProject.Model;

namespace BurgerProject.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<Burger,BurgerDTO>().ReverseMap();
            CreateMap<Order,OrderDTO>().ReverseMap();
            CreateMap<BurgerType,BurgerTypeDTO>().ReverseMap();
        }
    }
}
