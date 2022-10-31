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
            string sqlQuery = "INSERT INTO Courses (Name, Credits, Hours) VALUES (@Name, @Credits, @Hours); ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new {
                    course.Name,
                    course.Credits,
                    course.Hours
                });
            }
            return course;
        }
        public async Task UpdateCourse(int idCourse, Course course)
        {
            string sqlQuery = "UPDATE Courses SET Name=@Name, Credits=@Credits, Hours=@Hours WHERE IdCourse=@idCourse";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new 
                {
                    idCourse,
                    course.Name,
                    course.Credits,
                    course.Hours
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

        public async Task<IEnumerable<Course>> GetAll()
        {
            string sqlQuery = "SELECT * FROM Courses";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<Course>(sqlQuery);
            }
        }

        public async Task<Course> GetById(int idCourse)
        {
            string sqlQuery = "SELECT * FROM Courses WHERE IdCourse = @CourseId";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Course>(sqlQuery, new { CourseId = idCourse });
            }
        }
    }
}