namespace TicTacToe.Domain.Models.Cordinates;

public struct CoordinateBot
{
    public CoordinateBot(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;
    }

    public int X { get; }
    public int Y { get; }

    /// <summary>
    /// Value of this move
    /// </summary>
    public int Value { get; }
}