interface IDatabaseIO 
{
    Task WriteEpicGamesDB(List<EpicGameInfoModel> models);
    Task<List<EpicGameInfoModel>> RetrieveFromEpicGamesDB();
    Task<List<UserModel>> RetrieveFromUsersDB();
    Task DumpToUsersDB();
    Task WriteToUserDB(List<UserModel> users);
    Task AddUserToDb(string email, List<string> services);
    Task RemoveUserFromDB(string uuid);
    Task<List<UserModel>> GetUsers();
    Task<bool> EmailExists(string email);
    Task<List<string>> GetAllUserEmails();
}