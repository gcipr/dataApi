using AutoMapper;
using Data.Domain.Entities;
using Data.Domain.EntitiesDTO;

namespace Data.Web.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<FishingSpot, FishingSpotDTO>();
            CreateMap<FishingSpotDTO, FishingSpot>();
        }
    }
}
