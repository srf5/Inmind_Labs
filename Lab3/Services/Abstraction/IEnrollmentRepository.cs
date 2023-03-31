using Lab3.Entities;

namespace Lab3.Services.Abstraction;



public interface IEnrollmentRepository : IRepository<Enrollment>
{
    Task<Enrollment> GetEnrollmentByIdAsync(int id);
    Task<IEnumerable<Enrollment>> GetEnrollmentsAsync();
    Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
    void RemoveEnrollment(Enrollment enrollment);
}