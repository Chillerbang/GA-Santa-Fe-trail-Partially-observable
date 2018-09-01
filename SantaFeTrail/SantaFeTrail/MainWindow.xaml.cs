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

        private static int maxScore = 55;
        private static int numGenerations = 1000;
        private static int chromaLenght = 100;

        char[][] map;
        int width = 25;
        int height = 25;
        int aXpos = 0;
        int aYpos = 0;

        
        string[] chroma = new string[numGenerations]; // limited to that of 200 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            //intialise the Game
            initaliseData();
            drawGUI();
            //start genetic algorithem
            createIntialPopulation();
            

            int pathScore = 0;
            while (pathScore < maxScore)
            {
                // test these children who are very bad
            }
        }

        private void createIntialPopulation()
        {
            Random r = new Random();
            
            string sequence;
            for (int i = 0; i < numGenerations; i++)
            {
                sequence = "";
                for (int j = 0; j < chromaLenght; j++)
                {
                    sequence += r.Next(0, 4);
                }
                chroma[i] = sequence;
                tbLog.Text += sequence + "\n";
            }
        }

        private void drawGUI()
        {
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
            mario.SetValue(Canvas.LeftProperty, aXpos * pnlDisplay.Width / width);
            mario.SetValue(Canvas.TopProperty, aYpos * pnlDisplay.Height / height);
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
                    if (map[i][j] == '2')
                    {
                        coin.SetValue(Canvas.LeftProperty, j * pnlDisplay.Width / width +1);
                        coin.SetValue(Canvas.TopProperty, i * pnlDisplay.Height / height +1);
                        coin.Fill = new SolidColorBrush(Color.FromRgb(0, 153, 51));
                        pnlDisplay.Children.Add(coin);
                        //FinalCoin = Rectangle.Union(coin, FinalCoin);
                    }
                    if (map[i][j] == '1')
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
