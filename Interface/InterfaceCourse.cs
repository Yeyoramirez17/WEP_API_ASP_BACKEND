using WEB_API.Models;
using WEB_API.Models.EntitiesDto;
using WEB_API.Models.EntitiesDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB_API.Interface
{
    public interface InterfaceCourse
    {
        Task <IEnumerable<CourseDto>> GetAll();
        Task <IEnumerable<CourseCreateUpdate>> GetCourseAll();
        Task <CourseDto> GetById(int idCourse); 
        Task <CourseCreateUpdate> CourseGetById(int idCourse);
        Task <Course> GetCourseAndFaculty(int idCourse);
        Task <CourseCreateUpdate> CreateCourse(CourseCreateUpdate course);
        Task UpdateCourse(int idCourse, CourseCreateUpdate course);
        Task DeleteCourse(int idCourse);

    }
}