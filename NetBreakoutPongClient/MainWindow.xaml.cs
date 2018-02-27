using System;
using System.Collections.Generic;
using System.Linq;
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

namespace NetBreakoutPongClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClassicPongGameData gameData = new ClassicPongGameData();
        //private Driver driver = new Driver();
        private GameServer server = new GameServer();

        private Boolean leftKeyPressed = false;
        private Boolean rightKeyPressed = false;

        private Brush myPaddleColor = Brushes.Blue;
        private Brush oppPaddleColor = Brushes.Red;
        private Brush ballColor = Brushes.Yellow;

        public MainWindow()
        {
            InitializeComponent();

            gameData = server.GetData();
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

            while(gameData.ILost == false && gameData.OppLost == false)
            {
                gameData = server.GetData();
                server.SendKeypress(new Keypress(leftKeyPressed, rightKeyPressed));
            }


            //This code is for my local driver
            /*while (gameData.ILost == false && gameData.OppLost == false)
            {
                gameData = driver.getGameData();
                PaintGame(gameData);
            }
            gameData = driver.getGameData();
            PaintGame(gameData);
            
            gameData = driver.getGameData();
            PaintGame(gameData);
            gameData = driver.getGameData();
            PaintGame(gameData);
            */


        }

        public void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                leftKeyPressed = true;
            }
            else
                leftKeyPressed = false;
            if (Keyboard.IsKeyDown(Key.Right))
            {
                rightKeyPressed = true;
            }
            else
                rightKeyPressed = false;
            
        }

        private void PaintGame(ClassicPongGameData data)
        {
            Rectangle myPaddle = new Rectangle();
            myPaddle.Fill = myPaddleColor;
            myPaddle.Height = data.myPaddle.Height;
            myPaddle.Width = data.myPaddle.Width;

            Canvas.SetTop(myPaddle, (data.myPaddle.Position.Y - (data.myPaddle.Height / 2)));
            Canvas.SetLeft(myPaddle, (data.myPaddle.Position.X - (data.myPaddle.Width / 2)));

            Rectangle oppPaddle = new Rectangle();
            oppPaddle.Fill = oppPaddleColor;
            oppPaddle.Height = data.oppPaddle.Height;
            oppPaddle.Width = data.oppPaddle.Width;

            Canvas.SetTop(oppPaddle, (data.oppPaddle.Position.Y - (data.oppPaddle.Height / 2)));
            Canvas.SetLeft(oppPaddle, (data.oppPaddle.Position.X - (data.oppPaddle.Width / 2)));

            Ellipse gameBall = new Ellipse();
            gameBall.Width = data.gameBall.Radius * 2;
            gameBall.Height = data.gameBall.Radius * 2;
            gameBall.Fill = ballColor;

            Canvas.SetTop(gameBall, (data.gameBall.Position.Y - (data.gameBall.Radius)));
            Canvas.SetLeft(gameBall, (data.gameBall.Position.X - (data.gameBall.Radius)));

            if (gameCanvas.Children.Count > 0)
            {
                gameCanvas.Children.Clear();
            }

            gameCanvas.Children.Add(myPaddle);
            gameCanvas.Children.Add(oppPaddle);
            gameCanvas.Children.Add(gameBall);

        }
    }
}
