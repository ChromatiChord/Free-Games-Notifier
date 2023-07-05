using Google.Cloud.Firestore;
using System.Threading.Tasks;

class FirestoreDatabaseIO : IDatabaseIO
{
    FirestoreDb _db;

    public FirestoreDatabaseIO()
    {
        string projectId = "free-games-notifier-391811";
        _db = FirestoreDb.Create(projectId);
    }

    public async Task WriteEpicGamesDB(List<EpicGameInfoModel> models)
    {
        var collectionRef = _db.Collection("epicgamesdb");
        foreach(var model in models)
        {
            await collectionRef.AddAsync(model);
        }
    }

    public async Task<List<EpicGameInfoModel>> RetrieveFromEpicGamesDB()
    {
        var collectionRef = _db.Collection("epicgamesdb");
        var snapshot = await collectionRef.GetSnapshotAsync();
        return snapshot.Documents.Select(doc => doc.ConvertTo<EpicGameInfoModel>()).ToList();
    }

    public async Task<List<UserModel>> RetrieveFromUsersDB()
    {
        var collectionRef = _db.Collection("users");
        var snapshot = await collectionRef.GetSnapshotAsync();
        return snapshot.Documents.Select(doc => doc.ConvertTo<UserModel>()).ToList();
    }

    public Task DumpToUsersDB() 
    {
        // To dump the users database, you would need to delete each document individually.
        // Firestore doesn't currently support delete operations on entire collections.
        throw new NotImplementedException();
    }

    public async Task WriteToUserDB(List<UserModel> users)
    {
        var collectionRef = _db.Collection("users");
        foreach(var user in users)
        {
            await collectionRef.AddAsync(user);
        }
    }

    public async Task AddUserToDb(string email, List<string> services) 
    {
        var collectionRef = _db.Collection("users");
        UserModel newUser = new() {
            email = email,
            uuid = Guid.NewGuid().ToString(),
            services = services
        };
        await collectionRef.AddAsync(newUser);
    }

    public async Task RemoveUserFromDB(string uuid) 
    {
        var collectionRef = _db.Collection("users");
        Query query = collectionRef.WhereEqualTo("uuid", uuid);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
        {
            await documentSnapshot.Reference.DeleteAsync();
        }
    }

    public async Task<List<UserModel>> GetUsers() 
    {
        return await RetrieveFromUsersDB();
    }

    public async Task<bool> EmailExists(string email) 
    {
        var collectionRef = _db.Collection("users");
        Query query = collectionRef.WhereEqualTo("email", email);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
        return querySnapshot.Count > 0;
    }

    public async Task<bool> UserExists(string uuid) 
    {
        var collectionRef = _db.Collection("users");
        Query query = collectionRef.WhereEqualTo("uuid", uuid);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
        return querySnapshot.Count > 0;
    }

    public async Task<List<string>> GetAllUserEmails() 
    {
        var users = await RetrieveFromUsersDB();
        return users.Select(user => user.email).ToList();
    }
}
