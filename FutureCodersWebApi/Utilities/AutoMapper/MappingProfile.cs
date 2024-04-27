﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseDtoForUpdate, Course>().ReverseMap();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDtoForInsertion, Course>();
            CreateMap<UserForRegistirationDto, User>();
        }
    }
}
