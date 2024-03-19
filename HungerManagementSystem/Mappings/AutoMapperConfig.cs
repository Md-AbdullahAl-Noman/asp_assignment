// AutoMapperConfig.cs
using AutoMapper;
using HungerManagementSystem.Mappings;

namespace HungerManagementSystem
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantProfile>();
                // Add other mapping profiles as needed
            });

            return config;
        }
    }
}
