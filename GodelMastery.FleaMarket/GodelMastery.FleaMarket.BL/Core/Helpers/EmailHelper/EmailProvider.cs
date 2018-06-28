using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.Messages;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper
{
    public class EmailProvider : IEmailProvider
    {
        private readonly IConfigProvider configProvider;

        public EmailProvider(IConfigProvider configProvider)
        {
            this.configProvider = configProvider ?? throw new ArgumentNullException(nameof(configProvider));
        }

        public async Task SendMessageAsync(Message message, IMessageContext context)
        {
            var sendMessageConfigModel = configProvider.ConfigurateSendMessageConfigModel();
            var fromEmail = new MailAddress(sendMessageConfigModel.Email, message.Title);
            var fromEmailPassword = sendMessageConfigModel.Password;
            var toEmail = new MailAddress(message.ToEmailAddress);
            using (var smtp = new SmtpClient())
            {
                smtp.Host = sendMessageConfigModel.SmtpHost;
                smtp.Port = sendMessageConfigModel.SmtpPort;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword);
                var mailMessage = new MailMessage(fromEmail, toEmail)
                {
                    Subject = message.Subject,
                    Body = message.GetBody(context),
                    IsBodyHtml = true,
                };
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
