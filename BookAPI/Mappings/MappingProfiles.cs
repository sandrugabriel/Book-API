using AutoMapper;
using BookAPI.Dto;
using BookAPI.Models;

namespace BookAPI.Mappings
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {


            CreateMap<CreateRequest, Book>();
        }

    }
}
