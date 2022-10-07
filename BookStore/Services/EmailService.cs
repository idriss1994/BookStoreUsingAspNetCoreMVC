using BookStore.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{

    public class EmailService : IEmailService
    {
        const string templatePath = @"EmailTemplate/{0}.html";
        readonly SMTPConfigModel _smtpConfigModel;

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfigModel = smtpConfig.Value;
        }

        public async Task SendTestEmail(UserEmailOptions userEmailOption)
        {
            userEmailOption.Subject = UpdatePlaceHolders("Hello {{UserName}}, This is test email subject from Book store web app",
                                                         userEmailOption.PlaceHolders);
            userEmailOption.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"), userEmailOption.PlaceHolders);

            await SendEmail(userEmailOption);
        }
        public async Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOption)
        {
            userEmailOption.Subject = UpdatePlaceHolders("Hello {{UserName}}, confirm your email id",
                                                         userEmailOption.PlaceHolders);
            userEmailOption.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirm"), userEmailOption.PlaceHolders);

            await SendEmail(userEmailOption);
        }

        async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mailMessage = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfigModel.SenderAddress, _smtpConfigModel.SenderDisplayName),
                IsBodyHtml = _smtpConfigModel.IsBodyHTML
            };
            foreach (string email in userEmailOptions.ToEmails)
            {
                mailMessage.To.Add(email);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfigModel.UserName, _smtpConfigModel.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfigModel.Host,
                Port = _smtpConfigModel.Port,
                EnableSsl = _smtpConfigModel.EnableSSL,
                UseDefaultCredentials = _smtpConfigModel.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mailMessage.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mailMessage);
        }
        string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
        string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> placeHolders)
        {
            if (!string.IsNullOrEmpty(text) && placeHolders != null)
            {
                foreach (KeyValuePair<string, string> placeHolder in placeHolders)
                {
                    if (text.Contains(placeHolder.Key))
                    {
                        text = text.Replace(placeHolder.Key, placeHolder.Value);
                    }
                }
            }
            return text;
        }
    }
}
