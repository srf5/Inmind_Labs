using Lab3.Entities;
using Lab3.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    private readonly RoleManager<IdentityRole> _roleManager;
    public TeacherController(ITeacherService teacherService, RoleManager<IdentityRole> roleManager)
    {
        _teacherService = teacherService;
        _roleManager = roleManager;
    }
    public async Task<IActionResult> CreateStudentRole()
    {
        // Check if the role already exists
        if (await _roleManager.RoleExistsAsync("Teacher"))
        {
            return BadRequest("Role already exists");
        }
        
        // Create a new role for students
        var role = new IdentityRole { Name = "Teacher" };
        var result = await _roleManager.CreateAsync(role);
        
        if (result.Succeeded)
        {
            return Ok("Teacher role created successfully");
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Teacher")]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<ActionResult<Teacher>> GetTeacherByIdAsync(int id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        return Ok(teacher);
    }

    [HttpGet]
    [Authorize(Roles = "Teacher")]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachersAsync()
    {
        var teachers = await _teacherService.GetTeachersAsync();
        return Ok(teachers);
    }

    // [HttpPost]
    // public async Task<ActionResult<Teacher>> CreateTeacherAsync(Teacher teacher)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     await _teacherService.CreateTeacherAsync(teacher);
    //     return CreatedAtAction("GetTeacherByIdAsync", new { id = teacher.Id }, teacher);
    // }
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<ActionResult<Teacher>> CreateTeacherAsync(Teacher teacher)
    {
        try
        {
            var createdTeacher = await _teacherService.CreateTeacherAsync(teacher);

            return StatusCode(201, createdTeacher);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Teacher")]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<IActionResult> UpdateTeacherAsync(int id, Teacher teacher)
    {
        if (id != teacher.Id)
        {
            return BadRequest();
        }

        await _teacherService.UpdateTeacherAsync(teacher.Id,teacher);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Teacher")]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<IActionResult> DeleteTeacherAsync(int id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        await _teacherService.DeleteTeacherByIdAsync(teacher.Id);

        return NoContent();
    }
}
