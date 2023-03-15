namespace inmind_Lab1_week1;

public interface IStudentService
{
    List<Student> GetAllStudents();
    Student GetStudentById(long id);
    List<Student> GetStudentsByName(string name);
    string GetCurrentDate(string acceptedLanguage);
    Student UpdateName(Student studentupdated);
    Task<string> UploadImage(IFormFile file);
    Student Delete(int id);
}
