using WEB_API.Repository;
using WEB_API.Interface;
using WEB_API.Models;
using WEB_API.Models.EntitiesDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB_API.Controllers
{
    [Route("api/Courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly InterfaceCourse _iCourseRepository;
        public CourseController(InterfaceCourse iCourseRepository)
        {
            _iCourseRepository = iCourseRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<CourseDto>> getAll()
        {
            return await _iCourseRepository.GetAll();
        }
        [HttpGet("GetCourseAll")]
        public async Task<IEnumerable<CourseCreateUpdate>> GetCourseAll()
        {
            return await _iCourseRepository.GetCourseAll();
        }
        [HttpGet("GetById/{idCourse}")]
        public async Task<ActionResult<CourseDto>> getId(int idCourse)
        {
            return await _iCourseRepository.GetById(idCourse);
        }
        [HttpGet("CourseGetById/{idCourse}")]
        public async Task<ActionResult<CourseCreateUpdate>> CourseGetBytId(int idCourse)
        {
            return await _iCourseRepository.CourseGetById(idCourse);
        }
        [HttpGet("GetCourseAndFaculty/{idCourse}")]
        public async Task<ActionResult<Course>> GetCourseAndFaculty(int idCourse)
        {
            return await _iCourseRepository.GetCourseAndFaculty(idCourse);
        }
        [HttpPost("CreateCourse/")]
        public async Task<ActionResult<CourseCreateUpdate>> CreateCourse(CourseCreateUpdate course)
        {
            CourseCreateUpdate newCourse = await _iCourseRepository.CreateCourse(course);
            return CreatedAtAction(nameof(GetCourseAll), new { id =  newCourse.IdCourse }, course);
        }
        [HttpPut("UpdateCourse/{idCourse}")]
        public async Task<ActionResult<CourseCreateUpdate>> UpdateCourse(int idCourse, CourseCreateUpdate course)
        {
            if (idCourse != course.IdCourse)
                return BadRequest();
            
            await _iCourseRepository.UpdateCourse(idCourse, course);
            return NoContent();
        }
        [HttpDelete("DeleteCourse/{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var courseToDelete = await _iCourseRepository.GetById(id);

            if (courseToDelete == null)
                return NotFound();
            
            await _iCourseRepository.DeleteCourse(courseToDelete.IdCourse);
            return NoContent();

        }
    }
}