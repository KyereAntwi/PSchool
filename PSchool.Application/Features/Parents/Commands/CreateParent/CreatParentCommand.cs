using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Parents.Commands.CreateParent;

public class CreateParentCommand : IRequest<Guid>
{
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Username { get; set; }
}

public class CreeateParentCommandHandler : IRequestHandler<CreateParentCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Parent> _asyncRepository;

    public CreeateParentCommandHandler(IMapper mapper, IAsyncRepository<Parent> asyncRepository)
    {
        _mapper = mapper;
        _asyncRepository = asyncRepository;
    }

    public async Task<Guid> Handle(CreateParentCommand request, CancellationToken cancellationToken)
    {
        var validation = new CreateParentCommandValidator();
        var validationResult = await validation.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            throw new Exceptions.ValidationExceptions(validationResult);
        }

        var parent = _mapper.Map<Parent>(request);

        parent = await _asyncRepository.AddAsync(parent);

        return parent.Id;
    }
}
