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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.Connect();
            userTxt.Focus();
        }
        private void login()
        {
            //Validation
            string user = userTxt.Text;
            string pass = passTxt.Password;
            if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(pass)) MessageBox.Show("Please fill in the fields");
            else //Ready to log in
            {
                try
                {
                    //Initialize User class with userdata to use anywhere
                    //Þetta með index dótið var bug, var að reyna að ná í gögn úr gagnagrunni þótt að login-ið virkaði ekki. fyrirgefðu var ekki búinn að testa þetta
                    //er samt búinn að laga það
                    try
                    {
                        User login = new User(user, MD5(pass));
                        if (login.login())
                        {
                            Home hom = new Home();
                            hom.Show();
                            this.Close();
                        }
                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Username/Password is not correct");
                    }
                    
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            login();
        }
        //String to MD5 hash
        public string MD5(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs) s.Append(b.ToString("x2").ToLower());
            return s.ToString();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) login();
        }
    }
}