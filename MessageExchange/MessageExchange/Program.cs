using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
char choice;
string availableChoices = "abcdABCD";
bool ok;
do
{
    Console.WriteLine("a)Collegati");
    Console.WriteLine("b)Manda un messaggio");
    Console.WriteLine("c)Leggi un messaggio");
    Console.WriteLine("d)Disconnettiti");
    do
    {
        ok = false;
        Console.Write("Inserisci l'opzione: ");
        choice = Console.ReadKey().KeyChar;
        if (availableChoices.Contains(choice))
        {
            ok = true;
        }
    } while (!ok);
    Console.WriteLine();
    choice = Char.ToLower(choice);
    switch (choice)
    {
        case 'a':
            RemoteConnection(socket);
            break;

        case 'b':
            if(checkSocketConnection(socket))
                SendMessage(socket);
            else
                Console.WriteLine("Socket non connessso!");
            break;

        case 'c':
            
            //    ReceiveMessage();
            break;

        case 'd':
            
            //    Disconnected();
            break;
    }
} while (choice != 'd');

void RemoteConnection(Socket socket)
{
    var remoteIp = IPAddress.Parse("192.168.1.38").Address;
    int port = 50500;
    try
    {
        socket.Connect(new IPEndPoint(remoteIp, port));
        Console.WriteLine("Collegato correttamente!");
    }
    catch (Exception exx)
    {
        Console.WriteLine($"The following error occurred: {exx.Message}");
    }
}


bool SendMessage(Socket socket)
{
    try
    {
        Console.WriteLine("Scrivi il tuo messaggio");
        string message = Console.ReadLine();
        message += "<EOF>";
        socket.Send(Encoding.ASCII.GetBytes(message));
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return false;
    }
    return true;
}

bool checkSocketConnection(Socket socket)
{
    return socket.Poll(1000, SelectMode.SelectWrite);
}