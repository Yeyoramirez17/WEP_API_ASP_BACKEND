using WEB_API.Interface;
using WEB_API.Models;
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
        public async Task<Course> CreateCourse(Course course)
        {
            string selectIdFacuty = "SELECT * FROM Faculties WHERE Name = @NameFaculty";
            string sqlQuery = $"INSERT INTO Courses (Name, Credits, Hours, IdFaculty) VALUES (@Name, @Credits, @Hours, ({selectIdFacuty})); ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new {
                    course.NameCourse,
                    course.Credits,
                    course.Hours,
                    course.Faculty.NameFaculty
                });
            }
            return course;
        }
        public async Task UpdateCourse(int idCourse, Course course)
        {
            string selectIdFacuty = "SELECT * FROM Faculties WHERE Name = @NameFaculty";
            string sqlQuery = $"UPDATE Courses SET Name=@Name, Credits=@Credits, Hours=@Hours,IdFaculty=({selectIdFacuty}) WHERE IdCourse=@idCourse";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new 
                {
                    idCourse,
                    course.NameCourse,
                    course.Credits,
                    course.Hours,
                    course.Faculty.NameFaculty
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
        public async Task<CourseDto> GetById(int idCourse)
        {
            string sqlQuery = "SELECT * FROM Courses WHERE IdCourse = @CourseId";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<CourseDto>(sqlQuery, new { CourseId = idCourse });
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

    }
}