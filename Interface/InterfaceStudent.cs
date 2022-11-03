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
        Task<StudentForCreateAndUpdateDto> CreateStudent(StudentForCreateAndUpdateDto student);
        Task UpdateStudent(int idStudent, StudentForCreateAndUpdateDto student);
        Task DeleteStudent(int id);
    }
}
