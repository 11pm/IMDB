using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace IMDB
{
    class Database
    {
        
        #region Strings
        private static string server,database,uid,password;
        static string conString = null;
        static string Query = null;

        static MySqlConnection SqlConnection;
        static MySqlCommand SQLCommand;
        static MySqlDataReader SQLReader = null;
        #endregion
        //Only need to init once, data stays alive
        public static void Connect()
        {
            server = "127.0.0.1";
            database = "alexander_movielist";
            uid = "root";
            password = "";

            conString = "server=" + server + ";userid=" + uid + ";password=" + password + ";database=" + database;
            SqlConnection = new MySqlConnection(conString);
        }
        private static bool OpenConnection()
        {
            try
            {
                SqlConnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
        }
        //espace string
        public static string MySQLEscape(string str)
        {
            return Regex.Replace(str, @"[\x00'""\b\n\r\t\cZ\\%_]",
                delegate(Match match)
                {
                    string v = match.Value;
                    switch (v)
                    {
                        case "\x00":            // ASCII NUL (0x00) character
                            return "\\0";
                        case "\b":              // BACKSPACE character
                            return "\\b";
                        case "\n":              // NEWLINE (linefeed) character
                            return "\\n";
                        case "\r":              // CARRIAGE RETURN character
                            return "\\r";
                        case "\t":              // TAB
                            return "\\t";
                        case "\u001A":          // Ctrl-Z
                            return "\\Z";
                        default:
                            return "\\" + v;
                    }
                });
        }
        //0 ROWS = nothing found = false; else = true
        public static bool Login(string username, string password)
        {
            if (OpenConnection())
            {
                Query = "SELECT * FROM user WHERE username = '" + username + "' AND password = '" + password + "'";
                SQLCommand = new MySqlCommand(Query,SqlConnection);
                SQLReader = SQLCommand.ExecuteReader();
                CloseConnection();
                if (SQLReader.HasRows) return true;
                else return false;
            }
            return false;
        }
        //-11-
        public static bool CheckMovie(string userid, string imdbid)
        {
            if (OpenConnection())
            {
                Query = "SELECT * FROM movies WHERE userID = '" + userid + "' AND imdbID = '" + imdbid + "'";
                SQLCommand = new MySqlCommand(Query, SqlConnection);
                SQLReader = SQLCommand.ExecuteReader();
                CloseConnection();
                if (SQLReader.HasRows) return true;
                else return false;
            }
            return false;
        }
        public static void AddMovie(int userID, string imdbId, int rating)
        {
            if (OpenConnection())
            {
                Query = "INSERT INTO movies (userID, imdbID, rating) VALUES (?uid,?imdb,?rating)";
                SQLCommand = new MySqlCommand(Query, SqlConnection);
                SQLCommand.Parameters.AddWithValue("?uid", userID);
                SQLCommand.Parameters.AddWithValue("?imdb", imdbId);
                SQLCommand.Parameters.AddWithValue("?rating", rating);
                SQLCommand.ExecuteNonQuery();
                CloseConnection();
            }
        }
        public static Dictionary<string, int> MovieList(int userid)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            if (OpenConnection())
            {
                Query = "SELECT * FROM movies WHERE userID = '" + userid + "'";
                SQLCommand = new MySqlCommand(Query, SqlConnection);
                SQLReader = SQLCommand.ExecuteReader();
                while (SQLReader.Read())
                {
                    temp.Add(SQLReader.GetValue(1).ToString(), int.Parse(SQLReader.GetValue(2).ToString()));
                }
                CloseConnection();
                return temp;
            }
            return temp;
        }
        //return List of data from desired user
        public static List<string> UserData(string user)
        {
            List<string> temp = new List<string>();
            if (OpenConnection())
            {
                Query = "SELECT * FROM user WHERE username = '" + user + "'";
                SQLCommand = new MySqlCommand(Query, SqlConnection);
                SQLReader = SQLCommand.ExecuteReader();
                while (SQLReader.Read())
                {
                    for (int i = 0; i < SQLReader.FieldCount; i++)
                    {
                        temp.Add(SQLReader.GetValue(i).ToString());
                    }
                }
                CloseConnection();
                return temp;
            }
            return temp;
        }
        private static bool CloseConnection()
        {
            try
            {
                SqlConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
        }
    }
}
