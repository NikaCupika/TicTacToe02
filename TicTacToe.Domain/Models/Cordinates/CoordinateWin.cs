namespace TicTacToe.Domain.Models.Cordinates;

public class CoordinateWin
{
    private readonly Coordinate _start;
    private readonly Coordinate _end;

    public CoordinateWin(Coordinate start, Coordinate end)
    {
        _start = start;
        _end = end;
    }

    public int X1 => _start.X + 1 * 95;
    public double Y1 => _start.Y + 1 * 84.5 + 187;

    public int X2 => _end.X + 1 * 95;
    public double Y2 => _end.Y + 1 * 84.5 + 187;
}
