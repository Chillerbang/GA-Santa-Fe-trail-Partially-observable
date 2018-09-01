using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaFeTrail
{
    class MoveAnt // always clone before
    {
        int aPostiony = 0;
        int aPostionx = 0;
        
        private int[][] neighbors;

        public MoveAnt(int aXpos,int aYpos)
        {
            aPostionx = aXpos;
            aPostiony = aYpos;
        }

        //private void findNeighbors()
        //{
        //    int[] top = new int[] { aPostionx, aPostiony + 1 };
        //    int[] right = new int[] { aPostionx + 1, aPostiony };
        //    int[] bottom = new int[] { aPostionx, aPostiony - 1 };
        //    int[] left = new int[] { aPostionx - 1, aPostiony };

        //    neighbors = new int[][] { top, right, bottom, left };
        //}

        public int CalcScore(string MovementSet, char[][] map)
        {
            int width = 25;
            int height = 25;
            int score = 0;
            int distancetocoin;
            int currentx = aPostionx;
            int currenty = aPostiony;

            
            for (int i = 0; i < MovementSet.Length; i++)
            {
                switch (MovementSet[i])
                {
                    case '0': // move up
                        if (currenty == 0)
                        {
                            currenty = height - 1;
                        }
                        else
                        {
                            currenty -= 1;
                        }
                        break;
                    case '1': // move down
                        if (currenty == height - 1)
                        {
                            currenty = 0;
                        }
                        else
                        {
                            currenty += 1;
                        }
                        break;
                    case '2': // move Left
                        if (currentx == 0)
                        {
                            currentx = width - 1;
                        }
                        else
                        {
                            currentx -= 1;
                        }
                        break;
                    case '3': // move right
                        if (currentx == width - 1)
                        {
                            currentx = 0;
                        }
                        else
                        {
                            currentx += 1;
                        }
                        break;
                }
                if (map[currenty][currentx] == '1')
                {
                    map[currenty][currentx] = '0';
                    score+= 10*(MovementSet.Length-i);
                }
                if (map[currenty][currentx] == '0')
                {
                    map[currenty][currentx] = '3';
                    if (map[(currenty + 1) % height][currentx] == '1' ||
                        map[currenty][(currentx + 1) % width] == '1' ||
                        map[(currenty + 1) % height][(currentx + 1)%width] == '1' ||
                        map[RemoveNegative(currenty -1) ][currentx] == '1' ||
                        map[currenty][RemoveNegative(currentx - 1) ] == '1' ||
                        map[RemoveNegative(currenty - 1) ][RemoveNegative(currentx - 1) ] == '1' ||
                        map[(currenty + 1) % height][RemoveNegative(currentx - 1) ] == '1' ||
                        map[RemoveNegative(currenty - 1) ][(currentx + 1)%height] == '1' )
                    {
                        score++;
                    }
                }
                if (map[currenty][currentx] == '0')
                {
                    map[currenty][currentx] = '3';
                    score -= 5;
                }
            }
            return score;
        }

        public int RemoveNegative(int value)
        {
            if (value < 0)
            {
                return 24;
            }
            else
            {
                return value;
            }
        }
    }

    
}
