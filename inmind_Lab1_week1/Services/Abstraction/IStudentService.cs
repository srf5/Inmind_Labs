using inmind_Lab1_week1.Entities;

namespace inmind_Lab1_week1.Services.Abstraction;

public interface IStudentService
{
    Task<List<Student>> GetStudentsAsync();
    string GetCurrentDate(string acceptedLanguage);
    Task<string> UploadImage(IFormFile file);
    Task<List<Student>> GetStudentByNameFilterAsync(string requestNameFilter);
    Task DeleteStudentByIdAsync(int studentId);
    Task<Student> GetStudentByIdAsync(int id); 
    Task<Student> UpdateStudentNameAsync(int id, string newName);
}
