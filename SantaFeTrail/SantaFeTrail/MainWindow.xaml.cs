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
    public partial class MainWindow : Window
    {
        char[][] map;
        int width = 25;
        int height = 25;

        string[] chroma = new string[200]; // limited to that of 200 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            //intialise the Game
            initalise();

        }

        private void initalise()
        {
            //load the data into an arrays
            
            var filename = "c:\\Trail.Map";
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
            tbLog.Text += fullData;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tbLog.Text += map[i][j];
                }
                tbLog.Text += "\n";
            }
        }
        
    }
}
