using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaFeTrail
{
    class MoveAnt
    {
        public int CalcScore(int[] MovementSet, char[][] map)
        {
            int width = 25;
            int height = 25;
            int score = 0;
            int retJ = 0;
            int retI = 0;
            bool end = false;
            // find A
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i][j] == 'A')
                    {
                        retI = i;
                        retJ = j;
                        end = true;
                        break;
                    }
                    retI = i;
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

                        break;
                    case 1: // move right

                        break;
                    case 2: // move down

                        break;
                    case 3: // move right

                        break;
                    default:

                        break;

                }

            }
            return score;
        }

        

    }
}
