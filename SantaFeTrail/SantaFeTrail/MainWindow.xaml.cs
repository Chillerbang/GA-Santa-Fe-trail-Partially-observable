using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SantaFeTrail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private static int maxScore = 20050;
        private static int numberOfcandidates = 1000;
        private static int chromaLenght = 100;
        private static double constmutationChance = 0.01;

        char[][] map;
        int width = 25;
        int height = 25;
        int aXpos = 0;
        int aYpos = 0;

        
        string[] chroma = new string[numberOfcandidates]; // limited to that of 200 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            //intialise the Game
            initaliseData();
            drawGUI(map, aXpos, aYpos);
            //start genetic algorithem
            createIntialPopulation();
            int max = 0;
            int NumberIterations = 0;
            // testing
            while (true) { 
                int[] pathScore = new int[numberOfcandidates];
                char[][] cpyMap = new char[height][];
                for (int k = 0; k < width; k++)
                {
                    cpyMap[k] = new char[width];
                }

                for (int i = 0; i < numberOfcandidates; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        for (int k = 0; k < height; k++)
                        {
                            cpyMap[j][k] = map[j][k];
                        }
                    }
                    MoveAnt mAnt = new MoveAnt(aXpos, aXpos);
                    pathScore[i] = mAnt.CalcScore(chroma[i], cpyMap);
                    //tbLog.Text += "Best Score" + pathScore[i] + "\n";
                
                }
                for (int j = 0; j < width; j++)
                {
                    for (int k = 0; k < height; k++)
                    {
                        cpyMap[j][k] = map[j][k];
                    }
                }
                max = pathScore.Max();
                System.Diagnostics.Debug.WriteLine("max" +max);
                tbLog.Text += "Best Score" + pathScore.Max() + "\n";
                tbLog.Text += "iterations" + NumberIterations + "\n";
                if (NumberIterations == 5)
                {
                    NumberIterations = 0;
                    //string save = chroma[Array.IndexOf(pathScore, max)];
                    createIntialPopulation();
                    //chroma[RandomNumber(0, numberOfcandidates)] = save;
                }
                // play the best
                int postion = Array.IndexOf(pathScore, max);
                if (max > maxScore)
                {
                    AnimateBest(cpyMap, chroma[postion]);
                    drawGUI(map, aXpos, aYpos); tbLog.Text += "Best Score" + pathScore.Max() + "\n";
                    tbLog.Text += "iterations" + NumberIterations + "\n";
                    break;
                }
               

                // now for something else make me some genes
                string[][] geneticPool = selection(pathScore, chroma);

                // generate that me some stuff? no i mean i want a new population 
                chroma = newPopulation(geneticPool);
                NumberIterations++;
            }

            

        }

        private string[] newPopulation(string[][] geneticPool)
        {
            string[] newChroma = new string[numberOfcandidates];
            // here we look at the terrible implementation of some cross over? i guess it should be fine.. the mutation that is
            newChroma = crossOver(geneticPool);

            newChroma = mutate(newChroma);

            return newChroma;
        }

        private string[] mutate(string[] newChroma)
        {
            for (int i = 0; i < newChroma.Length; i++)
            {
                int randomToMutate = RandomNumber(0, 100);
                int mutationChance = (int)Math.Round(constmutationChance * 100);
                if (randomToMutate < mutationChance)
                {
                    int postion = RandomNumber(0, chromaLenght);
                    StringBuilder sb = new StringBuilder(newChroma[i]);
                    int direction = RandomNumber(0, 3);
                    sb[postion] = (char)direction;
                    newChroma[i] = sb.ToString();
                }
            }
            return newChroma;
        }

        int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);

        }

        private string[] crossOver(string[][] geneticPool)
        {
            string[] NewChroma = new string[numberOfcandidates];
            int total = 0;
            int temp = 0;
            for (int i = 0; i < geneticPool.Length; i++)
            {
                if (Int32.TryParse(geneticPool[i][1], out temp))
                total += temp;
            }
            
            for (int i =0; i < numberOfcandidates; i++)
            {
                int postionAParent = RandomNumber(0, temp);
                int postionBParent = RandomNumber(0, temp);
                // cross half and half
                bool theSame = true;
                while (theSame)
                {
                    if (postionAParent == postionBParent)
                    {
                        postionBParent = RandomNumber(0, temp);
                    }
                    else
                    {
                        theSame = false;
                    }
                }
                // find index of parent
                int parentaPos = 0;
                int parentbPos = 0;
                int vala = 0;
                int valb = 0;


                for (int j = 0; j < geneticPool.Length; j++)
                {
                    if (Int32.TryParse(geneticPool[j][1], out vala))
                        postionAParent -= vala;

                    if (postionAParent < 0)
                    {
                        break;
                    }
                    else
                    {
                        parentaPos++;
                    }
                }

                for (int j = 0; j < geneticPool.Length; j++)
                {

                    if (Int32.TryParse(geneticPool[j][1], out valb))
                        postionBParent -= valb;
                    if (postionBParent < 0)
                    {
                        break;
                    }
                    else
                    {
                        parentbPos++;
                    }
                }
                // ratio/ratio split
                NewChroma[i] = geneticPool[parentaPos][0].Substring(0, chromaLenght / 2) + geneticPool[parentbPos][0].Substring(chromaLenght / 2,  chromaLenght / 2);
                //NewChroma[i] = geneticPool[postionAParent];
            }
            return NewChroma;
        }

        private string[][] selection(int[] pathScore,string[] chroma)
        {
            string[][] ret = new string[chroma.Length][];

            for (int i = 0; i < chroma.Length; i++)
            {
                ret[i] = new string[2];
            }

            for (int i = 0; i < pathScore.Length; i++)
            {
                ret[i][0] = chroma[i];
                ret[i][1] = pathScore[i].ToString();
            }

            return ret;
        }

        private void createIntialPopulation()
        {
            Random r = new Random();
            
            string sequence;
            for (int i = 0; i < numberOfcandidates; i++)
            {
                sequence = "";
                for (int j = 0; j < chromaLenght; j++)
                {
                    sequence += r.Next(0, 4);
                }

                chroma[i] = sequence;
            }
        }

        private async void AnimateBest(char[][] mapAnimate,string MovementSet)
        {
            int currentx = aXpos;
            int currenty = aYpos;

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
                if (mapAnimate[currenty][currentx] == '1')
                {
                    mapAnimate[currenty][currentx] = '0';
                }
                await Task.Delay(100);
                drawGUI(mapAnimate,currentx,currenty);
            }
        }

        private void drawGUI(char[][] mapdraw, int marioPostionX, int marioPostionY)
        {
            pnlDisplay.Children.Clear();
            //make the worst grid ever
            Line L;
            DrawingGroup imageDrawings = new DrawingGroup();
            for (int i = 0; i < width; i++)
            {
                L = new Line()
                {
                    Stroke = Brushes.Black,
                    X1 = 0,
                    X2 = pnlDisplay.Width,
                    Y1 = i * pnlDisplay.Width / width,
                    Y2 = i * pnlDisplay.Width / width,
                };
                pnlDisplay.Children.Add(L);
                L = new Line()
                {
                    Stroke = Brushes.Black,
                    X1 = i * pnlDisplay.Width / width,
                    X2 = i * pnlDisplay.Width / width,
                    Y1 = 0,
                    Y2 = pnlDisplay.Width
                };
                pnlDisplay.Children.Add(L);   
            }

            //itsa me mario?
            BitmapImage imgMario = new BitmapImage(new Uri("mario.png", UriKind.Relative));
            Rectangle mario = new Rectangle();
            mario.StrokeThickness = 10;
            mario.Height = pnlDisplay.Height / height;
            mario.Width = pnlDisplay.Width / width;
            mario.SetValue(Canvas.LeftProperty, marioPostionX * pnlDisplay.Width / width);
            mario.SetValue(Canvas.TopProperty, marioPostionY * pnlDisplay.Height / height);
            mario.Fill = new ImageBrush(imgMario);
            pnlDisplay.Children.Add(mario);
            
            // lets get some coins
            BitmapImage imgcoin = new BitmapImage(new Uri("coin.png", UriKind.Relative));
            // path fill code coin.Fill = new SolidColorBrush(Color.FromRgb(0, 153, 51));
            // coin code coin.Fill = new ImageBrush(imgcoin);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Rectangle coin = new Rectangle();
                    coin.StrokeThickness = 10;
                    coin.Height = pnlDisplay.Height / height -2;
                    coin.Width = pnlDisplay.Width / width -2;
                    if (mapdraw[i][j] == '2')
                    {
                        coin.SetValue(Canvas.LeftProperty, j * pnlDisplay.Width / width +1);
                        coin.SetValue(Canvas.TopProperty, i * pnlDisplay.Height / height +1);
                        coin.Fill = new SolidColorBrush(Color.FromRgb(0, 153, 51));
                        pnlDisplay.Children.Add(coin);
                        //FinalCoin = Rectangle.Union(coin, FinalCoin);
                    }
                    if (mapdraw[i][j] == '1')
                    {
                        coin.SetValue(Canvas.LeftProperty, j * pnlDisplay.Width / width + 1);
                        coin.SetValue(Canvas.TopProperty, i * pnlDisplay.Height / height + 1);
                        coin.Fill = new ImageBrush(imgcoin);
                        pnlDisplay.Children.Add(coin);
                    }
                }
            }
            
        }

        private void initaliseData()
        {
            //load the data into an arrays
            
            var filename = "Trail.Map";
            string fullData = File.ReadAllText(filename);
            map = new char[25][];
            for (int i = 0; i < width; i++)
            {
                map[i] = new char[25];
            }
            int postionCount = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i][j] = fullData[postionCount];
                    postionCount++;
                }
            }
            //tbLog.Text += fullData;

            // find A
            bool end = false;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i][j] == 'A')
                    {
                        aYpos = i;
                        aXpos = j;
                        end = true;
                        break;
                    }
                }
                if (end == true)
                {
                    break;
                }
            }
            tbLog.Text += "Location of mario: " + aXpos + " and " +aYpos + "\n";
            tbLog.Text += "Lets see what happens \n";
            //for (int i = 0; i < width; i++)
            //{
            //    for (int j = 0; j < height; j++)
            //    {
            //        tbLog.Text += map[i][j];
            //    }
            //    tbLog.Text += "\n";
            //}
        }
        
    }
}
