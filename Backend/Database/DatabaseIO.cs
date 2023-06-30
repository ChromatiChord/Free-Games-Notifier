using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

class DatabaseIO {
    private string _epicGamesDbPath = "./Database/epicgamesdb.json";
    private string _emailsDbPath = "./Database/emails.json";

    public void WriteEpicGamesDB(List<EpicGameInfoModel> models)
    {
        string json = JsonSerializer.Serialize(models);
        File.WriteAllText(_epicGamesDbPath, json);
    }

    public List<EpicGameInfoModel> RetrieveFromEpicGamesDB()
    {
        if (!File.Exists(_epicGamesDbPath))
        {
            return new List<EpicGameInfoModel>();
        }

        string json = File.ReadAllText(_epicGamesDbPath);
        return JsonSerializer.Deserialize<List<EpicGameInfoModel>>(json);
    }

    public EmailDBModel RetrieveFromEmailsDB()
    {
        if (!File.Exists(_emailsDbPath))
        {
            return new EmailDBModel();
        }

        string json = File.ReadAllText(_emailsDbPath);
        return JsonSerializer.Deserialize<EmailDBModel>(json);
    }

    public void DumpToEmailsDB() {
        EmailDBModel data = new();
        data.emails = new List<string>();
        WriteToEmailDB(data);
    }

    public void WriteToEmailDB(EmailDBModel emails) {
        string json = JsonSerializer.Serialize(emails);
        File.WriteAllText(_emailsDbPath, json);
    }

    public void AddEmailToDb(string email) 
    {
        EmailDBModel data = RetrieveFromEmailsDB();

        data.emails.Add(email);

        WriteToEmailDB(data);
    }

    public void RemoveEmailFromDB(string email) {
        EmailDBModel data = RetrieveFromEmailsDB();

        data.emails.Remove(email);

        WriteToEmailDB(data);
    }

    public List<string> GetEmails() {
        EmailDBModel data = RetrieveFromEmailsDB();

        return data.emails;
    }
}