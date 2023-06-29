using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

class DatabaseIO {
    private string _dbPath = "database.json";

    public void SaveToDB(List<EpicGameInfoModel> models)
    {
        string json = JsonSerializer.Serialize(models);
        File.WriteAllText(_dbPath, json);
    }

    public List<EpicGameInfoModel> RetrieveFromDB()
    {
        if (!File.Exists(_dbPath))
        {
            return new List<EpicGameInfoModel>();
        }

        string json = File.ReadAllText(_dbPath);
        return JsonSerializer.Deserialize<List<EpicGameInfoModel>>(json);
    }
}