using WEB_API.Models;
using WEB_API.Models.EntitiesDto;
using WEB_API.Repository;
using WEB_API.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly InterfaceStudent _iStudentRepository;

        public StudentController(InterfaceStudent iStudentTepository)
        {
            _iStudentRepository = iStudentTepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> getAll()
        {
            return await _iStudentRepository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> getId(int id)
        {
            return await _iStudentRepository.GetId(id);
        }
        [HttpGet("StudentAndCourses/{idStudent}")]
        public async Task<ActionResult<Student>> getStudentAndCoursesById(int idStudent)
        {
            return await _iStudentRepository.GetStudentAndCoursesById(idStudent);
        }
        [HttpPost]
        public async Task<ActionResult<StudentForCreateDto>> CreateStudent([FromBody] StudentForCreateDto student)
        {
           StudentForCreateDto newStudent = await _iStudentRepository.CreateStudent(student);
           return CreatedAtAction(nameof(getAll), new { id = newStudent.IdStudent}, newStudent);
        }
        [HttpPut("{idStudent}")]
        public async Task<ActionResult<StudentForUpdateDto>> UpdateStudent(int idStudent, [FromBody] StudentForUpdateDto student)
        {
           if (idStudent != student.IdStudent)
           {
            return BadRequest();
           }

           await _iStudentRepository.UpdateStudent(idStudent, student);

           return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var studentToDelete = await _iStudentRepository.GetId(id);

            if (studentToDelete == null)
                return NotFound();
            
            await _iStudentRepository.DeleteStudent(studentToDelete.IdStudent);
            return NoContent();
        }
    }   
}