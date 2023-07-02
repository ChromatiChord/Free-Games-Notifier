using System;

using MailKit.Net.Smtp;
using MimeKit;

class EmailSender
{
    public void SendEmail(string emailBodyText, string recipient)
    {
        string emailAppKey = Environment.GetEnvironmentVariable("GAMESREMINDER_APP_KEY") ?? "";
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
            smtp.Authenticate("freegamesreminder@gmail.com", emailAppKey);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}