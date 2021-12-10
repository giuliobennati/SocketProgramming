using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ExecuteServer();
        }

        static void ExecuteServer()
        {
            var IpAddress = IPAddress.Parse("192.168.1.38");
            Socket listener = new Socket(IpAddress.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            Console.WriteLine("CIAO");
            try
            {
                listener.Bind(new IPEndPoint(IpAddress,50500));
                listener.Listen(10);
                Socket clientConnection = listener.Accept();
                while (true)
                {
                    Console.WriteLine("Aspettando arrivo di messaggi:");
                    byte[] bytes = new byte[1024];
                    string message = "";
                    while (true)
                    {
                        int numbyte = clientConnection.Receive(bytes);
                        message += Encoding.ASCII.GetString(bytes,0,numbyte);
                        if (message.IndexOf("<EOF>") > -1)
                        {
                            Console.WriteLine(message.Remove(message.IndexOf("<EOF>")));
                            break;
                        }
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }

}
