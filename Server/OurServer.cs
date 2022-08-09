using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    class OurServer
    {
        TcpListener server;

        public OurServer()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);
            server.Start();

            LoopClients();  
        }

        void LoopClients()
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                Thread thread = new Thread(() => HandleClient(client));
                thread.Start();
            }
        }

        void HandleClient(TcpClient client)
        {
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);

            while (true)
            {
                string message = sReader.ReadLine();
                Console.WriteLine($"Клиент написал - {message}");

                Console.WriteLine("Дайте сообщение клиенту: ");
                string answer = Console.ReadLine();
                sWriter.WriteLine(answer);
                sWriter.Flush();
            }
        }
    }
}