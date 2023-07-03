using System.Text.Json;
using System.Threading.Tasks;

class DatabaseIO : IDatabaseIO
{
    private string _epicGamesDbPath = "./Database/epicgamesdb.json";
    private string _emailsDbPath = "./Database/emails.json";

    public Task WriteEpicGamesDB(List<EpicGameInfoModel> models)
    {
        string json = JsonSerializer.Serialize(models);
        File.WriteAllText(_epicGamesDbPath, json);
        return Task.CompletedTask;
    }

    public Task<List<EpicGameInfoModel>> RetrieveFromEpicGamesDB()
    {
        if (!File.Exists(_epicGamesDbPath))
        {
            return Task.FromResult(new List<EpicGameInfoModel>());
        }

        string json = File.ReadAllText(_epicGamesDbPath);
        var result = JsonSerializer.Deserialize<List<EpicGameInfoModel>>(json);
        return Task.FromResult(result ?? new List<EpicGameInfoModel>());
    }

    public Task<EmailDBModel> RetrieveFromEmailsDB()
    {
        if (!File.Exists(_emailsDbPath))
        {
            return Task.FromResult(new EmailDBModel());
        }

        string json = File.ReadAllText(_emailsDbPath);
        var result = JsonSerializer.Deserialize<EmailDBModel>(json);
        return Task.FromResult(result ?? new EmailDBModel());
    }

    public Task DumpToEmailsDB() 
    {
        EmailDBModel data = new();
        data.emails = new List<string>();
        return WriteToEmailDB(data);
    }

    public Task WriteToEmailDB(EmailDBModel emails) {
        string json = JsonSerializer.Serialize(emails);
        File.WriteAllText(_emailsDbPath, json);
        return Task.CompletedTask;
    }

    public Task AddEmailToDb(string email) 
    {
        EmailDBModel data = RetrieveFromEmailsDB().Result;

        if(data.emails == null)
            data.emails = new List<string>();
        

        data.emails.Add(email);

        return WriteToEmailDB(data);
    }

    public Task RemoveEmailFromDB(string email) 
    {
        EmailDBModel data = RetrieveFromEmailsDB().Result;

        data.emails?.Remove(email);

        return WriteToEmailDB(data);
    }

    public Task<List<string>> GetEmails() 
    {
        EmailDBModel data = RetrieveFromEmailsDB().Result;
        return Task.FromResult(data.emails ?? new List<string>());
    }

    public Task<bool> EmailExists(string email) 
    {
        EmailDBModel data = RetrieveFromEmailsDB().Result;
        return Task.FromResult(data.emails?.Contains(email) ?? false);
    }
}
