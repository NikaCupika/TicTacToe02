using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Domain.Models.Players;
public abstract class BaseBot : BasePlayer
{
    protected BaseBot(string name) 
        : base(name)
    { }

    /// <summary>
    /// Which coordinates should a player go to next
    /// </summary>
    public abstract Coordinate GetMove(char[,] board);
}
