using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface InterfaceStudent
    {
        Task<IEnumerable<Student>> getAll();
        Task<Student> getId(int id);
        Task<Student> createStudent(Student student);
        Task updateStudent(Student student);
        Task deleteStudent(int id);
    }
}
