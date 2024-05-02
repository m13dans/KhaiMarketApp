namespace KhaiMarket.API.DTO;

public class ReviewDTO
{
    public string VoterName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int NumStars { get; set; }

}