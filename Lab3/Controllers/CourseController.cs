using Lab3.Entities;
using Lab3.Models;
using Lab3.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<CourseDto> GetCourseByIdAsync(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        return course;
    }

    [HttpGet]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<ActionResult<IEnumerable<Course>>> GetCoursesAsync()
    {
        var courses = await _courseService.GetAllCoursesAsync();

        return Ok(courses);
    }

    [HttpGet("teacher/{teacherId}")]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByTeacherIdAsync(int teacherId)
    {
        var courses = await _courseService.GetCoursesByTeacherIdAsync(teacherId);

        return Ok(courses);
    }

    [HttpPost]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<ActionResult<Course>> CreateCourseAsync(CourseDto course)
    {
        await _courseService.AddCourseAsync(course);

        return Created("", course);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<IActionResult> UpdateCourseAsync(int id, CourseDto course)
    {
        if (id != course.Id)
        {
            return BadRequest();
        }

        await _courseService.UpdateCourseAsync(course.Id, course);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "CampusPolicy")]
    public async Task<IActionResult> DeleteCourseAsync(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        await _courseService.RemoveCourseAsync(course.Id);

        return NoContent();
    }
}
