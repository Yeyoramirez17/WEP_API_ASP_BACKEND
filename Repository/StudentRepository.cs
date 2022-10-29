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
    public class StudentRepository : InterfaceStudent
    {
        private readonly string _connectionString;
        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task<StudentForCreateDto> CreateStudent(StudentForCreateDto student)
        {
            string sqlQuery = "INSERT INTO Students(LastName, FirstName, Identification, BirthDate, Age, Phone, Email) " + 
                                  "VALUES (@LastName, @FirstName, @Identification, @BirthDate, @Age, @Phone, @Email); ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    student.LastName,
                    student.FirstName,
                    student.Identification,
                    student.BirthDate,
                    student.Age,
                    student.Phone,
                    student.Email
                });
            }
            return student;
        }
        public async Task UpdateStudent(int idStudent, StudentForUpdateDto student)
        {
            string sqlQuery = "UPDATE Students SET LastName=@LastName, FirstName=@FirstName, Identification=@Identification, BirthDate=@BirthDate, " + 
                                "Age=@Age, Phone=@Phone, Email=@Email WHERE IdStudent = @idStudent; ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    idStudent,
                    student.FirstName,
                    student.LastName,
                    student.Identification,
                    student.BirthDate,
                    student.Age,
                    student.Phone,
                    student.Email
                });
            }
        }
        public async Task DeleteStudent(int id)
        {
            var sqlQuery = $"DELETE FROM Students WHERE IdStudent = {id}";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery);
            }
        }
        public async Task<IEnumerable<Student>> GetAll()
        {
            string sqlQuery = "SELECT * FROM Students";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<Student>(sqlQuery);
            }
        }
        public async Task<Student> GetId(int id)
        {
            string sqlQuery = "SELECT * FROM Students WHERE IdStudent = @StudentId";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Student>(sqlQuery, new { StudentId = id });
            }
        }

        public async Task<Student> GetStudentAndCoursesById(int idStudent)
        {
            string sqlQuery = "SELECT * FROM Students WHERE IdStudent = @idStudent;" + 
                                "SELECT c.IdCourse, c.Name, c.Credits, c.Hours FROM Courses c JOIN Students_Course sc ON c.IdCourse = sc.IdCourse WHERE IdStudent = @idStudent; ";
            
            using (var connection = new SqliteConnection(_connectionString))
            using (var multi = await connection.QueryMultipleAsync(sqlQuery, new { idStudent }))
            {
                Student student = await multi.ReadSingleOrDefaultAsync<Student>();
                if (student != null)
                    student.CourseList = (await multi.ReadAsync<Course>()).ToList();
                
                return student;
            }
        }
    }
}