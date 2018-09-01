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
        public MoveAnt(int aXpos,int aYpos)
        {
            aPostionx = aXpos;
            aPostiony = aYpos;
        }

        public int CalcScore(string MovementSet, char[][] map)
        {
            int width = 25;
            int height = 25;
            int score = 0;
            
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
                    score++;
                }
            }
            return score;
        }
    }
}
