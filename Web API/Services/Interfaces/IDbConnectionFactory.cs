using System.Data;

namespace smartStoreApi.Services.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}