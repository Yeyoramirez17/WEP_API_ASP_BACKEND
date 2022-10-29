using WEB_API.Models;
using WEB_API.Models.EntitiesDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB_API.Interface
{
    public interface InterfaceStudent
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetId(int id);
        Task<Student> GetStudentAndCoursesById(int idStudent);
        Task<StudentForCreateDto> CreateStudent(StudentForCreateDto student);
        Task UpdateStudent(int idStudent, StudentForUpdateDto student);
        Task DeleteStudent(int id);
    }
}
