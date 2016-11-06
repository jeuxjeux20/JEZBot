using System;
using System.IO;
using System.Web;
using System.Net;
using Newtonsoft.Json;
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

namespace EZBotWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
         App.Bot bot = new App.Bot();
        private void start_Click(object sender, RoutedEventArgs e)
        {

            if (sayCmdEnable.IsChecked == true)
            {
                App.Bot.SayCmd = true;
            } else { App.Bot.SayCmd = false; }
            if (catCmdEnable.IsChecked == true)
            {
                App.Bot.CatCmd = true;
            } else { App.Bot.CatCmd = false; }
            
            bot.Start(Token.Text,Convert.ToChar(prefix.Text));
        }
        private void sayCmdEnable_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            WebRequest request = WebRequest.Create("http://random.cat/meow");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            dynamic catparsed = JsonConvert.DeserializeObject(responseFromServer);
            string file = catparsed.file;
            MessageBox.Show(file);
        }
    }
}
