using smartStoreApi.Models.Request;
using System.Threading.Tasks;

namespace smartStoreApi.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}