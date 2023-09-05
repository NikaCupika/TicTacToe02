using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Domain.Models.Players.Bots;

public class EasyBot : BaseBot
{
    public EasyBot() 
        : base("Easy Bot")
    { }

    public override Coordinate GetMove(char[,] board)
    {
        Random random = new();
        int x, y;

        do
        {
            x = random.Next(0, 3);
            y = random.Next(0, 3);
        } while (board[x, y] != default);

        return new Coordinate(x, y);
    }
}