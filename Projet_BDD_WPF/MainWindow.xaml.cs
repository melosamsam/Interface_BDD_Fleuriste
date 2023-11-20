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
using MySql.Data.MySqlClient;

namespace Projet_BDD_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            test(connection);
            Console.WriteLine("fin des opérations");
            connection.Close();
            Console.ReadLine();
        }

        private void clickconnexion(object sender, RoutedEventArgs e)
        {
            Connection p = new Connection();
            p.Show();
        }

        private void clickinscrire(object sender, RoutedEventArgs e)
        {
            Inscription p = new Inscription();
            p.Show();
        }

        private void clickpro(object sender, RoutedEventArgs e)
        {
            Espacepro p = new Espacepro();
            p.Show();
        }

        static void test(MySqlConnection connection)
        {
            //on cree un truc creation client ou creation commande?

        }
    }
}
