using TicTacToe.Domain.Extentions;
using TicTacToe.Domain.Models;
using TicTacToe.Domain.Models.Cordinates;
using TicTacToe.Domain.Models.Players;

namespace TicTacToe.Core;
public class GameLogic
{
    private char[,] _array = new char[3, 3];
    private readonly BasePlayer _player1;
    private readonly BasePlayer _player2;
    private BasePlayer? _currentPlayer;

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

        //omg, its a bot
        Coordinate initialMove = bot.GetMove(_array);
        SetValue(initialMove.X, initialMove.Y);
    }

    public bool GameOver { get; private set; }

    public BasePlayer? CurrentPlayer => _currentPlayer;

    public char GetValue(int x, int y) => _array[x, y];

    public void SetValue(int x, int y)
    {
        if (_array[x, y] == 'X' || _array[x, y] == 'O')
        {
            throw new Exception("Field already ocupied!");
        }
        if (_currentPlayer is null)
        {
            throw new Exception("Failed not get current player.");
        }

        _array[x, y] = _currentPlayer.Symbol;
        int result = BoardExtention.CheckGameSituation(_array);

        if (result < 2)
        {
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
        Coordinate move = bot.GetMove(_array);
        SetValue(move.X, move.Y);
    }
}
