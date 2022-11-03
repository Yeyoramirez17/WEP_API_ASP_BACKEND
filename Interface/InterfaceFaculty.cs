using WEB_API.Models;

namespace WEB_API.Interface
{
    public interface InterfaceFaculty
    {
        Task<IEnumerable<Faculty>> GetAll();
        Task<Faculty> GetById(int id);
        Task<Faculty> CreateFaculty(Faculty faculty);
        Task UpdateFaculty(int idFaculty, Faculty faculty);
        Task DeleteFaculty(int idFaculty);
    }
}