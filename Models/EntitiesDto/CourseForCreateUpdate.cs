namespace WEB_API.Models.EntitiesDto
{
    public class CourseCreateUpdate
    {
        public int IdCourse { get; set; }
        public string NameCourse { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int Hours { get; set; }
        public string NameFaculty { get; set; } = string.Empty;
    }
}