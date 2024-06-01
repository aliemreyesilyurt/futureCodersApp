using AutoMapper;
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

            CreateMap<BlogDtoForUpdate, Blog>().ReverseMap();
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogDtoForInsertion, Blog>();

            CreateMap<StepDtoForUpdate, Step>().ReverseMap();
            CreateMap<Step, StepDto>();
            CreateMap<StepDtoForInsertion, Step>();

            CreateMap<UserStep, UserStepDto>();
            CreateMap<UserStepDtoForInsertion, UserStep>();

            CreateMap<UserCourse, UserCourseDto>();
            CreateMap<UserCourseDtoForInsertion, UserCourse>();

            CreateMap<ReviewDtoForUpdate, Review>().ReverseMap();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDtoForInsertion, Review>();

            CreateMap<UserDtoForUpdate, User>().ReverseMap();
            CreateMap<User, UserDto>();
            CreateMap<UserForRegistirationDto, User>();
        }
    }
}
