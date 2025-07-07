
public class Asset
{
    public Asset()
    {
    }

    public Asset(AssetType type, string brand,DateTime purchaseDate)
    {
        PurchaseDate = purchaseDate;
        Type = type;
        Brand = brand;
    }

    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }

    public AssetType Type { get; set; }

    public string Brand { get; set; }


}

public enum AssetType
{
    Laptop,
    MobilePhone,
}