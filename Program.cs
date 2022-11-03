using WEB_API.Repository;
using WEB_API.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<InterfaceStudent, StudentRepository>();
builder.Services.AddScoped<InterfaceCourse, CourseRepository>();
builder.Services.AddScoped<InterfaceFaculty, FacultyRepository>();
builder.Services.AddScoped<InterfaceStudentCourse, StudentCourseRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
