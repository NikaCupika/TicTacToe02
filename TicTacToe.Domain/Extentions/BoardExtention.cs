namespace TicTacToe.Domain.Extentions;
public static class BoardExtention
{
    /// <returns>
    /// Check status of the game:
    /// [-1] player 2 won,
    /// [0] draw,
    /// [1] player 1 won,
    /// [2] ongoing
    /// </returns>
    public static int CheckGameSituation(char[,] board)
    {
        // columns
        for (int x = 0; x < 3; x++)
        {
            if (board[0, x] != default && board[0, x] == board[1, x] && board[1, x] == board[2, x])
            {
                return board[0, x] == 'X' ? 1 : -1;
            }
        }

        // rows
        for (int y = 0; y < 3; y++)
        {
            if (board[y, 0] != default && board[y, 0] == board[y, 1] && board[y, 1] == board[y, 2])
            {
                return board[y, 0] == 'X' ? 1 : -1;
            }
        }

        // diagonals
        if (board[0, 0] != default && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) //left up to right down
        {
            return board[0, 0] == 'X' ? 1 : -1;
        }
        if (board[0, 2] != default && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) //left down to right up
        {
            return board[0, 2] == 'X' ? 1 : -1;
        }

        // is it draw?
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (board[y, x] == default)
                {
                    return 2;
                }
            }
        }
        
        // draw
        return 0;
    }
}
