using inmind_Lab1_week1.Entities;
using inmind_Lab1_week1.Services.Abstraction;
using MediatR;

namespace inmind_Lab1_week1;

public class GetStudentsQuery : IRequest<IEnumerable<Student>>
{
}

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, IEnumerable<Student>>
{
    private readonly IStudentService _studentService;

    public GetStudentsQueryHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<IEnumerable<Student>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _studentService.GetStudentsAsync();
    }
}

public class GetStudentByNameFilterQuery : IRequest<List<Student>>
{
    public string NameFilter { get; set; }
}

public class GetStudentByNameFilterQueryHandler : IRequestHandler<GetStudentByNameFilterQuery, List<Student>>
{
    private readonly IStudentService _studentService;

    public GetStudentByNameFilterQueryHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<List<Student>> Handle(GetStudentByNameFilterQuery request, CancellationToken cancellationToken)
    {
        return await _studentService.GetStudentByNameFilterAsync(request.NameFilter);
    }
}


public class GetCurrentDateQuery : IRequest<string>
{
}

public class GetCurrentDateQueryHandler : IRequestHandler<GetCurrentDateQuery, string>
{
    public async Task<string> Handle(GetCurrentDateQuery request, CancellationToken cancellationToken)
    {
        return DateTime.UtcNow.ToShortDateString();
    }
}

public class UpdateStudentNameCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UpdateStudentNameCommandHandler : IRequestHandler<UpdateStudentNameCommand>
{
    private readonly IStudentService _studentService;

    public UpdateStudentNameCommandHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task Handle(UpdateStudentNameCommand request, CancellationToken cancellationToken)
    {
        await _studentService.UpdateStudentNameAsync(request.Id, request.Name);
    }
}





public class UploadStudentImageCommand : IRequest<string>
{
    public int StudentId { get; set; }
    public IFormFile ImageFile { get; set; }
}

public class UploadStudentImageCommandHandler : IRequestHandler<UploadStudentImageCommand, string>
{
    private readonly IStudentService _studentService;

    public UploadStudentImageCommandHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<string> Handle(UploadStudentImageCommand request, CancellationToken cancellationToken)
    {
        var result = await _studentService.UploadImage(request.ImageFile);
        return result;
    }
}
public class DeleteStudentByIdCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteStudentByIdCommandHandler : IRequestHandler<DeleteStudentByIdCommand, Unit>
{
    private readonly IStudentService _studentService;

    public DeleteStudentByIdCommandHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<Unit> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
    {
        await _studentService.DeleteStudentByIdAsync(request.Id);
        return Unit.Value;
    }
}
public class GetAllStudentsQuery : IRequest<IEnumerable<Student>>
{
}

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<Student>>
{
    private readonly IStudentService _studentService;

    public GetAllStudentsQueryHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<IEnumerable<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentService.GetStudentsAsync();
        return students;
    }
}


