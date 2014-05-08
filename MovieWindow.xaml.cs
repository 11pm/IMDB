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

namespace IMDB
{
    /// <summary>
    /// Interaction logic for MovieWindow.xaml
    /// </summary>
    public partial class MovieWindow
    {
        private static string Search { get; set; }
        public MovieWindow(string search)
        {
            data = new MovieData(search, 't');
            InitializeComponent();
            Display();
            for (int i = 0; i < 10; i++)
                rating.Items.Add(i + 1);
            rating.SelectedIndex = 0;
            hidden.Visibility = Visibility.Hidden;
        }
        MovieData data = null ;
        private void Display()
        {
            //Data from movie in key => value format, call MovieData.Info[key] to get value
            
            title.Content = MovieData.Info["title"];
            this.Title = MovieData.Info["title"] + " - " + MovieData.Info["year"];
            //image stuff
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(MovieData.Info["poster"]);
            myBitmapImage.EndInit();
            //set image source
            img.Source = myBitmapImage;
            string[] words = MovieData.Info["plot"].Split();
            string description = "";
            //Multiline label for plot
            for (int i = 0; i < words.Length; i++)
            {
                if (i % 13 == 0) description += Environment.NewLine + words[i];                
                else description += " " +  words[i];
            }
            plot.Content = description;

            imdbRating.Content = MovieData.Info["imdbrating"];
            extra_data.Content = MovieData.Info["type"] + " - " + MovieData.Info["rated"] + " - " + MovieData.Info["runtime"] + " - " + MovieData.Info["genre"];

            bool movie = Database.CheckMovie(User.Info["userID"], MovieData.Info["imdbID"]);
            if (movie) hide();
            else
            {
                hidden.Content = "This is on your list";
                hidden.Visibility = Visibility.Visible;
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.AddMovie(Convert.ToInt32(User.Info["userID"]), MovieData.Info["imdbID"], Convert.ToInt32(rating.Text));
                MessageBox.Show("Added to my list");
                hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void hide()
        {
            rating.Visibility = Visibility.Hidden;
            addBtn.Visibility = Visibility.Hidden;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Home hom = new Home();
            hom.Show();
            this.Close();
        }
    }
}
