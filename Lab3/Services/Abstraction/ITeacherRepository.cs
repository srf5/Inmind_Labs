using Lab3.Entities;

namespace Lab3.Services.Abstraction;

public interface ITeacherRepository : IRepository<Teacher>
{
    Task<Teacher> GetTeacherByIdAsync(int id);
    Task<IEnumerable<Teacher>> GetTeachersAsync();
    void RemoveTeacher(Teacher teacher);
    Task<List<Teacher>> GetAllTeachersAsync();
    Task SaveChangesAsync();
    void AddTeacher(Teacher teacher);
}