using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Application.Features.Common;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Students.Querries.GetStudentDetail;

public class GetStudentDetailQuery : IRequest<StudentDto>
{
    public Guid StudentId { get; set; }
}

public class GetStudentDetailQueryHandler : IRequestHandler<GetStudentDetailQuery, StudentDto>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Student> _asyncRepository;

    public GetStudentDetailQueryHandler(IMapper mapper, IAsyncRepository<Student> asyncRepository)
    {
        _mapper = mapper;
        _asyncRepository = asyncRepository;
    }

    public async Task<StudentDto> Handle(GetStudentDetailQuery request, CancellationToken cancellationToken)
    {
        var student = await _asyncRepository.GetByIdAsync(request.StudentId);

        if (student is null)
            throw new Exceptions.NotFoundException(nameof(Student), request.StudentId);

        return _mapper.Map<StudentDto>(student);
    }
}
