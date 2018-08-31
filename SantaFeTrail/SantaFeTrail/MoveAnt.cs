using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaFeTrail
{
    class MoveAnt // always clone before
    {
        public int CalcScore(int[] MovementSet, char[][] map)
        {
            int width = 25;
            int height = 25;
            int score = 0;
            int aPostiony = 0;
            int apostionx = 0;
            int currentx = 0;
            int currenty = 0;
            bool end = false;
            // find A
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i][j] == 'A')
                    {
                        currentx = apostionx = i;
                        currenty = aPostiony = j;
                        end = true;
                        break;
                    }
                }
                if (end == true)
                {
                    break;
                }
            }
            
            for (int i = 0; i < MovementSet.Length; i++)
            {
                switch (MovementSet[i])
                {
                    case 0: // move up
                        if (currentx == width-1)
                        {
                            currentx = 0;
                        }
                        else
                        {
                            currentx -= 1;
                        }
                        break;
                    case 1: // move right
                        if (currenty == 24)
                        {
                            currenty = 0;
                        }
                        else
                        {
                            currenty += 1;
                        }
                        break;
                    case 2: // move down
                        if (currentx == 0)
                        {
                            currentx = 24;
                        }
                        else
                        {
                            currentx += 1;
                        }
                        break;
                    case 3: // move left
                        if (currenty == 0)
                        {
                            currenty = 24;
                        }
                        else
                        {
                            currenty -= 1;
                        }
                        break;
                }
                if (map[currentx][currenty] == 1)
                {
                    map[currentx][currenty] = '0';
                    score++;
                }
            }
            return score;
        }
    }
}
