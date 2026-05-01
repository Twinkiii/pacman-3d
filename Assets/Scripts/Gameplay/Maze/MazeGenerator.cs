using System.Collections.Generic;
using UnityEngine;

namespace Pacman.Gameplay.Maze
{
    public class MazeGenerator 
    {
        public int[,] Map { get; private set; }
        public List<Vector2Int> FreeCells { get; private set; }

        private int m_Width;
        private int m_Height;

        public MazeGenerator(int width, int height)
        {
            m_Width = width % 2 == 0 ? width + 1 : width;
            m_Height = height % 2 == 0 ? height + 1 : height;
            FreeCells = new List<Vector2Int>();
        }

        public void Generate()
        {
            Map = new int[m_Height, m_Width];

            for(int r = 0; r < m_Height; r++)
                for(int c = 0; c < m_Width; c++)
                    Map[r, c] = 1;

            CarvePassage(1, 1);

            FreeCells.Clear();
            for(int r = 0; r < m_Height; r++)
            {
                for(int c = 0; c < m_Width; c++)
                {
                    if(Map[r, c] == 0)
                    {
                        FreeCells.Add(new Vector2Int(c, r));
                    }
                }
            }
        }

        private void CarvePassage(int row, int col)
        {
            Map[row, col] = 0;

            int[]dRow = {-2, 2, 0, 0 };
            int[]dCol = {0, 0, -2, 2 };
            Shuffle(dRow, dCol);
            for(int i = 0; i < 4; i++)
            {
                int newRow = row + dRow[i];
                int newCol = col + dCol[i];
                if(IsInBounds(newRow, newCol) && Map[newRow, newCol] == 1)
                {
                    Map[row + dRow[i]/2, col + dCol[i]/2] = 0;
                    CarvePassage(newRow, newCol);
                    }
                }
            }

        private bool IsInBounds(int newRow, int newCol)
        {
            return newRow > 0 && newRow < m_Height - 1 && 
                   newCol > 0 && newCol < m_Width - 1;
        }
        

        private void Shuffle(int[] dRow, int[] dCol)
        {
            for(int i = 3; i >= 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                int tmpR = dRow[i]; dRow[i] = dRow[j]; dRow[j] = tmpR;
                int tmpC = dCol[i]; dCol[i] = dCol[j]; dCol[j] = tmpC;
            }
        }
    }
}
