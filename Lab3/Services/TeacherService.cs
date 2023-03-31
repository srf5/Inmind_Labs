using Lab3.Entities;
using Lab3.Exceptions;
using Lab3.Services.Abstraction;

namespace Lab3.Services;
public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<IEnumerable<Teacher>> GetTeachersAsync()
    {
        return await _teacherRepository.GetTeachersAsync();
    }

    public async Task<Teacher> GetTeacherByIdAsync(int id)
    {
        return await _teacherRepository.GetTeacherByIdAsync(id);
    }

    public async Task<Teacher> UpdateTeacherAsync(int id, Teacher teacher)
    {
        var existingTeacher = await _teacherRepository.GetTeacherByIdAsync(id);
        if (existingTeacher == null)
        {
            throw new NotFoundException($"Teacher with id {id} not found");
        }

        existingTeacher.Name = teacher.Name;
        existingTeacher.Email = teacher.Email;

       await _teacherRepository.SaveChangesAsync();
       return await _teacherRepository.GetTeacherByIdAsync(id);
    }

    public async Task DeleteTeacherByIdAsync(int id)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(id);
        if (teacher == null)
        {
            throw new NotFoundException($"Teacher with id {id} not found");
        }

        _teacherRepository.RemoveTeacher(teacher);
        await _teacherRepository.SaveChangesAsync();
    }
    public async Task<Teacher> CreateTeacherAsync(Teacher teacher)
    {
        if (teacher == null)
        {
            throw new ArgumentNullException(nameof(teacher));
        }

        _teacherRepository.AddTeacher(teacher);
        await _teacherRepository.SaveChangesAsync();

        return teacher;
    }

}
