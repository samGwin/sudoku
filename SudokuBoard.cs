using System;
using System.Collections.Generic;

public class SudokuBoard
{
    public char[,] board { get; private set; }
    public string filePath { get; private set; }
    public SudokuBoard()
    {
        this.board = new char[9, 9];
        this.filePath = "";
    }

    public char[,] readSudokuFile(string filePath)
    {
        this.filePath = filePath;
        string line;
        string bstring = "";
        char[] barray;
        int count = 0;

        System.IO.StreamReader file = new System.IO.StreamReader(@filePath);
        while ((line = file.ReadLine()) != null)
        {
            if (line.Contains("|"))
            {
                bstring = bstring + readLineStyle1(line);
            }
            else if (line.Contains("0"))
            {
                bstring = bstring + readLineStyle2(line);
            }
            else if (line.Length == 81)
            {
                bstring = line;
            }
        }
        barray = bstring.ToCharArray();
        for(int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                this.board[i, j] = barray[count];
                count++;
            }
        }
        
        file.Close();

        return this.board;
    }

    private string readLineStyle1(string line)
    {
        if (line.Contains("-") || line == "")
        {
            return "";
        }

        var str = line.Replace(" ", "");
        return str.Replace("|", "");
    }

    private string readLineStyle2(string line)
    {
        if (line == "")
        {
            return "";
        }

        return line.Replace("0", ".");
    }

}

public class SudokuSolver{
    public static void solveSudoku(char[,] board)
    {
        if (board == null || board.Length == 0)
            return;
        solve(board);
    }
    private static bool solve(char[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == '.')
                {
                    for (char c = '1'; c <= '9'; c++)
                    {
                        if (isValid(board, i, j, c))
                        {
                            board[i, j] = c;

                            if (solve(board))
                                return true;
                            else
                                board[i, j] = '.';
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }
    private static bool isValid(char[,] board, int row, int col, char c)
    {
        for (int i = 0; i < 9; i++)
        {
            //check row  
            if (board[i, col] != '.' && board[i, col] == c)
                return false;
            //check column  
            if (board[row, i] != '.' && board[row, i] == c)
                return false;
            //check 3*3 block  
            int rtest = 3 * (row / 3) + i / 3;
            int ctest = 3 * (col / 3) + i % 3;
            if (board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] != '.' && board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] == c)
                return false;
        }
        return true;
    }
}