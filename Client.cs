using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;

namespace Server
{
    public class Client
    {
        private TcpClient connection;
        private NetworkStream stream;
        private Thread thread;
        private System.Timers.Timer timer;

        public Client(TcpClient tcp)
        {
            IsEnable = true;
            connection = tcp;
            Ip = ((IPEndPoint)tcp.Client.RemoteEndPoint).Address.ToString();
            stream = tcp.GetStream();
            thread = new Thread(LoopReceiveMessage);
            thread.Start();

            timer = new System.Timers.Timer(2000);
            timer.AutoReset = true;
            timer.Elapsed += CheckLastActivity;
            timer.Start();
        }

        public bool IsEnable { get; private set; }
        public string Ip { get; private set; }
        public string Name { get; private set; }
        public DateTime LastActivity { get; private set; }
        public DateTime FirstConnectTime { get; set; }


        private void LoopReceiveMessage()
        {
            // Сразу после подключения, считываем данные имени пользователя
            Name = GetName();

            // После получения имени, проверяем имя на уникальность
            foreach (var item in Server.server.Clients)
            {
                if (item.Name == Name)
                {
                    Disconnect("Пользователь с таким именем существует");
                    return;
                }
            }

            // Если имя уникально, то добавляем в коллекцию подключенных клиентов
            Server.server.Clients.Add(this);

            FirstConnectTime = DateTime.Now;
            Console.WriteLine($"{Name} подключился к серверу ({Ip})");

            while (IsEnable)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    if (bytes == 0)
                    {
                        continue;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, bytes);
                    if (message.Length == 1 && buffer[0] == 0)
                    {
                        LastActivity = DateTime.Now;
                    }
                    else
                    {
                        // Выводим на экран полученное сообщение
                        ShowMessage(message);
                        Server.server.SendMessage(this, message);
                    }
                }
                catch (Exception)
                {
                    if (IsEnable)
                        Disconnect();
                }
            }
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine($"{Name}: {message}");
        }

        public void SendMessage(Client author, string message)
        {
            Send($"{author.Name}: {message}");
        }

        public void SendMessage(string serverName, string message)
        {
            Send($"{serverName}: {message}");
        }

        private void Send(string text)
        {
                byte[] bufferSend = Encoding.UTF8.GetBytes(text);
                stream.Write(bufferSend, 0, bufferSend.Length);
                stream.Flush();
        }

        public void Disconnect(string text = null)
        {
            if (text != null)
                Send(text);

            IsEnable = false;
            Console.WriteLine($"{Name} disconnect");
            Server.server.Clients.Remove(this);
            connection.Close();
            connection.Dispose();
            timer.Stop();
        }

        private void CheckLastActivity(object sender, ElapsedEventArgs e)
        {
            var diff = DateTime.Now.Subtract(LastActivity).TotalSeconds;
            if (diff > 5)
            {
                Disconnect();
            }
        }

        private string GetName()
        {
            byte[] bufferName = new byte[1024];
            int bytesName = stream.Read(bufferName, 0, bufferName.Length);
            string name = Encoding.UTF8.GetString(bufferName, 0, bytesName);

            return name;
        }
    }
}
