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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace IMDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MovieFinder
    {
        /* OMDBAPI.COM
         * REQUEST
         * ?S = SEARCH
         * ?T = MOVIE DATA
         * ?Y = YEAR
         * ?I = ID
         * ?r = response type
         * ?plot = short/full | default short
         */
        private string Search { get; set; }
        
        public MovieFinder()
        {
            InitializeComponent();
            results.Visibility = Visibility.Hidden;
            Search = "";
            searchTxt.Focus();
            //searchTxt.Text = "tengen toppa gurren lagann";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Open();
        }
        private void Open()
        {
            if (Search.Length > 1)
            {
                try
                {
                    MovieWindow win = new MovieWindow(searchTxt.Text);
                    this.Close();
                    win.Show();
                }
                catch (Exception e) { MessageBox.Show("Sorry, the movie could not be found :("); }
            }
            else MessageBox.Show("Search string must be over two characters");
        }
        //Live search; only show listbox when over two chars
        private void searchTxt_KeyUp(object sender, KeyEventArgs e)
        {
            Search = searchTxt.Text;
            //API only returns data when ?s is over two chars
            if(Search.Length > 1){
                results.Visibility = Visibility.Visible;
                results.Items.Clear();
                //Class to get search api and return xdoc
                Search data = new Search(Search);
                //Todo get linq data from Search class
                var q = from c in data.doc.Descendants("Movie")
                        select (string)c.Attribute("Title");
                foreach (string name in q)
                {
                    results.Items.Add(name);   
                }
            }
        }
        

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Open(); 
        }

        private void results_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (results.SelectedIndex != -1)
            {

            try
            {
                searchTxt.Clear();
                searchTxt.Text = results.SelectedItem.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            }
        }

        private void searchTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            Search = searchTxt.Text;
            searchTxt.Clear();

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }
        
      
    }
}
