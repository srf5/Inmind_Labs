using Lab3.Entities;
using Lab3.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Lab3.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly MyDbContext _dbContext;

    public StudentRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _dbContext.Students.FindAsync(id);
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
        return await _dbContext.Students.ToListAsync();
    }
    

    public void RemoveStudent(Student student)
    {
        _dbContext.Students.Remove(student);
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _dbContext.Students.ToListAsync();
    }

    Task IStudentRepository.SaveChangesAsync()
    {
        return SaveChangesAsync();
    }

    public async Task<object?> GetAsync(int id)
    {
        return await _dbContext.Students.FindAsync(id);
    }

    

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void AddStudent(Student student)
    {
        _dbContext.Students.Add(student);
    }

    public Task<Student> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(Student student)
    {
        _dbContext.Students.Add(student);
    }

    public void Update(Student student)
    {
        _dbContext.Entry(student).State = EntityState.Modified;
    }

    public void Remove(Student student)
    {
        _dbContext.Students.Remove(student);
    }
}
