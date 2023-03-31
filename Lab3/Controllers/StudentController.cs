using System.Threading.Tasks;
using Lab3.Entities;
using Lab3.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public StudentController(IStudentService studentService, RoleManager<IdentityRole> roleManager)
        {
            _studentService = studentService;
            _roleManager = roleManager;
        }



        // Create a method to create a student role
        public async Task<IActionResult> CreateStudentRole()
        {
            // Check if the role already exists
            if (await _roleManager.RoleExistsAsync("Student"))
            {
                return BadRequest("Role already exists");
            }
        
            // Create a new role for students
            var role = new IdentityRole { Name = "Student" };
            var result = await _roleManager.CreateAsync(role);
        
            if (result.Succeeded)
            {
                return Ok("Student role created successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        

        [HttpGet]
        [Authorize(Roles = "Student")]
        [Authorize(Policy = "CampusPolicy")]
        public async Task<IActionResult> GetStudentsAsync()
        {
            var students = await _studentService.GetStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Student")]
        [Authorize(Policy = "CampusPolicy")]
        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // [HttpPost]
        // public async Task<IActionResult> CreateStudentAsync(Student student)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest();
        //     }
        //
        //     var createdStudent = await _studentService.AddStudentAsync(student);
        //     return CreatedAtAction(nameof(StudentController.GetStudentByIdAsync), new { id = createdStudent.Id }, createdStudent);
        //
        // }
        [HttpPost]
        [Authorize(Roles = "Student")]
        [Authorize(Policy = "CampusPolicy")]
        public async Task<IActionResult> CreateStudentAsync(Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentService.AddStudentAsync(student);
                return Ok(student);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Student")]
        [Authorize(Policy = "CampusPolicy")]
        public async Task<IActionResult> UpdateStudentNameAsync(int id, string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedStudent = await _studentService.UpdateStudentNameAsync(id, name);

            if (updatedStudent == null)
            {
                return NotFound();
            }

            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Student")]
        [Authorize(Policy = "CampusPolicy")]
        public async Task<IActionResult> DeleteStudentByIdAsync(int id)
        {
            await _studentService.DeleteStudentByIdAsync(id);
            return NoContent();
        }

        [HttpGet("filter")]
        [Authorize(Roles = "Student")]
        [Authorize(Policy = "CampusPolicy")]
        public async Task<IActionResult> GetStudentByNameFilterAsync(string nameFilter)
        {
            var students = await _studentService.GetStudentByNameFilterAsync(nameFilter);
            return Ok(students);
        }

        [HttpPost("uploadImage")]
        [Authorize(Roles = "Student")]
        [Authorize(Policy = "CampusPolicy")]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            var imagePath = await _studentService.UploadImage(file);
            return Ok(imagePath);
        }

        [HttpGet("currentDate")]
        [Authorize(Roles = "Student")]
        [Authorize(Policy = "CampusPolicy")]
        public IActionResult GetCurrentDate(string language)
        {
            var currentDate = _studentService.GetCurrentDate(language);
            return Ok(currentDate);
        }

        
    }
}
