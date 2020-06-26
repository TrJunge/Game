using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Connection : MonoBehaviour
{
        static int port = 8008; 
        static string address = "25.90.235.5";

    public static string position = string.Empty;
    //public static string lobbies = string.Empty;
    //public static string settings = string.Empty;
    //public static string wordbooks = string.Empty;
    //public static string authoriz = string.Empty;
    //public static string reg = string.Empty;
    //public static int countInLObby=0;
    //public static string leaver = string.Empty;
    //public static string changeTeam = string.Empty;
    //public static string infoTeam = string.Empty;
    //public static string playerinfo = string.Empty;
    //public static string start = string.Empty;
    //public static string statistic = string.Empty;
    //public static string authDefault = string.Empty;
    //public static string lobbyIsFull = string.Empty;
    //public static string createLobby = string.Empty;
    //public static string exitAll = string.Empty;
    //public static string roundStart = string.Empty;
    //public static string word = string.Empty;
    //public static string endGame = string.Empty;

    public static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public static void Connect()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                socket.BeginConnect(ipPoint, AsyncCompleted, "");
            }
            catch (Exception ex)
            {

            }
        }

        static void AsyncCompleted(IAsyncResult resObj)
        {
            string mes = (string)resObj.AsyncState;
            //statistic = mes;
        }

        public static void Close()
        {
            if(socket.Connected!=false)
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public class StateObject
        {
            public Socket workSocket = null;
            public const int BufferSize = 4096;
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder sb = new StringBuilder();
        }
        public static void Send(string k)
        {
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(k);
                socket.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, socket);
            }
            catch (Exception ex)
            {

            }
        }
    public static void SendPos(string k)
    {
        try
        {
            byte[] byteData = Encoding.UTF8.GetBytes(k);
            socket.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, socket);
        }
        catch (Exception ex)
        {

        }
    }
    public static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Receive();
            }
            catch (Exception e)
            {

            }
        }
        public static void Receive()
        {
            try
            {
                StateObject state = new StateObject
                {
                    workSocket = socket
                };
                socket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
            }
            catch (Exception e)
            {

            }
        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                int bytesRead = client.EndReceive(ar);
                string content = string.Empty;
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, state.buffer.Length)).ToString().TrimEnd();
                content = state.sb.ToString().TrimEnd();
                content = content.TrimEnd(content[content.Length-1]);
            if (content.EndsWith("pos"))
            {
                content = content.Substring(0, content.Length - 3);
                position = content;
            }
            // else
            // if (content.EndsWith("lobbies"))
            // {
            //     content = content.Substring(0, content.Length - 7);
            //     lobbies = content;
            // }
            // else
            // if (content.EndsWith("info"))
            // {
            //     content = content.Substring(0,content.Length-4);
            //     settings = content;
            // }
            // if (content.EndsWith("give wordbooks"))
            // {
            //     content = content.Substring(0, content.Length - 15);
            //     wordbooks = content;
            // }
            // if (content.EndsWith("reg"))
            // {
            //     reg = content.Substring(0, content.Length-3);
            // }
            // if (content.EndsWith("auth"))
            // {
            //     authoriz = content.Substring(0, content.Length - 4);
            // }
            // if (content.EndsWith("countInLobby"))
            // {
            //     countInLObby = int.Parse(content.Substring(0, content.Length - 12));
            // }
            // if (content.EndsWith("Exit"))
            // {
            //     leaver = content.Substring(0,content.Length-4);
            // }
            // if (content.EndsWith("team1") || content.EndsWith("team2"))
            // {
            //     changeTeam = content;
            // }
            // if (content == "full")
            // {
            //     lobbyIsFull = "true";
            // }
            // if (content.EndsWith("teamInfo"))
            // {
            //     infoTeam = content.Substring(0, content.Length - 8);
            //     if(lobbyIsFull=="")
            //     lobbyIsFull = "false";
            // }
            // if (content.EndsWith("playerinfo"))
            // {
            //     content = content.Substring(0, content.Length - 10);
            //     playerinfo = content;
            // }
            // if (content.EndsWith("start"))
            // {
            //     start = content;
            // }
            // if (content.EndsWith("authDefault"))
            // {
            //     authDefault = content.Substring(0, content.Length- "authDefault".Length);
            // }

            // if (content.EndsWith("CreateLobby"))
            // {
            //     createLobby = content.Substring(0, content.Length-"CreateLobby".Length);
            // }
            // if (content=="ExitAll")
            // {
            //     if (exitAll != "exitAll")
            //         exitAll = "exitAll";
            //     else exitAll = "";
            // }
            // if (content.EndsWith("roundStart"))
            // {
            //     roundStart = content;
            // }
            // if (content.EndsWith("word"))
            // {
            //     if (content.Split(',').Length == 2)
            //         word = content.Split(',')[0];
            //     else word = content.Split(',')[0] + "," + content.Split(',')[1];
            // }
            // if (content.EndsWith("endGame"))
            // {
            //     endGame = content;
            // }
            Receive();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }
    }

