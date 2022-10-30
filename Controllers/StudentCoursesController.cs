using WEB_API.Models;
using WEB_API.Models.EntitiesDto;
using WEB_API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCourseController : ControllerBase
    {
        private readonly InterfaceStudentCourse _iStudentCourseRepository;

        public StudentCourseController(InterfaceStudentCourse iStudentCourseRepository)
        {
            _iStudentCourseRepository = iStudentCourseRepository;
        }
        [HttpGet("/getStudentsAndCourses")]
        public async Task<IEnumerable<StudentCourse>> GetAll()
        {
            return await _iStudentCourseRepository.GetAll();
        }
        [HttpGet]
        public async Task<IEnumerable<StudentCourse>> GetAllData()
        {
            return await _iStudentCourseRepository.GetAllData();
        }
        [HttpPost]
        public async Task<ActionResult<StudentCourse>> CreateStudentCourse([FromBody] StudentCourse studentCourse)
        {
            var newStudentCourse = await _iStudentCourseRepository.CreateStudentCourse(studentCourse);
            return CreatedAtAction(nameof(GetAllData), new { idStudent = studentCourse.IdStudent }, newStudentCourse);
        }
    }
}