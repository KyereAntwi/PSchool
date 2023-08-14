using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Application.Features.Common;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Parents.Querries.GetParentDetail;

public class GetParentDetailQuerry : IRequest<ParentDto>
{
    public Guid ParentId { get; set; }
}

public class GetParentDetailQuerryHandler : IRequestHandler<GetParentDetailQuerry, ParentDto>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Parent> _asyncRepository;
    public GetParentDetailQuerryHandler(IMapper mapper, IAsyncRepository<Parent> asyncRepository)
    {
        _mapper = mapper;
        _asyncRepository = asyncRepository;
    }

    public async Task<ParentDto> Handle(GetParentDetailQuerry request, CancellationToken cancellationToken)
    {
        var parent = await _asyncRepository.GetByIdAsync(request.ParentId);

        if (parent is null)
            throw new Exceptions.NotFoundException(nameof(Parent), request.ParentId);

        return _mapper.Map<ParentDto>(parent);
    }
}