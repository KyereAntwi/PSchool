using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Students.Commands.UpdateStudent;

public class UpdateStudentCommand : IRequest
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Username { get; set; }
}

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Student> _asyncRepository;

    public UpdateStudentCommandHandler(IMapper mapper, IAsyncRepository<Student> asyncRepository)
    {
        _mapper = mapper;
        _asyncRepository = asyncRepository;
    }

    public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _asyncRepository.GetByIdAsync(request.Id);
        if (student is null)
            throw new Exceptions.NotFoundException(nameof(Parent), request.Id);

        var validation = new UpdateStudentCommandValidator();
        var validatoinResults = await validation.ValidateAsync(request);

        if (validatoinResults.Errors.Count > 0)
            throw new Exceptions.ValidationExceptions(validatoinResults);

        _mapper.Map(request, student, typeof(UpdateStudentCommand), typeof(Student));
        await _asyncRepository.UpdateAsync(student);

        return Unit.Value;
    }
}
