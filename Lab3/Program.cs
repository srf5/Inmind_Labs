using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Lab3;
using Lab3.Infrastructure.Repositories;
using Lab3.Services;
using Lab3.Services.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

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



// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Test",
            ValidAudience = "audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b'|\xe7\xbfU3`\xc4\xec\xa7\xa9zf:}\xb5\xc7\xb9\x139^3@Dv'"))
        };
        

    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CampusPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("campus");
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("StudentOnly", policy =>
        policy.RequireRole("Student"));
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TeacherOnly", policy =>
        policy.RequireRole("Teacher"));
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();