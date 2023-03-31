namespace Lab3.Models;

using System.ComponentModel.DataAnnotations;



public class CourseDto
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Range(1, int.MaxValue)]
    public int MaxStudents { get; set; }

    [Required]
    public DateTime EnrollmentStartDate { get; set; }

    [Required]
    public DateTime EnrollmentEndDate { get; set; }

    public int Id { get; set; }
}
