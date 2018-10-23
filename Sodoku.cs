using System;
using System.Linq;
using System.Windows.Forms;

namespace Sodoku
{
    public partial class Sodoku : Form
    {
        Square[,] SudokiGrid;

        public Sodoku()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            Initialize();
            PatternOne();
            UpdateScreen();
        }

        private void PatternOne()
        {
            Confirm(0, 2, 9);
            Confirm(0, 3, 4);
            Confirm(0, 6, 7);
            Confirm(1, 0, 8);
            Confirm(1, 2, 4);
            Confirm(1, 6, 5);
            Confirm(1, 8, 6);
            Confirm(2, 3, 6);
            Confirm(2, 4, 5);
            Confirm(2, 6, 3);
            Confirm(2, 7, 9);
            Confirm(3, 1, 1);
            Confirm(3, 2, 6);
            Confirm(3, 3, 3);
            Confirm(3, 7, 4);
            Confirm(3, 8, 5);
            Confirm(4, 0, 2);
            Confirm(4, 2, 7);
            Confirm(4, 6, 6);
            Confirm(5, 2, 3);
            Confirm(5, 4, 6);
            Confirm(5, 6, 1);
            Confirm(6, 0, 4);
            Confirm(6, 5, 3);
            Confirm(6, 6, 2);
            Confirm(6, 8, 7);
            Confirm(7, 0, 7);
            Confirm(7, 4, 9);
            Confirm(7, 7, 5);
            Confirm(7, 8, 3);
            Confirm(8, 0, 3);
            Confirm(8, 3, 7);
            Confirm(8, 5, 6);
            Confirm(8, 8, 8);
        }

        private void Initialize()
        {
            SudokiGrid = new Square[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    SudokiGrid[i, j] = new Square();
        }

        private void SearchEveryWhere()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    Square square = SudokiGrid[i, j];
                    SearchHorizontal(i, j);
                    SearchVertical(i, j);
                    SearchCube(i, j);
                    if (square.GetPossibilities().Count == 1)
                        Confirm(i, j, square.GetPossibilities().First());
                }
        }

        private void SearchVertical(int x, int y)
        {
            SearchUp(x, y);
            SearchDown(x, y);
        }

        private void SearchHorizontal(int x, int y)
        {
            SearchRight(x, y);
            SearchLeft(x, y);
        }

        private void Confirm(int x, int y, int num)
        {
            SudokiGrid[x, y].SetCertainty(num);
        }

        private void SearchUp(int x, int y)
        {
            Square current = SudokiGrid[x, y];
            while (x > 0)
            {
                current.RemovePossibilities(SudokiGrid[x, y].GetCertainty());
                x--;
            }
        }

        private void SearchRight(int x, int y)
        {
            Square current = SudokiGrid[x, y];
            while (y < 9)
            {
                current.RemovePossibilities(SudokiGrid[x, y].GetCertainty());
                y++;
            }
        }

        private void SearchDown(int x, int y)
        {
            Square current = SudokiGrid[x, y];
            while (x < 9)
            {
                current.RemovePossibilities(SudokiGrid[x, y].GetCertainty());
                x++;
            }
        }

        private void SearchLeft(int x, int y)
        {
            Square current = SudokiGrid[x, y];
            while (y > 0)
            {
                current.RemovePossibilities(SudokiGrid[x, y].GetCertainty());
                y--;
            }
        }

        private void SearchCube(int x, int y)
        {
            switch (WhatCube(x, y))
            {
                case 0:
                    CheckCube(x, y, 0, 3, 0, 3);
                    break;
                case 1:
                    CheckCube(x, y, 3, 6, 0, 3);
                    break;
                case 2:
                    CheckCube(x, y, 6, 9, 0, 3);
                    break;
                case 3:
                    CheckCube(x, y, 0, 3, 3, 6);
                    break;
                case 4:
                    CheckCube(x, y, 3, 6, 3, 6);
                    break;
                case 5:
                    CheckCube(x, y, 6, 9, 3, 6);
                    break;
                case 6:
                    CheckCube(x, y, 0, 3, 6, 9);
                    break;
                case 7:
                    CheckCube(x, y, 3, 6, 6, 9);
                    break;
                case 8:
                    CheckCube(x, y, 6, 9, 6, 9);
                    break;
            }
        }

        private int WhatCube(int x, int y)
        {
            int cubeNum = 0;
            if (Between(x, 0, 2)) cubeNum += 0;
            else if (Between(x, 3, 5)) cubeNum += 1;
            else if (Between(x, 6, 8)) cubeNum += 2;
            if (Between(y, 0, 2)) cubeNum += 0;
            else if (Between(y, 3, 5)) cubeNum = 3;
            else if (Between(y, 6, 8)) cubeNum += 6;
            return cubeNum;
        }

        private void CheckCube(int x, int y, int iStart, int iEnd, int jStart, int jEnd)
        {
            Square current = SudokiGrid[x, y];
            for (int i = iStart; i < iEnd; i++)
                for (int j = jStart; j < jEnd; j++)
                    current.RemovePossibilities(SudokiGrid[i, j].GetCertainty());
        }

        private void UpdateScreen()
        {
            UpdateSingle(LBL0_0, 0, 0);
            UpdateSingle(LBL0_1, 0, 1);
            UpdateSingle(LBL0_2, 0, 2);
            UpdateSingle(LBL0_3, 0, 3);
            UpdateSingle(LBL0_4, 0, 4);
            UpdateSingle(LBL0_5, 0, 5);
            UpdateSingle(LBL0_6, 0, 6);
            UpdateSingle(LBL0_7, 0, 7);
            UpdateSingle(LBL0_8, 0, 8);
            UpdateSingle(LBL1_0, 1, 0);
            UpdateSingle(LBL1_1, 1, 1);
            UpdateSingle(LBL1_2, 1, 2);
            UpdateSingle(LBL1_3, 1, 3);
            UpdateSingle(LBL1_4, 1, 4);
            UpdateSingle(LBL1_5, 1, 5);
            UpdateSingle(LBL1_6, 1, 6);
            UpdateSingle(LBL1_7, 1, 7);
            UpdateSingle(LBL1_8, 1, 8);
            UpdateSingle(LBL2_0, 2, 0);
            UpdateSingle(LBL2_1, 2, 1);
            UpdateSingle(LBL2_2, 2, 2);
            UpdateSingle(LBL2_3, 2, 3);
            UpdateSingle(LBL2_4, 2, 4);
            UpdateSingle(LBL2_5, 2, 5);
            UpdateSingle(LBL2_6, 2, 6);
            UpdateSingle(LBL2_7, 2, 7);
            UpdateSingle(LBL2_8, 2, 8);
            UpdateSingle(LBL3_0, 3, 0);
            UpdateSingle(LBL3_1, 3, 1);
            UpdateSingle(LBL3_2, 3, 2);
            UpdateSingle(LBL3_3, 3, 3);
            UpdateSingle(LBL3_4, 3, 4);
            UpdateSingle(LBL3_5, 3, 5);
            UpdateSingle(LBL3_6, 3, 6);
            UpdateSingle(LBL3_7, 3, 7);
            UpdateSingle(LBL3_8, 3, 8);
            UpdateSingle(LBL4_0, 4, 0);
            UpdateSingle(LBL4_1, 4, 1);
            UpdateSingle(LBL4_2, 4, 2);
            UpdateSingle(LBL4_3, 4, 3);
            UpdateSingle(LBL4_4, 4, 4);
            UpdateSingle(LBL4_5, 4, 5);
            UpdateSingle(LBL4_6, 4, 6);
            UpdateSingle(LBL4_7, 4, 7);
            UpdateSingle(LBL4_8, 4, 8);
            UpdateSingle(LBL5_0, 5, 0);
            UpdateSingle(LBL5_1, 5, 1);
            UpdateSingle(LBL5_2, 5, 2);
            UpdateSingle(LBL5_3, 5, 3);
            UpdateSingle(LBL5_4, 5, 4);
            UpdateSingle(LBL5_5, 5, 5);
            UpdateSingle(LBL5_6, 5, 6);
            UpdateSingle(LBL5_7, 5, 7);
            UpdateSingle(LBL5_8, 5, 8);
            UpdateSingle(LBL6_0, 6, 0);
            UpdateSingle(LBL6_1, 6, 1);
            UpdateSingle(LBL6_2, 6, 2);
            UpdateSingle(LBL6_3, 6, 3);
            UpdateSingle(LBL6_4, 6, 4);
            UpdateSingle(LBL6_5, 6, 5);
            UpdateSingle(LBL6_6, 6, 6);
            UpdateSingle(LBL6_7, 6, 7);
            UpdateSingle(LBL6_8, 6, 8);
            UpdateSingle(LBL7_0, 7, 0);
            UpdateSingle(LBL7_1, 7, 1);
            UpdateSingle(LBL7_2, 7, 2);
            UpdateSingle(LBL7_3, 7, 3);
            UpdateSingle(LBL7_4, 7, 4);
            UpdateSingle(LBL7_5, 7, 5);
            UpdateSingle(LBL7_6, 7, 6);
            UpdateSingle(LBL7_7, 7, 7);
            UpdateSingle(LBL7_8, 7, 8);
            UpdateSingle(LBL8_0, 8, 0);
            UpdateSingle(LBL8_1, 8, 1);
            UpdateSingle(LBL8_2, 8, 2);
            UpdateSingle(LBL8_3, 8, 3);
            UpdateSingle(LBL8_4, 8, 4);
            UpdateSingle(LBL8_5, 8, 5);
            UpdateSingle(LBL8_6, 8, 6);
            UpdateSingle(LBL8_7, 8, 7);
            UpdateSingle(LBL8_8, 8, 8);
        }

        private void UpdateSingle(Label label, int x, int y)
        {
            if (SudokiGrid[x, y].GetCertainty() == 0) label.Text = "▢";
            else label.Text = SudokiGrid[x, y].GetCertainty().ToString();
        }

        private void BTNPossibilities_Click(object sender, EventArgs e)
        {
            PossibilityCheck(Convert.ToInt32(CBBX.Text) - 1, Convert.ToInt32(CBBY.Text) - 1);
        }

        private void PossibilityCheck(int x, int y)
        {
            Square square = SudokiGrid[x, y];
            Console.WriteLine("Ollo?");
            Console.WriteLine("Possibility Count: " + square.GetPossibilities().Count);
            Console.WriteLine("Certainty is: " + square.GetCertainty());
            if (square.GetPossibilities().Count == 0 && square.GetCertainty() == 0)
                Console.WriteLine("Something's wrong here");
            foreach (int poss in square.GetPossibilities())
                Console.WriteLine("Poss: " + poss);
        }

        private void BTNCycle_Click(object sender, EventArgs e)
        {
            Cycle();
        }

        private void Cycle()
        {
            SearchEveryWhere();
            UpdateScreen();
        }

        private bool Between(int value, int start, int end)
        {
            if (value >= start && value <= end) return true;
            else return false;
        }
    }
}
