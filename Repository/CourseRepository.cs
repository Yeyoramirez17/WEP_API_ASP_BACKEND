using WEB_API.Interface;
using WEB_API.Models;
using WEB_API.Models.EntitiesDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;

namespace WEB_API.Repository 
{
    public class CourseRepository : InterfaceCourse
    {
        private readonly string _connectionString;
        public CourseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task<CourseCreateUpdate> CreateCourse(CourseCreateUpdate course)
        {
            string selectIdFacuty = $"SELECT f.IdFaculty FROM Faculties f WHERE NameFaculty = @NameFaculty";

            string sqlQuery = $"INSERT INTO Courses (NameCourse, Credits, Hours, IdFaculty) VALUES (@NameCourse, @Credits, @Hours, ({selectIdFacuty})); ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new {
                    course.NameCourse,
                    course.Credits,
                    course.Hours,
                    course.NameFaculty
                });
            }
            return course;
        }
        public async Task UpdateCourse(int idCourse, CourseCreateUpdate course)
        {
            string selectIdFacuty = "SELECT IdFaculty FROM Faculties WHERE NameFaculty = @NameFaculty";
            string sqlQuery = $"UPDATE Courses SET NameCourse=@NameCourse, Credits=@Credits, Hours=@Hours,IdFaculty=({selectIdFacuty}) WHERE IdCourse=@idCourse";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new 
                {
                    idCourse,
                    course.NameCourse,
                    course.Credits,
                    course.Hours,
                    course.NameFaculty
                });
            }
        }
        public async Task DeleteCourse(int idCourse)
        {
            string sqlQuery = $"DELETE FROM Courses WHERE IdCourse = {idCourse}";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery);
            }
        }

        public async Task<IEnumerable<CourseDto>> GetAll()
        {
            string sqlQuery = "SELECT * FROM Courses";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<CourseDto>(sqlQuery);
            }
        }
        public async Task<IEnumerable<CourseCreateUpdate>> GetCourseAll()
        {
            string sqlQuery = "SELECT c.IdCourse, c.NameCourse, c.Credits, c.Hours, f.NameFaculty" +
                                    "FROM Courses c JOIN Faculties f ON c.IdFaculty = f.IdFaculty";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<CourseCreateUpdate>(sqlQuery);
            }
        }
        public async Task<CourseDto> GetById(int idCourse)
        {
            string sqlQuery = "SELECT * FROM Courses WHERE IdCourse = @CourseId";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<CourseDto>(sqlQuery, new { CourseId = idCourse });
            }
        }
        public async Task <CourseCreateUpdate> CourseGetById(int idCourse){
            string sqlQuery = "SELECT c.IdCourse, c.NameCourse, c.Credits, c.Hours, " + 
            "f.NameFaculty AS NameFaculty FROM Courses c JOIN Faculties f ON c.IdFaculty = f.IdFaculty WHERE IdCourse = @CourseId";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<CourseCreateUpdate>(sqlQuery, new { CourseId = idCourse });
            }
        }
        public async Task<Course> GetCourseAndFaculty(int idCourse)
        {
            var sqlQuery = "SELECT * FROM Courses WHERE IdCourse = @CourseId";
            using (var connection = new SqliteConnection(_connectionString))
            {
                var courseDto = await connection.QueryFirstOrDefaultAsync<CourseDto>(sqlQuery, new { CourseId = idCourse });
                var course = new Course();
                course.IdCourse = courseDto.IdCourse;
                course.NameCourse = courseDto.NameCourse;
                course.Credits = courseDto.Credits;
                course.Hours = courseDto.Hours;
                course.Faculty = await GetFacultyById(courseDto.IdFaculty);
                return course;
            }
        }
        private async Task<Faculty> GetFacultyById(int id)
        {
            string sqlQuery = "SELECT * FROM Faculties WHERE IdFaculty = @idFaculty";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Faculty>(sqlQuery, new { idFaculty = id });
            }
        }
        private async Task<Faculty> GetFacultyByNameFaculty(string nameFaculty)
        {
            string sqlQuery = "SELECT * FROM Faculties WHERE NameFaculty = @NameFaculty";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Faculty>(sqlQuery, new { NameFaculty = nameFaculty });
            }
        }
    }
}