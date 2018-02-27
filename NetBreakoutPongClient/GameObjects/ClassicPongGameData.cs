using System;

namespace NetBreakoutPongClient
{
    public struct ClassicPongGameData
    {
        public bool ILost;
        public bool OppLost;

        public BallData gameBall;

        public PaddleData myPaddle;
        public PaddleData oppPaddle;


        public struct PaddleData
        {
            public Location Position;
            public int Width;
            public int Height;
        }

        public struct BallData
        {
            public Location Position;
            public int Radius;
        }
        
    }
}
