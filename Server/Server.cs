using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class Server
    {
        private string ip;
        private int port;
        private TcpListener tcpListener;
        private Thread thread;


        public Server(string ip)
        {
            IPEndPoint iplocal = new IPEndPoint(IPAddress.Any, 8080);
            tcpListener = new TcpListener(iplocal);
            thread = new Thread(LoopGetData);
            InfoServer.Ip = iplocal;
        }

        public bool IsEnable { get; private set; }
        public List<Client> Clients { get; private set; } = new List<Client>();


        public void Start()
        {
            IsEnable = true;
            tcpListener.Start();
            thread.Start();
            InfoServer.ServerStartTime = DateTime.Now;
        }

        public void Stop()
        {
            IsEnable = false;
            Console.WriteLine("Сервер выключен");
        }

        private void LoopGetData()
        {
            while (IsEnable)
            {
                try
                {
                    var clientSocket = tcpListener.AcceptTcpClient();
                    var client = new Client(clientSocket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка при подключении клиента к серверу");
                    Console.WriteLine($"Исключение: {ex.Message}");
                    break;
                }
            }
        }

        public void SendMessage(Client author, string message)
        {
            foreach (var client in Clients)
            {
                if (client == author)
                    continue;

                client.SendMessage(author, message);
            }
        }

        public void InfoClient()
        {
            if (Clients.Count == 0)
            {
                Console.WriteLine("В данный момент на сервере нет подключенных пользователей");
                return;
            }
            Console.WriteLine("В данный момент на сервере:");
            foreach (var client in Clients)
            {
                Console.WriteLine($"{client.Name} ({client.Ip})  {client.FirstConnectTime}");
            }
        }

        public void SendMessage(string message)
        {
            foreach (var client in Clients)
            {
                client.SendMessage("Сервер", message);
            }
        }

        public void Ban(string name)
        {
            try
            {
                foreach (var user in Clients)
                {
                    if (user.Name == name)
                    {
                        BanMessage(user);
                        Disconnect(user);
                        user.Disconnect();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        private void BanMessage(Client client)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Clients.Remove(client);
            Console.WriteLine($"пользователь забанен {client}");
            Console.ResetColor();
        }

        public void Disconnect(Client client)
        {
            Clients.Remove(client);
            client.Disconnect();
        }

    }
}
