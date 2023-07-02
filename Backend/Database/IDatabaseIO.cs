interface IDatabaseIO 
{
    Task WriteEpicGamesDB(List<EpicGameInfoModel> models);
    Task<List<EpicGameInfoModel>> RetrieveFromEpicGamesDB();
    Task<EmailDBModel> RetrieveFromEmailsDB();
    Task DumpToEmailsDB();
    Task WriteToEmailDB(EmailDBModel emails);
    Task AddEmailToDb(string email);
    Task RemoveEmailFromDB(string email);
    Task<List<string>> GetEmails();
}