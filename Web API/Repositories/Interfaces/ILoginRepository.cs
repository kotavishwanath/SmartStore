using System.Threading.Tasks;

namespace smartStoreApi.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<int> UpdatePasswordAsync(int id, string password);
    }
}