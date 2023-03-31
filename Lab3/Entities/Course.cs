namespace Lab3.Entities;
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MaxStudents { get; set; }
    public DateTime EnrollmentStartDate { get; set; }
    public DateTime EnrollmentEndDate { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
    public int TeacherId { get; set; }
}
