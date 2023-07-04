using System.Text.Json;
using System.Threading.Tasks;

class DatabaseIO : IDatabaseIO
{
    private string _epicGamesDbPath = "./Database/epicgamesdb.json";
    private string _usersDbPath = "./Database/users.json";

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

    public Task<List<UserModel>> RetrieveFromUsersDB()
    {
        if (!File.Exists(_usersDbPath))
        {
            return Task.FromResult(new List<UserModel>());
        }

        string json = File.ReadAllText(_usersDbPath);
        var result = JsonSerializer.Deserialize<List<UserModel>>(json);
        return Task.FromResult(result ?? new List<UserModel>());
    }

    public Task DumpToUsersDB() 
    {
        List<UserModel> data = new List<UserModel>();
        return WriteToUserDB(data);
    }

    public Task WriteToUserDB(List<UserModel> users) {
        string json = JsonSerializer.Serialize(users);
        File.WriteAllText(_usersDbPath, json);
        return Task.CompletedTask;
    }

    public Task AddUserToDb(string email, List<string> services) 
    {

        UserModel newUser = new() {
            email = email,
            uuid = Guid.NewGuid().ToString(),
            services = services
        };
        List<UserModel> data = RetrieveFromUsersDB().Result;
        data.Add(newUser);
        return WriteToUserDB(data);
    }

    public Task RemoveUserFromDB(string uuid) 
    {
        List<UserModel> data = RetrieveFromUsersDB().Result;

        var userToRemove = data.FirstOrDefault(u => u.uuid == uuid);
        if (userToRemove != null)
        {
            data.Remove(userToRemove);
        }

        return WriteToUserDB(data);
    }

    public Task<List<UserModel>> GetUsers() 
    {
        List<UserModel> data = RetrieveFromUsersDB().Result;
        return Task.FromResult(data);
    }

    public Task<bool> EmailExists(string email) 
    {
        List<UserModel> data = RetrieveFromUsersDB().Result;
        return Task.FromResult(data.Any(u => u.email == email));
    }

    public async Task<List<string>> GetAllUserEmails() {
        List<UserModel> users = await GetUsers();
        return users.Select(user => user.email).ToList();
    }
}
