using Lab3.Entities;

namespace Lab3.Services.Abstraction;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course> GetCourseByIdAsync(int id);
    Task<IEnumerable<Course>> GetCoursesAsync();
    Task<IEnumerable<Course>> GetCoursesByTeacherIdAsync(int teacherId);
    void RemoveCourse(Course course);
    Task<object?> GetAsync(int id);
    Task<object?> GetAllAsync();
    Task<int> SaveChangesAsync();
}
