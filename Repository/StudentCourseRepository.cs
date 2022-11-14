using WEB_API.Interface;
using WEB_API.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace WEB_API.Repository
{
    public class StudentCourseRepository : InterfaceStudentCourse
    {
        private readonly string _connectionString;
        public StudentCourseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task<StudentCourse> CreateStudentCourse(StudentCourse studentCourse)
        {
            string selectStudent = "SELECT IdStudent FROM Students WHERE Identification = @IdentificationStudent ";
            string selectCourse = "SELECT IdCourse FROM Courses WHERE NameCourse = @NameCourse";

            string sqlQuery = $"INSERT INTO Students_Courses (IdStudent, IdCourse) VALUES (({selectStudent}), ({selectCourse}));";

            using(var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new 
                { 
                    studentCourse.IdentificationStudent,
                    studentCourse.NameCourse
                });
            }
            return studentCourse;
        }
        public async Task<IEnumerable<StudentCourse>> GetAll()
        {
            string sqlQuery = "SELECT * FROM Students_Courses";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<StudentCourse>(sqlQuery);
            }
        }
        public async Task<IEnumerable<StudentCourse>> GetAllData()
        {
            string sqlQuery = "SELECT sc.IdStudent AS IdStudent, sc.IdCourse AS IdCourse, s.Identification AS " + 
                                "IdentificationStudent, s.LastName AS LastNameStudent,s.FirstName AS FirstNameStudent, " + 
                                    "c.NameCourse FROM Students_Courses sc JOIN Students s ON sc.IdStudent = s.IdStudent " +
                                        "JOIN Courses c ON  sc.IdCourse = c.IdCourse; ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<StudentCourse>(sqlQuery);
            }
        }
    }
}