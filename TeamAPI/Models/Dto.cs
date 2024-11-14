namespace TeamAPI.Models
{
    public record CreatePlayerDto(string Name, int Height, int Weight);
    public record UpdatePlayerDto(string name, int Weight);
}
