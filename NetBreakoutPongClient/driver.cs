using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetBreakoutPongClient
{
    class Driver
    {
        ClassicPongGameData gameData;

        public Driver()
        {
            gameData = new ClassicPongGameData();

            //Initialize my paddle
            gameData.myPaddle.Height = 20;
            gameData.myPaddle.Width = 80;
            gameData.myPaddle.Position.X = 150;
            gameData.myPaddle.Position.Y = 590;

            //Initialize opponent paddle
            gameData.oppPaddle.Height = 20;
            gameData.oppPaddle.Width = 80;
            gameData.oppPaddle.Position.X = 150;
            gameData.oppPaddle.Position.Y = 10;

            //Initialize the ball 
            gameData.gameBall.Radius = 10;
            gameData.gameBall.Position.X = 250;
            gameData.gameBall.Position.Y = 300;            

            gameData.ILost = false;
            gameData.OppLost = false;
        }        

        public ClassicPongGameData getGameData()
        {
            Random random = new Random();
            int myRandomX = random.Next(0, 500);
            int myY = 590;

            int oppRandX = random.Next(0, 500);
            int oppY = 10;

            int ballRandX = random.Next(0, 500);
            int ballRandY = random.Next(0, 600);

            gameData.myPaddle.Position.Y = myY;
            gameData.myPaddle.Position.X = myRandomX;

            gameData.oppPaddle.Position.Y = oppY;
            gameData.oppPaddle.Position.X = oppRandX;

            gameData.gameBall.Position.X = ballRandX;
            gameData.gameBall.Position.Y = ballRandY;

            return gameData;
        }   
    }
}
