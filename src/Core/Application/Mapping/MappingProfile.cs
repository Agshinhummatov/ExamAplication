using Application.DTOs.Exam;
using Application.DTOs.Lesson;
using Application.DTOs.Student;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            CreateMap<Exam, ExamListDTO>()
        .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson));

            CreateMap<ExamCreateAndUpdateDto, Exam>()
                .ForMember(dest => dest.Students, opt => opt.Ignore())
                .ForMember(dest => dest.Lesson, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<StudentCreateAndUpdateDto, Student >();
            CreateMap<Student, StudentListDto>();
            CreateMap<StudentCreateAndUpdateDto, Student>().ReverseMap();


            CreateMap<LessonCreateAndUpdateDto, Lesson>();
            CreateMap<Lesson, LessonListDto>();
            CreateMap<LessonCreateAndUpdateDto, Lesson>().ReverseMap();


        }
    }
}
