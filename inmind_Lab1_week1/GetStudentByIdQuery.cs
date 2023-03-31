using inmind_Lab1_week1.Entities;
using MediatR;

namespace inmind_Lab1_week1;

public class GetStudentByIdQuery : IRequest<Student>
{
    public int Id { get; set; }
}