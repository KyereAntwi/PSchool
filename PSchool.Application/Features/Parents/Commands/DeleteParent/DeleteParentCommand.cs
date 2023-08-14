using MediatR;
using PSchool.Application.Contracts;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Parents.Commands.DeleteParent;

public class DeleteParentCommand : IRequest
{
    public Guid ParentId { get; set; }
}

public class DeleteParentCommandHandler : IRequestHandler<DeleteParentCommand>
{
    private readonly IAsyncRepository<Parent> _asyncRepository;

    public DeleteParentCommandHandler(IAsyncRepository<Parent> asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }

    public async Task<Unit> Handle(DeleteParentCommand request, CancellationToken cancellationToken)
    {
        var parent = await _asyncRepository.GetByIdAsync(request.ParentId);

        if (parent is null)
            throw new Exceptions.NotFoundException(nameof(Parent), request.ParentId);

        await _asyncRepository.DeleteAsync(parent);

        return Unit.Value;
    }
}
