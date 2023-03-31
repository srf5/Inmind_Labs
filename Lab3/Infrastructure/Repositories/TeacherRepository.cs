using Lab3.Entities;
using Lab3.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly MyDbContext _context;

    public TeacherRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Teacher>> GetAllTeachersAsync()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task<IEnumerable<Teacher>> GetTeachersAsync()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task<Teacher> GetTeacherByIdAsync(int id)
    {
        return await _context.Teachers.FindAsync(id);
    }

    public void RemoveTeacher(Teacher teacher)
    {
        _context.Teachers.Remove(teacher);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void AddTeacher(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
    }

    public Task<Teacher> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
    }

    public void Update(Teacher teacher)
    {
        _context.Entry(teacher).State = EntityState.Modified;
    }

    public void Remove(Teacher teacher)
    {
        _context.Teachers.Remove(teacher);
    }
}
