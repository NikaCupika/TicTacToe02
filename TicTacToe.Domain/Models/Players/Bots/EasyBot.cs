using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Domain.Models.Players.Bots;

public class EasyBot : BaseBot
{
    private readonly Random _random = new();

    public EasyBot() 
        : base("Easy Bot")
    { }

    public override Coordinate GetMove(char[,] board)
    {
        int x, y;
        do
        {
            x = _random.Next(0, 3);
            y = _random.Next(0, 3);
        } while (board[x, y] != default);

        return new Coordinate(x, y);
    }
}