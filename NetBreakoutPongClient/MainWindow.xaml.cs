using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

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

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

            Thread myThread = new Thread(Gameplay);
            myThread.IsBackground = true;
            myThread.Start();


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

        public void Gameplay()
        {
            PaintGame(server.GetData());

            while (gameData.ILost == false && gameData.OppLost == false)
            {
                server.SendKeypress(new Keypress(leftKeyPressed, rightKeyPressed));
                leftKeyPressed = rightKeyPressed = false;
                gameData = server.GetData();
                PaintGame(gameData);
            }
        }

        public void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            leftKeyPressed = rightKeyPressed = false;
            if (Keyboard.IsKeyDown(Key.Left))
            {
                leftKeyPressed = true;
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                rightKeyPressed = true;
            }
        }

        private void PaintGame(ClassicPongGameData data)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
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
            }));
        }
    }
}
