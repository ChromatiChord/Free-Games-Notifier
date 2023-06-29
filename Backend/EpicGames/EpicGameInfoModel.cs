public struct EpicGameInfoModel
{
    public string Name { get; }
    public string ProductUrl { get; }

    public EpicGameInfoModel(string name, string productUrl)
    {
        Name = name;
        ProductUrl = productUrl;
    }
}