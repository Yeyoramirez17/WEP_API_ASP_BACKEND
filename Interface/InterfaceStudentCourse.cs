using WEB_API.Models;
namespace WEB_API.Interface
{
    public interface InterfaceStudentCourse
    {
        Task<IEnumerable<StudentCourse>> GetAll();
        Task<IEnumerable<StudentCourse>> GetAllData();
        Task<StudentCourse> CreateStudentCourse(StudentCourse studentCourse);
    }
}