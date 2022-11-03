namespace WEB_API.Models;

public class Course
{
    public int IdCourse { get; set; }
    public string NameCourse { get; set; } = string.Empty;
    public int Credits { get; set; }
    public int Hours { get; set; }
    public Faculty Faculty { get; set; } = new Faculty();

}