using System.Diagnostics;
using System.Globalization;
using inmind_Lab1_week1.Entities;
using inmind_Lab1_week1.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
namespace inmind_Lab1_week1.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class Studentcontroller : ControllerBase
{
    public List<Student> students = new List<Student>() 
    {
        new Student() { Id = 1, name = "Alice", Email = "alice@example.com" },
        new Student() { Id = 2, name = "Bob", Email = "bob@example.com" },
        new Student() { Id = 3, name = "Charlie", Email = "charlie@example.com" },
        new Student() { Id = 4, name = "David", Email = "david@example.com" },
        new Student() { Id = 5, name = "Eve", Email = "eve@example.com" },
    };
    private readonly IWebHostEnvironment _env;
    private readonly IStudentService _studentService;
    // private readonly IMediator _mediator;
    public Studentcontroller(IWebHostEnvironment env,IStudentService studentService)
    {
        _studentService = studentService;
        _env = env;
        // _mediator = mediator;
       
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        // var query = new GetAllStudentsQuery();
        // var result = await _mediator.Send(query);

        return Ok(await _studentService.GetStudentsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var query = new GetStudentByIdQuery { Id = id };
        // var result = await _mediator.Send(query);
        return Ok(query);
    }

    [HttpGet("NameFilter")]
    public async Task<ActionResult<List<Student>>> GetStudentByNameFilterAsync(string nameFilter)
    {
        var query = new GetStudentByNameFilterQuery { NameFilter = nameFilter };
        // var result = await _mediator.Send(query);

        // if (result == null || result.Count == 0)
        // {
        //     return NotFound();
        // }

        return Ok(query);
    }
    
    [HttpGet("currentDate")]
    public async Task<IActionResult> GetCurrentDate()
    {
        try
        {
            // var query = new GetCurrentDateQuery();
            // var result = await _mediator.Send(query);

            return Ok();
        }
        catch (CultureNotFoundException)
        {
            return BadRequest("Invalid Accept-Language header");
        }
    }
    private CultureInfo GetCultureInfoFromHeader(string header)
    {
        var cultureName = header.Split(',').FirstOrDefault()?.Trim();
        if (string.IsNullOrEmpty(cultureName))
        {
            cultureName = "en-US"; // default culture
        }

        return new CultureInfo(cultureName);
    }
    


    [HttpPost("updateName")]
    public async Task<IActionResult> UpdateStudentName([FromBody] UpdateStudentNameCommand command)
    {
        // await _mediator.Send(command);

        return Ok();
    }

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadStudentImage([FromForm] UploadStudentImageCommand command)
    {
        // var result = await _mediator.Send(command);
        //
        // return Ok(result);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudentById(int id)
    {
        // var command = new DeleteStudentByIdCommand { Id = id };
        // var result = await _mediator.Send(command);

        return Ok();
    }

}
    

