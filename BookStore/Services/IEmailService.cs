using BookStore.Models;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOption);
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOption);
        Task SendEmailForForgotPassword(UserEmailOptions userEmailOption);
    }
}