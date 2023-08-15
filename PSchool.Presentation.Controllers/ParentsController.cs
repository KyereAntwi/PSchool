using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSchool.Application.Features.Parents.Commands.CreateParent;
using PSchool.Application.Features.Parents.Commands.DeleteParent;
using PSchool.Application.Features.Parents.Commands.UpdateParent;
using PSchool.Application.Features.Parents.Querries.GetParentDetail;
using PSchool.Application.Features.Parents.Querries.GetParentsList;

namespace PSchool.Presentation.Controllers;

[Route("api/parents")]
[ApiController]
public class ParentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ParentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllParents([FromQuery] int pageSize = 10, [FromQuery] int page = 1)
    {
        var parentDtos = await _mediator.Send(new GetParentsListQuery()
        {
            Page = page,
            Size = pageSize
        });

        return Ok(parentDtos);
    }

    [HttpGet("{parentId}")]
    public async Task<ActionResult> GetParentDetail([FromRoute] Guid parentId)
    {
        var parentDto = await _mediator.Send(new GetParentDetailQuerry()
        {
            ParentId = parentId
        });
        return Ok(parentDto);
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateParent([FromBody] CreateParentCommand createParentCommand)
    {
        var id = await _mediator.Send(createParentCommand);
        return CreatedAtAction(nameof(GetParentDetail), new
        {
            parentId = id
        }, id);
    }

    [HttpDelete("{parentId}")]
    public async Task<ActionResult> DeleteParent([FromHeader] Guid parentId)
    {
        await _mediator.Send(new DeleteParentCommand() { ParentId = parentId });
        return NoContent();
    }

    [HttpPut("")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateParent([FromBody] UpdateParentCommand updateParentCommand)
    {
        await _mediator.Send(updateParentCommand);
        return NoContent();
    }
}
