using System.Threading.Tasks;

namespace SistemaControleEmpresarial.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
