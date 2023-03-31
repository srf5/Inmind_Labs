using Lab3.Entities;
using Lab3.Exceptions;
using Lab3.Models;
using Lab3.Services.Abstraction;

namespace Lab3.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseDto> GetCourseByIdAsync(int id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        return MapToDto(course);
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetCoursesAsync();
        return courses.Select(MapToDto);
    }

    public async Task<IEnumerable<CourseDto>> GetCoursesByTeacherIdAsync(int teacherId)
    {
        var courses = await _courseRepository.GetCoursesByTeacherIdAsync(teacherId);
        return courses.Select(MapToDto);
    }

    public async Task<int> AddCourseAsync(CourseDto courseDto)
    {
        var course = MapToEntity(courseDto);
        _courseRepository.Add(course);
        await _courseRepository.SaveChangesAsync();
        return course.Id;
    }

    public async Task UpdateCourseAsync(int id, CourseDto courseDto)
    {
        var existingCourse = await _courseRepository.GetCourseByIdAsync(id);
        if (existingCourse == null)
        {
            throw new NotFoundException("Course not found.");
        }

        MapToEntity(courseDto, existingCourse);
        _courseRepository.Update(existingCourse);
        await _courseRepository.SaveChangesAsync();
    }

    public async Task RemoveCourseAsync(int id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        if (course == null)
        {
            throw new NotFoundException("Course not found.");
        }

        _courseRepository.Remove(course);
        await _courseRepository.SaveChangesAsync();
    }

    private static CourseDto MapToDto(Course course)
    {
        return new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            MaxStudents = course.MaxStudents,
            EnrollmentStartDate = course.EnrollmentStartDate,
            EnrollmentEndDate = course.EnrollmentEndDate
        };
    }

    private static void MapToEntity(CourseDto courseDto, Course course)
    {
        course.Name = courseDto.Name;
        course.Description = courseDto.Description;
        course.MaxStudents = courseDto.MaxStudents;
        course.EnrollmentStartDate = courseDto.EnrollmentStartDate;
        course.EnrollmentEndDate = courseDto.EnrollmentEndDate;
    }

    private static Course MapToEntity(CourseDto courseDto)
    {
        return new Course
        {
            Name = courseDto.Name,
            Description = courseDto.Description,
            MaxStudents = courseDto.MaxStudents,
            EnrollmentStartDate = courseDto.EnrollmentStartDate,
            EnrollmentEndDate = courseDto.EnrollmentEndDate
        };
    }
}
