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
using MySql.Data.MySqlClient;

namespace Projet_BDD_WPF
{
    /// <summary>
    /// Logique d'interaction pour Connection.xaml
    /// </summary>
    public partial class Connection : Window
    {
        public Connection()
        {
            InitializeComponent();
        }
        private void clickco(object sender, RoutedEventArgs e)
        {
            if (email.Text != "")
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command2 = connection.CreateCommand();
                command2.CommandText = "select mot_de_passe_client from client where courriel_client = @emailtext";
                command2.Parameters.AddWithValue("@emailtext", email.Text);
                MySqlDataReader reader;
                reader = command2.ExecuteReader();
                string motdep = "";
                while (reader.Read())
                {
                    motdep = reader.GetString(0);
                    Console.WriteLine(motdep);
                }
                if (motdep == mdp.Text)
                {
                    reessayer.Text = "";
                    ChoixCommande p = new ChoixCommande(email.Text);
                    p.Show();
                    this.Close();
                }
                else
                {
                    reessayer.Text = "Mot de passe incorrect ou identifiant incorrect";
                }
                connection.Close();
            }
            else
            {
                reessayer.Text = "Mot de passe incorrect ou identifiant incorrect";
            }
            

        }
    }
}
