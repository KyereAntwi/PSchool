using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSchool.Application.Features.Students.Commands.CreateStudent;
using PSchool.Application.Features.Students.Commands.DeleteStudent;
using PSchool.Application.Features.Students.Commands.UpdateStudent;
using PSchool.Application.Features.Students.Querries.GetStudentDetail;
using PSchool.Application.Features.Students.Querries.GetStudentsByParentList;
using PSchool.Application.Features.Students.Querries.GetStudentsList;

namespace PSchool.Presentation.Controllers;

[Route("api/students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetStudentsList([FromQuery] int pageSize = 20, [FromQuery] int page = 1)
    {
        var studentsListDto = await _mediator.Send(new GetStudentsListQuery()
        {
            Page = page,
            Size = pageSize
        });

        return Ok(studentsListDto);
    }

    [HttpGet("byparent/{parentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetStudentsByParentList([FromRoute] Guid parentId)
    {
        var studentsListDto = await _mediator.Send(new GetStudentsByParentListQuery()
        {
            ParentId = parentId
        });

        return Ok(studentsListDto);
    }

    [HttpGet("{studentId}")]
    public async Task<ActionResult> GetStudentDetail([FromRoute] Guid studentId)
    {
        var studentDto = await _mediator.Send(new GetStudentDetailQuery() { StudentId = studentId });
        return Ok(studentDto);
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateStudent([FromBody] CreateStudentCommand createStudentCommand)
    {
        var id = await _mediator.Send(createStudentCommand);
        return CreatedAtAction(nameof(GetStudentDetail), new { studentId = id }, id);
    }

    [HttpPut("")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentCommand updateStudentCommand)
    {
        await _mediator.Send(updateStudentCommand);
        return NoContent();
    }

    [HttpDelete("{studentId}")]
    public async Task<ActionResult> DeleteStudent([FromRoute] Guid studentId)
    {
        await _mediator.Send(new DeleteStudentCommand() { StudentId = studentId });
        return NoContent();
    }
}
