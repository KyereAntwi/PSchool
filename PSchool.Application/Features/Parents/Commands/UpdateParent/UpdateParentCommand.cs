using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Parents.Commands.UpdateParent;

public class UpdateParentCommand : IRequest
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Username { get; set; }
}

public class UpdateParentCommandHandler : IRequestHandler<UpdateParentCommand>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Parent> _asyncRepository;

    public UpdateParentCommandHandler(IMapper mapper, IAsyncRepository<Parent> asyncRepository)
    {
        _mapper = mapper;
        _asyncRepository = asyncRepository;
    }

    public async Task<Unit> Handle(UpdateParentCommand request, CancellationToken cancellationToken)
    {
        var parent = await _asyncRepository.GetByIdAsync(request.Id);
        if (parent is null)
        {
            throw new Exceptions.NotFoundException(nameof(Parent), request.Id);
        }

        var validation = new UpdateParentCommandValidator();
        var validatoinResults = await validation.ValidateAsync(request);

        if (validatoinResults.Errors.Count > 0)
            throw new Exceptions.ValidationExceptions(validatoinResults);

        _mapper.Map(request, parent, typeof(UpdateParentCommand), typeof(Parent));
        await _asyncRepository.UpdateAsync(parent);

        return Unit.Value;
    }
}
