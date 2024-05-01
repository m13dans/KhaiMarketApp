namespace KhaiMarket.API.Core.Entities;

public class Review : BaseEntities
{
    public string VoterName { get; set; } = string.Empty;
    public int NumStars { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int ProductId { get; set; }
}