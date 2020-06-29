using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    public class Player
    {
        public Socket socket;
        //public Team team;
        public string nickname;

        public Player(Socket socket, string nickname)
        {
            this.socket = socket;
            this.nickname = nickname;
        }

        /*public void subscribeToTeam(Team team)
        {
            this.team = team;
        }*/

        //public string printInfoTeam()
        //{
        //    return team.printName();
        //}

        public string printPlayerInfo()
        {
            return nickname + ","; //+ team.name;
        }
    }
}
