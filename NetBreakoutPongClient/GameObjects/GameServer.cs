using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace NetBreakoutPongClient
{
    public class GameServer
    {
        TcpClient client;
        NetworkStream stream;
        byte[] inBuffer = new byte[1024];
        string data;
        string encoding;

        public GameServer()
        {
            client = new TcpClient();
            client.Connect(Constants.SERVER_IP, Constants.SERVER_PORT);
            stream = client.GetStream();
        }

        public ClassicPongGameData GetData()
        {
            int size = stream.Read(inBuffer, 0, inBuffer.Length);
            data = Encoding.ASCII.GetString(inBuffer, 0, size);
            return JsonConvert.DeserializeObject<ClassicPongGameData>(data);
        }

        
        public void SendKeypress(Keypress keypress)
        {
            encoding = JsonConvert.SerializeObject(keypress);
            stream.Write(Encoding.ASCII.GetBytes(encoding), 0, encoding.Length);
        }

        public void Dispose()
        {
            stream.Close();
            client.Close();
        }

    }
}
