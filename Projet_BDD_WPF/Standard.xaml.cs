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
    /// Logique d'interaction pour Standard.xaml
    /// </summary>
    public partial class Standard : Window
    {
        String Email;
        public Standard(String email)
        {
            InitializeComponent();
            this.Email = email;

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT * from standards ;";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string listee="";
            while (reader.Read())// parcourt ligne par ligne
            {
                for (int i = 0; i < 4; i++) {
                    listee += reader.GetString(i) + "            ";  // récupération de la ième colonne
                }
                listee += '\n';
                listee += '\n';
            }
            connection.Close();
            liste.Text =listee;
        }
        private void clickchoix(object sender, RoutedEventArgs e)
        {
            if (choix.Text != "")
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " SELECT prix_bouquet_standards from standards where @value=nom_bouquet;";
                command.Parameters.AddWithValue("@value", choix.Text);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string pri = "";
                while (reader.Read())// parcourt ligne par ligne
                {
                    pri += reader.GetString(0);
                }
                connection.Close();
                if (pri != "")
                {
                    reessayer.Text = "";
                    Commander p = new Commander(this.Email,choix.Text,pri,"standard");
                    p.Show();
                    this.Close();
                }
                else
                {
                    //ecrire reessayer
                    reessayer.Text = "Le nom saisi est incorrect, veuillez réessayez svp";
                }
            }
            else
            {
                //ecrire reessayer
                reessayer.Text = "Le nom saisi est incorrect, veuillez réessayez svp";
            }
        }

    }
}
