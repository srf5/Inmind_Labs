using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using inmind_Lab1_week1;


public class StudentService : inmind_Lab1_week1.IStudentService
{
    private readonly List<Student> students = new List<Student>()
    {
        new Student() { Id = 1, name = "Alice", Email = "alice@example.com" },
        new Student() { Id = 2, name = "Bob", Email = "bob@example.com" },
        new Student() { Id = 3, name = "Charlie", Email = "charlie@example.com" },
        new Student() { Id = 4, name = "David", Email = "david@example.com" },
        new Student() { Id = 5, name = "Eve", Email = "eve@example.com" },
    };

    public List<Student> GetAllStudents()
    {
        return students;
    }

    public Student GetStudentById(long id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }

    public List<Student> GetStudentsByName(string name)
    {
        return students.Where(s => s.name.Contains(name)).ToList();
    }

    public string GetCurrentDate(string language)
    {
        var culture = GetCultureInfoFromLanguageCode(language);
        var formattedDate = DateTime.Now.ToString("D", culture);
        return formattedDate;
    }

    public Student UpdateName(Student studentupdated)
    {
        if (studentupdated == null)
        {
            throw new ArgumentNullException(nameof(studentupdated));
        }

        var student = students.FirstOrDefault(s => s.Id == studentupdated.Id);

        if (student == null)
        {
            throw new ArgumentException($"Student with ID {studentupdated.Id} not found");
        }

        student.name = studentupdated.name;
        return student;
    }

    Task<string> inmind_Lab1_week1.IStudentService.UploadImage(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public string UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Invalid file");
        }

        var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        return $"/uploads/{fileName}";
    }

    public Student Delete(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            throw new ArgumentException($"Student with ID {id} not found");
        }

        students.Remove(student);
        return student;
    }

    private static CultureInfo GetCultureInfoFromLanguageCode(string language)
    {
        var culture = CultureInfo.InvariantCulture;

        try
        {
            culture = CultureInfo.GetCultureInfo(language);
        }
        catch (CultureNotFoundException)
        {
            // log or handle the error in some way
        }

        return culture;
    }
}
