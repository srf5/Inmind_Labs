using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
namespace inmind_Lab1_week1.Controllers;

[ApiController]
[Route("[controller]")]

public class Studentcontroller : ControllerBase
{
    public List<Student> students;
    private readonly IWebHostEnvironment _env;
    private readonly IStudentService _studentService;
    public Studentcontroller(IWebHostEnvironment env,IStudentService studentService)
    {
        _studentService = studentService;
        _env = env;

        students = new List<Student>()
        {
            new Student() { Id = 1, name = "Alice", Email = "alice@example.com" },
            new Student() { Id = 2, name = "Bob", Email = "bob@example.com" },
            new Student() { Id = 3, name = "Charlie", Email = "charlie@example.com" },
            new Student() { Id = 4, name = "David", Email = "david@example.com" },
            new Student() { Id = 5, name = "Eve", Email = "eve@example.com" },
        };
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
    public IActionResult GetAllStudents()
    {
        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(long id)
    {
        Student student = students.FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpGet("Namefilter")]
    public IActionResult GetStudentsByName([FromQuery] string name)
    {
        List<Student> filteredStudents = students.Where(s => s.name.Contains(name)).ToList();

        return Ok(filteredStudents);
    }
    
    [HttpGet("currentDate")]
    public IActionResult GetCurrentDate()
    {
        try
        {
            var culture = GetCultureInfoFromHeader(Request.Headers["Accept-Language"]);
            var formattedDate = DateTime.Now.ToString("D", culture);
            return Ok(formattedDate);
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
    public ActionResult<Student> UpdateName([FromBody] Student studentupdated)
    {

        if (studentupdated == null)
        {
            return NotFound();
        }

        for (int i = 0; i < students.Count; ++i)
        {
            if (students[i].Id == studentupdated.Id)
            {
                students[i].name = studentupdated.name;
                return students[i];
            }
        }

        return Ok(studentupdated);
    }

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Invalid file");
        }

        var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        return Ok(new { Path = $"/uploads/{fileName}" });
    }

    [HttpDelete("{id}")]
    public ActionResult<Student> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid student id");
        }

        bool validation = false;
        for (int i = 0; i < students.Count; ++i)
        {
            if (students[i].Id == id)
            {
                validation = true;
                break;
            }
        }

        if (validation == false)
        {
            return BadRequest("Invalid student id");
        }
        for (int i = 0; i < students.Count; ++i)
        {
            if (students[i].Id == id)
            {
                var student = students[i];
                students.Remove(students[i]);
                return student;
            }
        }

        return Ok();
    }

}
    

