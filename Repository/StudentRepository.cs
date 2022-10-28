using API.Interface;
using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;

namespace API.Repository
{
    public class StudentRepository : InterfaceStudent
    {
        private readonly string _connectionString;
        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task<Student> createStudent(Student student)
        {
            string sqlQuery = "INSERT INTO Students(nombre, identificacion, edad, telefono) VALUES (@Nombre, @Identificacion, @Edad, @Telefono); ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    student.Nombre,
                    student.Identificacion,
                    student.Edad,
                    student.Telefono
                });
            }
            return student;
        }
        public async Task deleteStudent(int id)
        {
            var sqlQuery = $"DELETE from Students WHERE Id={id}";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery);
            }
        }
        public async Task<IEnumerable<Student>> getAll()
        {
            string sqlQuery = "SELECT * FROM Students";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<Student>(sqlQuery);
            }
        }
        public async Task<Student> getId(int id)
        {
            string sqlQuery = "SELECT * FROM Students WHERE id = @StudentId";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Student>(sqlQuery, new { StudentId = id });
            }
        }
        public async Task updateStudent(Student student)
        {
            string sqlQuery = "UPDATE Students SET nombre = @Nombre, identificacion = @Identificacion, edad = @Edad, telefono = @Telefono WHERE id = @Id; ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    student.Id,
                    student.Nombre,
                    student.Identificacion,
                    student.Edad,
                    student.Telefono
                });
            }
        }
    }
}