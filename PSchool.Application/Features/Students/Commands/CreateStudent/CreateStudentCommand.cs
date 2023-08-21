using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentCommand : IRequest<Guid>
{
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Username { get; set; }
    public Guid ParentId { get; set; }
}

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Student> _asyncRepository;

    public CreateStudentCommandHandler(IMapper mapper, IAsyncRepository<Student> asyncRepository)
    {
        _mapper = mapper;
        _asyncRepository = asyncRepository;
    }

    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var validation = new CreateStudentCommandValidator();
        var validationResult = await validation.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            throw new Exceptions.ValidationExceptions(validationResult);
        }

        var student = _mapper.Map<Student>(request);

        student = await _asyncRepository.AddAsync(student);

        return student.Id;
    }
}
