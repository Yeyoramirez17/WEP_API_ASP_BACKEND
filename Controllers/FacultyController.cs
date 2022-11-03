using WEB_API.Models;
using WEB_API.Repository;
using WEB_API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [Route("api/Faculties")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly InterfaceFaculty _iFacultyRepository;
        public FacultyController(InterfaceFaculty iFacultyRepository)
        {
            _iFacultyRepository = iFacultyRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Faculty>> GetAll()
        {
            return await _iFacultyRepository.GetAll();
        }
        [HttpGet("/{idFaculty}")]
        public async Task<ActionResult<Faculty>> GetById(int idFaculty)
        {
            return await _iFacultyRepository.GetById(idFaculty);
        }
        [HttpPost("/CreateFaculty")]
        public async Task<ActionResult<Faculty>> CreateFaculty(Faculty faculty)
        {
            var newFaculty = await _iFacultyRepository.CreateFaculty(faculty);
            return CreatedAtAction(nameof(GetAll), new {id = newFaculty.IdFaculty}, faculty);
        }
        [HttpPut("/UpdateFaculty/{idFaculty}")]
        public async Task<ActionResult<Faculty>> UpdateFaculty(int idFaculty, Faculty faculty)
        {
            if (idFaculty != faculty.IdFaculty)
                return BadRequest();
            
            await _iFacultyRepository.UpdateFaculty(idFaculty, faculty);
            return NoContent();
        }
        [HttpDelete("/DeleteFaculty/{idFaculty}")]
        public async Task<ActionResult> DeleteFaculty(int idFaculty)
        {
            var facultyToDelete = await _iFacultyRepository.GetById(idFaculty);

            if (facultyToDelete == null)
                return NotFound();
            
            await _iFacultyRepository.DeleteFaculty(facultyToDelete.IdFaculty);
            return NoContent();
        }
    }
}