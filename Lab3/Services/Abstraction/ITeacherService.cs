using Lab3.Entities;

namespace Lab3.Services.Abstraction;

public interface ITeacherService
{
    Task<IEnumerable<Teacher>>  GetTeachersAsync();
    Task<Teacher> GetTeacherByIdAsync(int id);
    Task<Teacher> CreateTeacherAsync(Teacher teacher);
    Task<Teacher> UpdateTeacherAsync(int id, Teacher teacher);
    Task DeleteTeacherByIdAsync(int id);
}