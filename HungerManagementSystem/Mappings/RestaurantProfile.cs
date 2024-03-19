// RestaurantProfile.cs
using AutoMapper;
using HungerManagementSystem.DTO;
using HungerManagementSystem.EF;

namespace HungerManagementSystem.Mappings
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>(); // Mapping from Restaurant entity to RestaurantDTO
            CreateMap<RestaurantDTO, Restaurant>(); // Mapping from RestaurantDTO to Restaurant entity
        }
    }
}
