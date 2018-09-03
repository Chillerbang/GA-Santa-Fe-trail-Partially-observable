using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaFeTrail
{
    struct xandy
    {
        public int x;
        public int y;
        public bool found;
    }
    class MoveAnt // always clone before
    {
        int aPostiony = 0;
        int aPostionx = 0;
        int width = 25;
        int height = 25;
        xandy[] allCoins;
        public MoveAnt(int aXpos,int aYpos, char[][] map)
        {
            aPostionx = aXpos;
            aPostiony = aYpos;
            xandy x;
            List<xandy> lxandy = new List<xandy>();
            for (int j = 0; j < width; j++)
            {
                for (int k = 0; k < height; k++)
                {
                    if (map[j][k] == '1')
                    {
                        x.x = k;
                        x.y = j;
                        x.found = false;
                        lxandy.Add(x);
                    }
                }
            }
            allCoins = lxandy.ToArray();
        }

        public int CalcScore(string MovementSet, char[][] map)
        {
            int width = 25;
            int height = 25;
            int score = 0;
            
            int currentx = aPostionx;
            int currenty = aPostiony;

            //find coins
            
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
                    map[currenty][currentx] = '1';
                    score += 10;
                    map[currenty][currentx] = '3';
                    for (int l = 0; l < allCoins.Length; l++)
                    {
                        if (allCoins[l].y == currenty && allCoins[l].x == currentx)
                        {
                            allCoins[l].found = true;
                            break;
                        }
                    }
                }else
                if (map[currenty][currentx] == '0')
                {
                    map[currenty][currentx] = '3';
                    // encorage if you are getting closer
                    score += 3;
                    //for (int l = 0; l < allCoins.Length; l++)
                    //{
                    //    if (((allCoins[l].x + 1 == currentx) || (allCoins[l].x - 1 == currentx) || (allCoins[l].x == currentx)) && ((allCoins[l].y == currenty) || (allCoins[l].y - 1 == currenty) || (allCoins[l].y + 1 == currenty)) && (allCoins[l].found == false))
                    //    {
                    //        //score += 5;
                    //    }
                    //    else
                    //    {
                    //        //if (score > 2)
                    //        //{
                    //        //    score -= 1;
                    //        //}

                    //    }

                    //}
                }else
                if (map[currenty][currentx] == '3')
                {
                        score -= 2;
                }
            }
            return score;
        }

        private bool close(int currenty, int y, int currentx, int x)
        {
            throw new NotImplementedException();
        }
    }
}
