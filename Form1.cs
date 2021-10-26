using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace SudokuKing
{
    public partial class Form1 : Form
    {
        private SudokuBoard board;
        private string file;
        private Label[,] labels = new Label[9,9];
        public Form1()
        {
            InitializeComponent();
            this.board = new SudokuBoard();
            this.file = "";
            initializeLabels();
            Task.Run(initializeLabels);
        }

        private void initializeLabels()
        {
            Console.WriteLine("test me");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    createNewLabel(i, j);
                }
            }
        }
        private void createNewLabel(int i, int j)
        {
            Label label = new Label();
            label.Name = label + i.ToString() + j.ToString();
            label.Text = "";
            this.labels[i, j] = label;
            this.tableLayoutPanel1.Controls.Add(label, i, j);
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            if(board.filePath != "")
            {
                SudokuSolver.solveSudoku(board.board);
                setLabels(board.board);
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            this.file = this.openFileDialog.InitialDirectory + this.openFileDialog.FileName;
            intitialzeSudokuBoard(this.file);
        }

        private void intitialzeSudokuBoard(string file)
        {
            char[,] boardArray = this.board.readSudokuFile(file);
            setLabels(boardArray);
        }

        private void setLabels(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    char s = board[i, j];
                    if (s.Equals('.')) continue;

                    this.labels[i, j].Text = s.ToString();
                }
            }
        }

    }
}
