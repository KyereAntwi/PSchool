using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Application.Features.Common;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Parents.Querries.GetAllParents;

public class GetAllParentsQuery : IRequest<IEnumerable<ParentDto>>
{
}

public class GetAllParentQueryHandler : IRequestHandler<GetAllParentsQuery, IEnumerable<ParentDto>>
{
    private readonly IAsyncRepository<Parent> _asyncRepository;
    private readonly IMapper _mapper;

    public GetAllParentQueryHandler(IAsyncRepository<Parent> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ParentDto>> Handle(GetAllParentsQuery request, CancellationToken cancellationToken)
    {
        var parentList = await _asyncRepository.ListAllAsync();
        return _mapper.Map<IEnumerable<ParentDto>>(parentList);
    }
}

