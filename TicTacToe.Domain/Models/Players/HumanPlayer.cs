using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Domain.Models.Players;
public class HumanPlayer : BasePlayer
{
    public HumanPlayer() 
        : base("Human")
    { }

    /// <summary>
    /// Human player desides for themselves where they will move
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override Coordinate GetMove(char[,] board) => throw new NotImplementedException();
}
