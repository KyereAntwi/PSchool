using AutoMapper;
using MediatR;
using PSchool.Application.Contracts;
using PSchool.Application.Features.Common;
using PSchool.Domain.Entities;

namespace PSchool.Application.Features.Parents.Querries.GetParentsList;

public class GetParentsListQuery : IRequest<PaggedListVm<ParentDto>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}

public class GetParentsListQueryHandler : IRequestHandler<GetParentsListQuery, PaggedListVm<ParentDto>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Parent> _asyncRepository;

    public GetParentsListQueryHandler(IMapper mapper, IAsyncRepository<Parent> asyncRepository)
    {
        _mapper = mapper;
        _asyncRepository = asyncRepository;
    }

    public async Task<PaggedListVm<ParentDto>> Handle(GetParentsListQuery request, CancellationToken cancellationToken)
    {
        var parentList = await _asyncRepository.GetPagedResponseAsync(request.Page, request.Size);
        var parentsListDto = _mapper.Map<List<ParentDto>>(parentList);

        var count = parentList.Count;

        return new PaggedListVm<ParentDto>()
        {
            Count = count,
            ListItems = parentsListDto,
            Page = request.Page,
            Size = request.Size
        };
    }
}