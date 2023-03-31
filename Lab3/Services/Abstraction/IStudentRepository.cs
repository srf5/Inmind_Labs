using Lab3.Entities;

namespace Lab3.Services.Abstraction;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student> GetStudentByIdAsync(int id);
    Task<IEnumerable<Student>> GetStudentsAsync();
    void RemoveStudent(Student student);
    Task<List<Student>> GetAllStudentsAsync();
    Task SaveChangesAsync();
    void AddStudent(Student student);
}
