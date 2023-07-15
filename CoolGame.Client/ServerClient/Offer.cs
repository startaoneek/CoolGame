namespace CoolGameClient.ServerClient;

public class Offer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int OfferType { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Starts At: {StartsAt}, Expires At: {ExpiresAt}, Offer Type: {OfferType}";
    }
}