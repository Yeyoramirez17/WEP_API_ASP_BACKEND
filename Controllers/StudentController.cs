using API.Models;
using API.Repository;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers{

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
            return await _iStudentRepository.getAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> getId(int id)
        {
            return await _iStudentRepository.getId(id);
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
           Student newStudent = await _iStudentRepository.createStudent(student);
           return CreatedAtAction(nameof(getAll), new { id = newStudent.Id}, newStudent);
        }
        [HttpPut]
        public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] Student student)
        {
           if (id != student.Id)
           {
            return BadRequest();
           }

           await _iStudentRepository.updateStudent(student);

           return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var studentToDelete = await _iStudentRepository.getId(id);

            if (studentToDelete == null)
                return NotFound();
            
            await _iStudentRepository.deleteStudent(studentToDelete.Id);
            return NoContent();
        }
    }   
}