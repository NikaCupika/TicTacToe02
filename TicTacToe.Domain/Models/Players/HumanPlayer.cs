using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Domain.Models.Players;
public class HumanPlayer : BasePlayer
{
    public HumanPlayer() 
        : base("Human")
    { }

    //public HumanPlayer(string name) 
    //    : base(name)
    //{ }

    public override Coordinate GetMove(char[,] board) => throw new NotImplementedException();
}
