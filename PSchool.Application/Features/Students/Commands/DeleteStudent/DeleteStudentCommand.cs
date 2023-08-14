using MediatR;
using PSchool.Application.Contracts;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Students.Commands.DeleteStudent;

public class DeleteStudentCommand : IRequest
{
    public Guid StudentId { get; set; }
}

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
{
    private readonly IAsyncRepository<Student> _asyncRepository;
    public DeleteStudentCommandHandler(IAsyncRepository<Student> asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }

    public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _asyncRepository.GetByIdAsync(request.StudentId);

        if (student is null)
            throw new Exceptions.NotFoundException(nameof(Parent), request.StudentId);

        await _asyncRepository.DeleteAsync(student);

        return Unit.Value;
    }
}
