using WEB_API.Interface;
using WEB_API.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace WEB_API.Repository
{
    public class FacultyRepository : InterfaceFaculty
    {
        private readonly string _connectionString;
        public FacultyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task<Faculty> CreateFaculty(Faculty faculty)
        {
            string sqlQuery = "INSERT INTO Faculties (NameFaculty, Address, Phone, Email) VALUES (@NameFaculty, @Address, @Phone, @Email); ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new {
                    faculty.NameFaculty,
                    faculty.Address,
                    faculty.Phone,
                    faculty.Email
                });
            }
            return faculty;
        }
        public async Task UpdateFaculty(int idFaculty, Faculty faculty)
        {
            string sqlQuery = "UPDATE Faculties SET NameFaculty=@NameFaculty, Address=@Address, Phone=@Phone, Email=@Email WHERE IdFaculty=@idFaculty; ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new {
                    idFaculty,
                    faculty.NameFaculty,
                    faculty.Address,
                    faculty.Phone,
                    faculty.Email
                });
            }
        }
        public async Task DeleteFaculty(int idFaculty)
        {
            string sqlQuery = $"DELETE FROM Faculties WHERE IdFaculty = {idFaculty}";
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery);
            }
        }

        public async Task<IEnumerable<Faculty>> GetAll()
        {
            string sqlQuery = "SELECT * FROM Faculties; ";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<Faculty>(sqlQuery);
            }
        }

        public async Task<Faculty> GetById(int id)
        {
            string sqlQuery = "SELECT * FROM Faculties WHERE IdFaculty = @idFaculty";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Faculty>(sqlQuery, new { idFaculty = id });
            }
        }

    }
}