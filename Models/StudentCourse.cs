namespace WEB_API.Models;

public class StudentCourse{
    public int IdStudentCourses { get; set; }
    public int? IdStudent { get; set; }
    public int? IdCourse { get; set; }
    public string? IdentificationStudent { get; set; } = string.Empty;
    public string? LastNameStudent { get; set; } = string.Empty;
    public string? FirstNameStudent { get; set; } = string.Empty;
    public string? NameCourse { get; set; } = string.Empty;
}