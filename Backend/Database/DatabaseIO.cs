using System.Text.Json;

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
        var result = JsonSerializer.Deserialize<List<EpicGameInfoModel>>(json);
        return result ?? new List<EpicGameInfoModel>();
    }

    public EmailDBModel RetrieveFromEmailsDB()
    {
        if (!File.Exists(_emailsDbPath))
        {
            return new EmailDBModel();
        }

        string json = File.ReadAllText(_emailsDbPath);
        var result = JsonSerializer.Deserialize<EmailDBModel>(json);
        return result ?? new EmailDBModel();
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

        if(data.emails == null)
            data.emails = new List<string>();

        data.emails.Add(email);

        WriteToEmailDB(data);
    }

    public void RemoveEmailFromDB(string email) {
        EmailDBModel data = RetrieveFromEmailsDB();

        data.emails?.Remove(email);

        WriteToEmailDB(data);
    }

    public List<string> GetEmails() {
        EmailDBModel data = RetrieveFromEmailsDB();

        return data.emails ?? new List<string>();
    }
}
