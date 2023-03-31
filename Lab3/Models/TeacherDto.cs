using System.ComponentModel.DataAnnotations;

namespace Lab3.Models;

public class TeacherDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Branch { get; set; }
    
}
