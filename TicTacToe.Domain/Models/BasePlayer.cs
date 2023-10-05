namespace TicTacToe.Domain.Models;
public abstract class BasePlayer
{
    public BasePlayer(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public char Symbol { get; set; }
    public BasePlayer? Enemy { get; set; }
}
