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

    /// <summary>
    /// Get the most aficient next move, by calculating through all posibilities
    /// </summary>
    private CoordinateBot MinMaxMove(char[,] board, BasePlayer currentPlayer)
    {
        // is given game done
        switch (BoardExtention.CheckGameSituation(board))
        {
            case 0: return new CoordinateBot(-1, -1, 0);
            case 1: return new CoordinateBot(-1, -1, 1);
            case -1: return new CoordinateBot(-1, -1, -1);
        }

        // on going
        List<CoordinateBot> coords = new();
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (board[y, x] != default)
                {
                    continue;
                }

                // posible move
                board[y, x] = currentPlayer.Symbol;
                CoordinateBot move = MinMaxMove(board, currentPlayer.Enemy!);
                coords.Add(new CoordinateBot(x, y, move.Value));

                // last version
                board[y, x] = default;
            }
        }

        // the best moves
        int chosenValue = currentPlayer.Symbol == 'X' ? coords.Max(x => x.Value) : coords.Min(x => x.Value);
        coords.RemoveAll(x => x.Value != chosenValue);

        return coords[new Random().Next(0, coords.Count)];
    }
}