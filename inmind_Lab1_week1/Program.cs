using inmind_Lab1_week1;
using inmind_Lab1_week1.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
;


builder.Services.AddScoped<IStudentService, StudentService>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();

// Create the host builder with web root path
var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseWebRoot("wwwroot");
        webBuilder.UseStartup<Startup>();
    });

await hostBuilder.Build().RunAsync();


    
