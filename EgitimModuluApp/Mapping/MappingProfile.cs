using AutoMapper;
using Entities;
using Entities.Dtos;
using Entities.Dtos.AccountDtos;
using Entities.Dtos.ClassroomDtos;
using Entities.Dtos.ExamDtos;
using Entities.Dtos.HomeworkDtos;
using Entities.Dtos.LessonDtos;
using Entities.Dtos.SchoolDtos;
using Entities.Dtos.StudentDtos;
using Entities.Dtos.TeacherDtos;

namespace EgitimModuluApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<School, AccountDto>()
            // School -> AccountDto
            .ReverseMap()
            // AccountDto -> School
            .PreserveReferences();

            CreateMap<Teacher, AccountDto>()
            // Teacher -> AccountDto
            .ReverseMap()
            // AccountDto -> Teacher
            .PreserveReferences();

            CreateMap<Student, AccountDto>()
            // Student -> AccountDto
            .ReverseMap()
            // AccountDto -> Student
            .PreserveReferences();

            CreateMap<School, SchoolDto>()
            // School -> SchoolDto
            .ReverseMap()
            // SchoolDto -> School
            .PreserveReferences();

            CreateMap<Classroom, ClassroomDto>()
            // Class -> ClassDto
            .ReverseMap()
            // ClassDto -> Class
            .PreserveReferences();

            CreateMap<Teacher, TeacherDto>()
            // Teacher -> TeacherDto
            .ReverseMap()
            // TeacherDto -> Teacher
            .PreserveReferences();

            CreateMap<Lesson, LessonDto>()
            // Lesson -> LessonDto
            .ReverseMap()
            // LessonDto -> Lesson
            .PreserveReferences();

            CreateMap<Student, StudentDto>()
            // Student -> StudentDto
            .ReverseMap()
            // StudentDto -> Student
            .PreserveReferences();

            CreateMap<School, SchoolRegisterDto>()
            // School -> AccountDto
            .ReverseMap()
            // AccountDto -> School
            .PreserveReferences();

            CreateMap<Exam, ExamDto>()
            // Exam -> ExamDto
            .ReverseMap()
            // ExamDto -> Exam
            .PreserveReferences();

            CreateMap<Homework, HomeworkDto>()
            // Exam -> ExamDto
            .ReverseMap()
            // ExamDto -> Exam
            .PreserveReferences();
        }
    }
}
