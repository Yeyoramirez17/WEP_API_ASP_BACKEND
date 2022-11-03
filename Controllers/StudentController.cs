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

        [HttpGet("/GetAll/")]
        public async Task<IEnumerable<Student>> getAll()
        {
            return await _iStudentRepository.GetAll();
        }
        [HttpGet("/StudentById/{id}")]
        public async Task<ActionResult<Student>> getId(int id)
        {
            return await _iStudentRepository.GetId(id);
        }
        
        [HttpGet("/StudentAndCourses/{idStudent}")]
        public async Task<ActionResult<Student>> getStudentAndCoursesById(int idStudent)
        {
            return await _iStudentRepository.GetStudentAndCoursesById(idStudent);
        }
        [HttpPost("/CreateStudent/")]
        public async Task<ActionResult<StudentForCreateAndUpdateDto>> CreateStudent([FromBody] StudentForCreateAndUpdateDto student)
        {
           StudentForCreateAndUpdateDto newStudent = await _iStudentRepository.CreateStudent(student);
           return CreatedAtAction(nameof(getAll), new { id = newStudent.IdStudent}, newStudent);
        }
        [HttpPut("/UpdateStudent/{idStudent}")]
        public async Task<ActionResult<StudentForCreateAndUpdateDto>> UpdateStudent(int idStudent, [FromBody] StudentForCreateAndUpdateDto student)
        {
           if (idStudent != student.IdStudent)
           {
            return BadRequest();
           }

           await _iStudentRepository.UpdateStudent(idStudent, student);

           return NoContent();
        }
        [HttpDelete("/Delete/{id}")]
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