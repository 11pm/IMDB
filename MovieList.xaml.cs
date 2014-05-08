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
using System.Data;

namespace IMDB
{
    /// <summary>
    /// Interaction logic for MovieList.xaml
    /// </summary>
    public partial class MovieList
    {
        public MovieList()
        {
            InitializeComponent();
            listGrid.CanUserResizeColumns = false;
            listGrid.ItemsSource = list();
        }
        private List<ListData> list()
        {
            Dictionary<string, int> list = Database.MovieList(int.Parse(User.Info["userID"]));
            //List of class objects
            List<ListData> items = new List<ListData>();
            foreach (var item in list)
            {
                MovieData data = new MovieData(item.Key, 'i');
                //format object and add to lit
                items.Add(new ListData()
                {
                    Title   = MovieData.Info["title"],
                    Runtime = MovieData.Info["runtime"],
                    yourRating = item.Value.ToString(),
                    imdbRating = MovieData.Info["imdbrating"],
                    Genre = MovieData.Info["genre"],
                    year = MovieData.Info["year"],
                    Type    = MovieData.Info["type"]
                });
            }
            return items;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

      

     

       
    }
}
