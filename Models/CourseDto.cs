namespace WEB_API.Models;

public class CourseDto
{
    public int IdCourse { get; set; }
    public string NameCourse { get; set; } = string.Empty;
    public int Credits { get; set; }
    public int Hours { get; set; }
    public int IdFaculty { get; set; }
}