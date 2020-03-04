using AutoMapper;
using first_rest_api.Models;
using first_rest_api.ResponseObjects;
using first_rest_api.CustomExceptions;

namespace first_rest_api.Utilities
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() {
            CreateMap<UserDetails, UserDetailsObject>();
        }
    }
}