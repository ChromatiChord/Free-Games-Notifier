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

    public async Task<List<UserModel>> RetrieveFromUsersDB()
    {
        Query query = _db.Collection("users");
        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        if (snapshot.Documents.Any())
        {
            return snapshot.Documents
                .Select(document => document.ConvertTo<UserModel>())
                .ToList();
        }
        else
        {
            return new List<UserModel>();
        }
    }

    public async Task WriteToUserDB(List<UserModel> users)
    {
        CollectionReference collectionReference = _db.Collection("users");
        foreach (var user in users)
        {
            await collectionReference.AddAsync(user);
        }
    }

    public async Task AddUserToDb(string email, List<string> services) 
    {
        var users = await RetrieveFromUsersDB();
        users.Add(new UserModel() {
            email = email,
            uuid = Guid.NewGuid().ToString(),
            services = services
        });
        await WriteToUserDB(users);
        
    }

    public async Task RemoveUserFromDB(string uuid) 
    {
        var users = await RetrieveFromUsersDB();
        var userToRemove = users.FirstOrDefault(u => u.uuid == uuid);
        if (userToRemove != null)
        {
            users.Remove(userToRemove);
        }

        await WriteToUserDB(users);
    }

    public async Task<List<UserModel>> GetUsers() 
    {
        return await RetrieveFromUsersDB();
    }

    public async Task DumpToUsersDB() 
    {
        List<UserModel> users = new List<UserModel>();
        await WriteToUserDB(users);
    }

    public async Task<bool> EmailExists(string email) 
    {
        var users = await RetrieveFromUsersDB();

        return users.Any(u => u.email == email);
    }

    public async Task<List<string>> GetAllUserEmails() {
        List<UserModel> users = await GetUsers();
        return users.Select(user => user.email).ToList();
    }
}
