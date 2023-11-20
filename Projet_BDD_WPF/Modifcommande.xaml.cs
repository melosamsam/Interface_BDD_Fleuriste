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
    /// Logique d'interaction pour Modifcommande.xaml
    /// </summary>
    public partial class Modifcommande : Window
    {
        public Modifcommande()
        {
            InitializeComponent();
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT * from commande ;";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string listee = "";
            while (reader.Read())// parcourt ligne par ligne
            {
                for (int i = 0; i < 11; i++)
                {
                    listee += reader.GetString(i) + "        ";  // récupération de la ième colonne
                }
                listee += '\n';
            }
            listecommande.Text = listee;
            connection.Close();
        }
        private void clickchoix(object sender, RoutedEventArgs e)
        {
            if (choix.Text != "")
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " SELECT etat_commande from commande where @value=num_commande;";
                command.Parameters.AddWithValue("@value", choix.Text);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string etat = "";
                while (reader.Read())// parcourt ligne par ligne
                {
                    etat += reader.GetString(0);
                }
                connection.Close();
                if (etat != "")
                {
                    reessayer.Text = "";
                    Gestioncommande p = new Gestioncommande(choix.Text, etat);
                    p.Show();
                    this.Close();
                }
                else
                {
                    //ecrire reessayer
                    reessayer.Text = "Le numéro saisi est incorrect, veuillez réessayez svp";
                }
            }
            else
            {
                //ecrire reessayer
                reessayer.Text = "Le numéro saisi est incorrect, veuillez réessayez svp";
            }
        }
    }
}
