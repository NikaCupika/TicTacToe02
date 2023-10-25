using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Domain.Models.Players.Bots;

public class EasyBot : BaseBot
{
    public EasyBot() 
        : base("Easy Bot")
    { }

    public override Coordinate GetMove(char[,] board)
    {
        int x, y;
        do
        {
            x = new Random().Next(0, 3);
            y = new Random().Next(0, 3);
        } while (board[y, x] != default);

        return new Coordinate(x, y);
    }
}