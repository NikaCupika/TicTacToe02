using TicTacToe.Domain.Extentions;
using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Domain.Models.Players.Bots;

public class HardBot : BaseBot
{
    private readonly Random _random = new();

    public HardBot() 
        : base("Hard Bot") 
    { }

    public override Coordinate GetMove(char[,] board)
    {
        CoordinateBot move = MinMaxMove(board, this);
        return new Coordinate(move.X, move.Y);
    }

    private CoordinateBot MinMaxMove(char[,] board, BasePlayer player)
    {
        int result = BoardExtention.CheckGameSituation(board);
        switch (result)
        {
            case 0: return new CoordinateBot(-1, -1, 0);
            case 1: return new CoordinateBot(-1, -1, 1);
            case -1: return new CoordinateBot(-1, -1, -1);
            default: break;
        }

        List<CoordinateBot> list = new();
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (board[x, y] != default)
                {
                    continue;
                }

                board[x, y] = player.Symbol;
                CoordinateBot move = MinMaxMove(board, player);
                list.Add(new CoordinateBot(x, y, move.Value));
                board[x, y] = default;
            }
        }

        int chosenValue = player.Symbol == 'X' ? list.Max(x => x.Value) : list.Min(x => x.Value);
        list.RemoveAll(x => x.Value != chosenValue);

        return list[_random.Next(0, list.Count)];
    }
}