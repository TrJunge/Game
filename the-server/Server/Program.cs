using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int buffersize = 4096;
        public byte[] buffer = new byte[buffersize];
        public StringBuilder sb = new StringBuilder();
    }

    public class Server
    {
        public static string answer = string.Empty;
        static public string[] ClientData = new string[7];
        static public List<Socket> clients = new List<Socket>();
        static byte[] buf = new byte[1024];
        static StringBuilder sb = new StringBuilder();


        static void Main(string[] args)
        {
            IPEndPoint IPAndPort = new IPEndPoint(IPAddress.Parse("25.90.235.5"), 8008);
            Socket Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Server.Bind(IPAndPort);
                Server.Listen(200);
                Console.WriteLine("Ожидаю подключения...");
                for (int i = 0; i < 200; i++)
                    Server.BeginAccept(new AsyncCallback(AsyncAccept), Server);
            }
            catch (Exception ex) { Console.WriteLine("Main" + ex.Message); }
            Console.ReadLine();
        }
        static void AsyncAccept(IAsyncResult res)
        {
            try
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    if (clients[i] != null && !clients[i].Connected) clients.Remove(clients[i]);
                }
                StateObject client = new StateObject();
                Socket socket = (Socket)res.AsyncState;
                client.workSocket = socket.EndAccept(res);
                client.buffer = new byte[client.buffer.Length];
                if (!clients.Contains(socket))
                {
                    Console.WriteLine($"Подключился {clients.Count + 1} пользователь: " + client.workSocket.RemoteEndPoint);
                    Receive(client.workSocket);
                    clients.Add(client.workSocket);
                }
            }
            catch (Exception ex) { Console.WriteLine("asyncaccept" + ex.ToString()); }
        }
        static void Receive(Socket client)
        {
            try
            {
                if (client.Connected)
                {
                    StateObject state = new StateObject();
                    state.workSocket = client;
                    client.BeginReceive(state.buffer, 0, StateObject.buffersize, 0, new AsyncCallback(AsyncRead), state);
                }
            }
            catch (Exception ex) { Console.WriteLine("receive" + ex.ToString()); }
        }
        static Socket CheckSoket;
        static void AsyncRead(IAsyncResult res)
        {
            try
            {
                StateObject clientbuf = (StateObject)res.AsyncState;
                Socket sock = clientbuf.workSocket;
                string content = string.Empty;
                if (!sock.Connected)
                {
                    CheckSoket = sock;
                    throw new SocketException();
                }
                int bytesRead = sock.EndReceive(res);
                clientbuf.sb.Append(Encoding.UTF8.GetString(clientbuf.buffer, 0, clientbuf.buffer.Length));
                content = clientbuf.sb.ToString().TrimEnd();
                content = content.TrimEnd(content[content.Length - 1]);
                if (content != "")
                {
                    Console.WriteLine("Сообщение от " + clientbuf.workSocket.RemoteEndPoint + ": " + content + ".");
                    if (answer.EndsWith("pos"))
                    {
                        string[] positions = answer.Split(new char[] {'p', 'o','s'});
                        foreach (string position in positions)
                        {
                            Send(clientbuf.workSocket, position.Trim(new char[] { '(', ' ', ')' })+"pos");
                        }
                    }
                    if (answer.EndsWith("auth"))
                    {
                        
                    }
                    Send(clientbuf.workSocket, answer);
                }
            }
            catch (SocketException)
            {
                clients.Remove(CheckSoket);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        public static void Send(Socket s, String m)
        {
            try
            {
                Console.WriteLine($"Сообщение отправлено клиенту {s.RemoteEndPoint}: {m}.");
                byte[] ToClient = Encoding.UTF8.GetBytes(m);
                s.BeginSend(ToClient, 0, ToClient.Length, 0, new AsyncCallback(SendCallBack), s);
            }
            catch (Exception ex) { Console.WriteLine("Send" + ex.ToString()); }
        }
        static void SendCallBack(IAsyncResult res)
        {
            try
            {
                StateObject client = new StateObject();
                Socket ClientSocket = (Socket)res.AsyncState;
                client.workSocket = ClientSocket;
                int bytetoclient = ClientSocket.EndSend(res);
                Receive(client.workSocket);
            }
            catch (Exception ex) { Console.WriteLine("sendcallBack" + ex.ToString()); }
        }
    }
}
