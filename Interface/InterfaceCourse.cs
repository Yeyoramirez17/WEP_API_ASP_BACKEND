using WEB_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB_API.Interface
{
    public interface InterfaceCourse
    {
        Task <IEnumerable<Course>> GetAll();
        Task <Course> GetById(int idCourse);
        Task <Course> CreateCourse(Course course);
        Task UpdateCourse(int idCourse, Course course);
        Task DeleteCourse(int idCourse);

    }
}