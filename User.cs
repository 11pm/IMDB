using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB
{
    class User
    {
        public static string Username { get; set; }
        //Make content addable
        public static Dictionary<string, string> Info {
            get {
                return _info;
            }
            private set { _info = value; }
        }
        //Init dictionary
        private static Dictionary<string, string> _info;
        private string Password { get; set; }
        //Init List
        private static List<string> userData = null;
        public User(string username, string password)
        {
            Username = username;
            Password = password;
            //Load list
            userData = Database.UserData(username);
            //Load Dictionary
            Info = new Dictionary<string, string>()
            {
                {"userID",          userData[0]},
                {"username",        userData[1]},
                {"password",        userData[2]},
                {"account_created", userData[3]}
            };
        }
        //returns true:false from sql
        public bool login()
        {
            return Database.Login(Username,Password);
        }
    }
}
