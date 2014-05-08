using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;

namespace IMDB
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        private static string Username { get; set; }
        public Home()
        {
            InitializeComponent();
            username.Content = User.Info["username"];
            //var ci = System.Globalization.CultureInfo.GetCultureInfo("en-us");
            var account_created = User.Info["account_created"];
            var account_create = account_created.Split(' ');
            var dateyy = account_create[0].Split('.');
            var dateHH = account_create[1].Split(':');
            var datetime = DateTime.Now.ToString("yyyy,MM,dd,HH,mm,ss");
            var datetim = datetime.Split(',');
            DateTime a = new DateTime(int.Parse(dateyy[2]), int.Parse(dateyy[1]), int.Parse(dateyy[0]), int.Parse(dateHH[0]), int.Parse(dateHH[1]), int.Parse(dateHH[2]));
            DateTime b = new DateTime(int.Parse(datetim[0]), int.Parse(datetim[1]), int.Parse(datetim[2]), int.Parse(datetim[3]), int.Parse(datetim[4]), int.Parse(datetim[5]));
            var asd = b.Subtract(a).TotalMinutes;
            TimeSpan span = TimeSpan.FromMinutes(asd);
            var time = span.ToString().Split('.');
            created.Content = "User for " + time[0] + " days";
            /*data = Database.UserData(username);
            usernameLbl.Content = username;*/
            //MessageBox.Show(User.Username);
        }
   

        private void movie_Click(object sender, RoutedEventArgs e)
        {
            MovieFinder mf = new MovieFinder();
            mf.Show();
            this.Close();
        }

        private void mlist_Click(object sender, RoutedEventArgs e)
        {
            MovieList mlist = new MovieList();
            mlist.Show();
            this.Close();
        }
    }
}
