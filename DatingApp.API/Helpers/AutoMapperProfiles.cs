using AutoMapper;
using DatingApp.API.Models;
using DatingApp.API.Dtos;
using System.Linq;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>().ForMember(dest => dest.PhotoUrl, option => option.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(destination => destination.Age, option => option.MapFrom(sourceMember => sourceMember.DateOfBirth.CalculateAge()));

            CreateMap<User, UserForDetailedDto>().ForMember(dest => dest.PhotoUrl, option => option.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(destination => destination.Age, option => option.MapFrom(sourceMember => sourceMember.DateOfBirth.CalculateAge())); ;

            CreateMap<Photo, PhotosForDetailedDto>();

            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();

            CreateMap<PhotoForCreationDto, Photo>();

            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
               .ForMember(m => m.SenderPhotoUrl, opt => opt
                   .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
               .ForMember(m => m.RecipientPhotoUrl, opt => opt
                   .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));

        }
    }
}