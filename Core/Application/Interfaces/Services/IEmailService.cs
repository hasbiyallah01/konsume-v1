
using konsume_v1.Core.Domain.Entities;
using konsume_v1.Models;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailClient(string msg, string title, string email);
        Task<BaseResponse> SendNotificationToUserAsync(Profile profile);
        Task<bool> SendEmailAsync(MailRecieverDto model, MailRequests request);
        Task<BaseResponse> SendProfileUpdateNotificationAsync(Profile profile);
    }

}