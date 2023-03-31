using Lab3.Models;

namespace Lab3.Services.Abstraction;

using System.Collections.Generic;
using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<IEnumerable<CourseDto>> GetCoursesByTeacherIdAsync(int teacherId);
        Task<int> AddCourseAsync(CourseDto courseDto);
        Task UpdateCourseAsync(int id, CourseDto courseDto);
        Task RemoveCourseAsync(int id);
    }

