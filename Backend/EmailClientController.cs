using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

class EmailClientController
{
    public void SendEmail(string emailBodyText, string recipient)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress("Free Games Reminder", "freegamesreminder@gmail.com"));
        email.To.Add(new MailboxAddress("Receiver Name", recipient));

        email.Subject = "Free Game Update";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { 
            Text = emailBodyText
        };

        using (var smtp = new SmtpClient())
        {
            smtp.Connect("smtp.gmail.com", 587, false);

            // Note: only needed if the SMTP server requires authentication
            smtp.Authenticate("freegamesreminder@gmail.com", Environment.GetEnvironmentVariable("GAMESREMINDER_APP_KEY"));

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}