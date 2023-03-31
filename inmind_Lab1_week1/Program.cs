using System.Globalization;
using inmind_Lab1_week1;
using inmind_Lab1_week1.Services;
using inmind_Lab1_week1.Services.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
;


builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddDbContext<ApplicationDbContext>(options=> options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=postgres;Password=Idkwhattoput979125!?;")
    ,ServiceLifetime.Scoped);

// builder.Services.AddMediatR(typeof(Program).Assembly); 
// Rest of the code...

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(); // This line is required to serve static files, including images

app.UseHttpsRedirection();

app.MapControllers();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();


// Create the host builder with web root path
var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseWebRoot("wwwroot");
    });

await hostBuilder.Build().RunAsync();
