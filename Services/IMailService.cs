using Web_Application_Desafio_Pulse_It.Models;

namespace Web_Application_Desafio_Pulse_It.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
