using AutoMapper;
using PSchool.Application.Features.Common;
using PSchool.Application.Features.Parents.Commands.CreateParent;
using PSchool.Application.Features.Students.Commands.CreateStudent;
using PSchool.Domain.Entities;

namespace PSchool.Application.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Parent, ParentDto>().ReverseMap();
        CreateMap<CreateParentCommand, Parent>();

        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<CreateStudentCommand, Student>();
    }
}
