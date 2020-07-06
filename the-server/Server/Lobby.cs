using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Lobby
    {
        public string name;
        public int countPlayers;
        public string teamName1;
        public string teamName2;
        //public Team team1;
        //public Team team2;
        public int time;
        public int score;
        public List<Player> players = new List<Player>();
        public Player creator;


        public Lobby(string name, string nameLibrary, int countPlayers, /*Team team1, Team team2,*/ int time, int wordsForWin, bool minus, Player creator)
        {
            this.name = name;
            this.countPlayers = countPlayers;
            this.time = time;
            //this.team1 = team1;
            //this.team2 = team2;
            this.creator = creator;
           //unusedWords = Database.getWordsFormWordbook(nameLibrary);
        }

        public string info()
        {
            return name + "," + countPlayers.ToString() + "," + time.ToString();
        }

        public void addInLobby(Player player)
        {
            players.Add(player);
        }

        public void removeInLobby(Player player)
        {
            players.Remove(player);
        }

        public void changeCreator()
        {
            creator = players[0];
        }

        //public void subscribeTeam(Team team, string nick)
        //{
        //    foreach (Player player in players)
        //    {
        //        if (player.nickname == nick) team.subscribe(player);
        //    }
        //}

        //public void unsubscribeTeam(Team team, string nick)
        //{
        //    foreach (Player player in players)
        //    {
        //        if (player.nickname == nick) team.unsubscribe(player);
        //    }
        //}

        //public string TeamInfo()
        //{
        //    return team1.playerInTeam() + "," + team2.playerInTeam() + "teamInfo";
        //}
    }
}
