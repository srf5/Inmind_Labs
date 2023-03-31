using Lab3.Entities;
using Lab3.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly MyDbContext _context;

    public CourseRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<Course> GetByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetCoursesByTeacherIdAsync(int teacherId)
    {
        return await _context.Courses.Where(c => c.TeacherId == teacherId).ToListAsync();
    }

    public void Add(Course entity)
    {
        _context.Courses.Add(entity);
    }

    public void Update(Course entity)
    {
        _context.Courses.Update(entity);
    }

    public void Remove(Course entity)
    {
        _context.Courses.Remove(entity);
    }

    public void RemoveCourse(Course course)
    {
        _context.Courses.Remove(course);
    }

    public async Task<object?> GetAsync(int id)
    {
        return await GetByIdAsync(id);
    }

    public async Task<object?> GetAllAsync()
    {
        return await GetCoursesAsync();
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
