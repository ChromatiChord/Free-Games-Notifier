using Google.Cloud.Firestore;
using System.Threading.Tasks;
using System.Linq;

class GCPDatabaseIO : IDatabaseIO
{
    private FirestoreDb _db;

    public GCPDatabaseIO()
    {
        string projectId = "free-games-reminder-database";
        _db = FirestoreDb.Create(projectId);
    }

    public async Task WriteEpicGamesDB(List<EpicGameInfoModel> models)
    {
        CollectionReference collectionReference = _db.Collection("epicGames");
        foreach (var model in models)
        {
            await collectionReference.AddAsync(model);
        }
    }

    public async Task<List<EpicGameInfoModel>> RetrieveFromEpicGamesDB()
    {
        Query query = _db.Collection("epicGames");
        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        return snapshot.Documents
            .Select(document => document.ConvertTo<EpicGameInfoModel>())
            .ToList();
    }

    public async Task<EmailDBModel> RetrieveFromEmailsDB()
    {
        DocumentReference docRef = _db.Collection("emails").Document("emailData");
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            return snapshot.ConvertTo<EmailDBModel>();
        }
        else
        {
            return new EmailDBModel();
        }
    }

    public async Task WriteToEmailDB(EmailDBModel emails)
    {
        DocumentReference docRef = _db.Collection("emails").Document("emailData");
        await docRef.SetAsync(emails);
    }

    public async Task AddEmailToDb(string email) 
    {
        var data = await RetrieveFromEmailsDB();

        if(data.emails == null)
            data.emails = new List<string>();

        data.emails.Add(email);

        await WriteToEmailDB(data);
    }

    public async Task RemoveEmailFromDB(string email) 
    {
        var data = await RetrieveFromEmailsDB();

        data.emails?.Remove(email);

        await WriteToEmailDB(data);
    }

    public async Task<List<string>> GetEmails() 
    {
        var data = await RetrieveFromEmailsDB();

        return data.emails ?? new List<string>();
    }

    public async Task DumpToEmailsDB() 
    {
        EmailDBModel data = new EmailDBModel { emails = new List<string>() };
        await WriteToEmailDB(data);
    }

    public Task<bool> EmailExists(string email) 
    {
        var data = RetrieveFromEmailsDB().Result;

        return Task.FromResult(data.emails?.Contains(email) ?? false);
    }
}
