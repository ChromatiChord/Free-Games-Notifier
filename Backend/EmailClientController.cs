using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

class EmailClientController
{
    public void SendEmail(List<EpicGameInfo> games)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress("Free Games Reminder", "freegamesreminder@gmail.com"));
        email.To.Add(new MailboxAddress("Receiver Name", "callumward56@gmail.com"));

        email.Subject = "Free Game Update";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { 
            Text = $@"<div>Hi Callum,<br/>
            Here's a list of all the new cool games that are free:<br/>
            <ul>
            <li>{games[0].Name}</li>
            <li>{games[1].Name}</li>
            </ul>
            Regards, Free Games Reminder </div>"
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