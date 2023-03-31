using Lab3.Entities;
using Lab3.Services.Abstraction;

namespace Lab3.Services;

using System.Globalization;

using Microsoft.EntityFrameworkCore;

public class StudentService : IStudentService

    {
        private readonly MyDbContext _dbContext;
        private readonly IStudentRepository _studentRepository;

        public StudentService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public string GetCurrentDate(string language)
        {
            var culture = GetCultureInfoFromLanguageCode(language);
            var formattedDate = DateTime.Now.ToString("D", culture);
            return formattedDate;
        }
        private IFormatProvider GetCultureInfoFromLanguageCode(string language)
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

        public async Task<string> UploadImage(IFormFile file)
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

        public async Task<List<Student>> GetStudentByNameFilterAsync(string requestNameFilter)
        {
            return await _dbContext.Students
            .Where(s => s.name.Contains(requestNameFilter))
            .ToListAsync();
        }

        public async Task DeleteStudentByIdAsync(int studentId)
        {
            var student = await _dbContext.Students.FindAsync(studentId);
            
            if (student == null)
            {
                throw new ArgumentException($"Student with id {studentId} does not exist.");
            }
            
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _dbContext.Students.FindAsync(id);
        }

        public async Task<Student> UpdateStudentNameAsync(int id, string newName)
        {
            var student = await _dbContext.Students.FindAsync(id);
            
            if (student == null)
            {
                throw new ArgumentException($"Student with id {id} does not exist.");
            }

            student.name = newName;
            return student;
        }

        public async Task<object> AddStudentAsync(Student student)
        {
            _studentRepository.AddStudent(student);
            await _studentRepository.SaveChangesAsync();
            return new { student.Id };
        }

    }





