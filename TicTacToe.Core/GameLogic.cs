using TicTacToe.Domain.Extentions;
using TicTacToe.Domain.Models;
using TicTacToe.Domain.Models.Cordinates;
using TicTacToe.Domain.Models.Players;

namespace TicTacToe.Core;
public class GameLogic
{
    private char[,] _board = new char[3, 3];
    private readonly BasePlayer _player1;
    private readonly BasePlayer _player2;
    private BasePlayer? _currentPlayer;

    /// <summary>
    /// Create a game and if player one a bot is, make the first move
    /// </summary>
    public GameLogic(BasePlayer player1, BasePlayer player2)
    {
        _player1 = player1;
        _player1.Symbol = 'X';
        _player1.Enemy = player2;

        _player2 = player2;
        _player2.Symbol = 'O';
        _player2.Enemy = player1;

        _currentPlayer = _player1;
        if (_currentPlayer is not BaseBot bot)
        {
            return;
        }

        // bot moves
        Coordinate initialMove = bot.GetMove(_board);
        SetValue(initialMove);
    }

    public bool GameOver { get; private set; }

    public BasePlayer? CurrentPlayer => _currentPlayer;

    /// <summary>
    /// Get X or O on given coordinates
    /// </summary>
    public char GetValue(Coordinate coord) => _board[coord.Y, coord.X];

    /// <summary>
    /// Place current player on the given coordinates. If the enemy is a bot it will make it's move
    /// </summary>
    /// <exception cref="Exception">
    /// Thrown when coordinates already has a player or the current player doesn't exist
    /// </exception>
    public void SetValue(Coordinate coord)
    {
        if (_board[coord.Y, coord.X] == 'X' || _board[coord.Y, coord.X] == 'O')
        {
            throw new Exception("Field already ocupied!");
        }
        if (_currentPlayer is null)
        {
            throw new Exception("Failed not get current player.");
        }

        _board[coord.Y, coord.X] = _currentPlayer.Symbol;
        int result = BoardExtention.CheckGameSituation(_board);

        if (result < 2)
        {
            // game finished
            if (result == 0)
            {
                _currentPlayer = default;
            }

            GameOver = true;
            return;
        }

        //ongoing
        _currentPlayer = _currentPlayer.Enemy;

        if (_currentPlayer is not BaseBot bot)
        {
            return;
        }

        // bot moves
        Coordinate move = bot.GetMove(_board);
        SetValue(move);
    }
}
