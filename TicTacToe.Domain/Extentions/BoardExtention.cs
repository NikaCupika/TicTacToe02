namespace TicTacToe.Domain.Extentions;
public static class BoardExtention
{
    /// <summary>
    /// Check status of the game
    /// -> -1 player 2 won
    /// ->  0 draw
    /// ->  1 player 1 won
    /// ->  2 ongoin
    /// </summary>
    /// <returns></returns>
    public static int CheckGameSituation(char[,] board)
    {
        //check rows
        for (int y = 0; y < 3; y++)
        {
            if (board[0, y] != default && board[0, y] == board[1, y] && board[1, y] == board[2, y])
            {
                return board[0, y] == 'X' ? 1 : -1;
            }
        }

        //check columns
        for (int x = 0; x < 3; x++)
        {
            if (board[x, 0] != default && board[x, 0] == board[x, 1] && board[x, 1] == board[x, 2])
            {
                return board[x, 0] == 'X' ? 1 : -1;
            }
        }

        //check diagonals
        if (board[0, 0] != default && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) //lo nach ru
        {
            return board[0, 0] == 'X' ? 1 : -1;
        }
        if (board[0, 2] != default && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) //lu nach ro
        {
            return board[0, 2] == 'X' ? 1 : -1;
        }

        // not draw ?
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (board[x, y] == default)
                {
                    return 2;
                }
            }
        }
        
        // draw
        return 0;
    }
}
