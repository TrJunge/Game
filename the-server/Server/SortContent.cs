using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class SortContent
    {
        public string HandThis(string message, Socket client)
        {
            if (message.EndsWith("pos"))
            {
                string[] positions = message.Split(new char[] { 'p', 'o', 's' });
                    return positions[positions.Length-1].Trim(new char[] { '(', ' ', ')' }) + "pos";
            }
            if (message.EndsWith("auth"))
            {
                //DataBase.Authorization;
                return "";
            }
            if (message.EndsWith("reg"))
            {
                //DataBase.Registration;
                return "";
            }
            return "nothing";
        }
    }
}
