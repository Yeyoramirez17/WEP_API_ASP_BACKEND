namespace WEB_API.Models;

public class Course
{
    public int IdCourse { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Credits { get; set; }
    public int Hours { get; set; }

}