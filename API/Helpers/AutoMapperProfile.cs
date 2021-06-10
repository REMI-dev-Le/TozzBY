using AutoMapper;
using API.Entities;
using API.Extensions;
using API.DTO;
using System.Linq;
namespace API.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<AppUser, MemberDTO>()
      .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
      .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
      CreateMap<Photo, PhotoDto>();
      CreateMap<MemberupdateDTO, AppUser>();
    }
  }
}