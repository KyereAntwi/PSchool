using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Application.Features.Common;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Students.Querries.GetStudentsList;

public class GetStudentsListQuery : IRequest<PaggedListVm<StudentDto>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}

public class GetStudentsListQueryHandler : IRequestHandler<GetStudentsListQuery, PaggedListVm<StudentDto>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Student> _asyncRepository;

    public GetStudentsListQueryHandler(IMapper mapper, IAsyncRepository<Student> asyncRepository)
    {
        _mapper = mapper;
        _asyncRepository = asyncRepository;
    }

    public async Task<PaggedListVm<StudentDto>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
    {
        var studentList = await _asyncRepository.GetPagedResponseAsync(request.Page, request.Size);
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
