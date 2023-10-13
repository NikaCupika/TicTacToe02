using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Domain.Models.Players.Bots;

public class EasyBot : BaseBot
{
    private Random random = new Random();

    public EasyBot() 
        : base("Einfacher Roboter")
    { }

    public override Coordinate GetMove(char[,] board)
    {
        int x, y;
        do
        {
            x = random.Next(0, 3);
            y = random.Next(0, 3);
        } while (board[y, x] != default);

        return new Coordinate(x, y);
    }
}