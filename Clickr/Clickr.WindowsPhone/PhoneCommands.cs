
using System;
using Windows.ApplicationModel.Email;

namespace Clickr
{
    public class PhoneCommands : Commands
    {
        public override async void Mail()
        {
            var sendTo = new EmailRecipient()
            {
                Address = Email
            };
            var mail = new EmailMessage { Subject = "Clickr Counter feedback" };
            mail.To.Add(sendTo);

            await EmailManager.ShowComposeNewEmailAsync(mail);
        }
    }
}
