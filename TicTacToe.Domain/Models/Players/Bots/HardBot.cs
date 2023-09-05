using TicTacToe.Domain.Extentions;
using TicTacToe.Domain.Models.Cordinates;

namespace TicTacToe.Domain.Models.Players.Bots;

public class HardBot : BaseBot
{
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
        switch (BoardExtention.CheckGameSituation(board))
        {
            case 0: return new CoordinateBot(-1, -1, 0);
            case 1: return new CoordinateBot(-1, -1, 1);
            case -1: return new CoordinateBot(-1, -1, -1);
        }

        List<CoordinateBot> coords = new();
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (board[y, x] != default)
                {
                    continue;
                }

                board[y, x] = player.Symbol;
                CoordinateBot move = MinMaxMove(board, player.Enemy);
                coords.Add(new CoordinateBot(x, y, move.Value));
                board[y, x] = default;
            }
        }

        int chosenValue = player.Symbol == 'X' ? coords.Max(x => x.Value) : coords.Min(x => x.Value);
        coords.RemoveAll(x => x.Value != chosenValue);

        return coords[new Random().Next(0, coords.Count)];
    }
}