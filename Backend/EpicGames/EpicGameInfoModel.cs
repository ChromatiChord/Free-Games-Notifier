public struct EpicGameInfoModel
{
    public string Name { get; set; }
    public string ProductUrl { get; set; }

    public EpicGameInfoModel(string name, string productUrl)
    {
        Name = name;
        ProductUrl = productUrl;
    }

    public override bool Equals(object? obj)
    {
        if (obj is EpicGameInfoModel)
        {
            var other = (EpicGameInfoModel)obj;
            return this.Name == other.Name && this.ProductUrl == other.ProductUrl;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (Name, ProductUrl).GetHashCode();
    }
}
