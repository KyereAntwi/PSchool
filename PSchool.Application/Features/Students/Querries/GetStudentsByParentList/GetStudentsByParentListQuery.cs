using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Application.Features.Common;

namespace PSchool.Application.Features.Students.Querries.GetStudentsByParentList;

public class GetStudentsByParentListQuery : IRequest<PaggedListVm<StudentDto>>
{
    public Guid ParentId { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
}

public class GetStudentsByParentListQueryHandler : IRequestHandler<GetStudentsByParentListQuery, PaggedListVm<StudentDto>>
{
    private readonly IMapper _mapper;
    private readonly IStudentRepository _studentRepository;

    public GetStudentsByParentListQueryHandler(IMapper mapper, IStudentRepository studentRepository)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
    }

    public async Task<PaggedListVm<StudentDto>> Handle(GetStudentsByParentListQuery request, CancellationToken cancellationToken)
    {
        var studentList = await _studentRepository.GetStudentsByParentId(request.ParentId);
        var studentListDto = _mapper.Map<List<StudentDto>>(studentList);

        var count = studentList.Count;

        return new PaggedListVm<StudentDto>()
        {
            Count = count,
            ListItems = studentListDto,
            Page = request.Page,
            Size = request.Size
        };

    }
}
