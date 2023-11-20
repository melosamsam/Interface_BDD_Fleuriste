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
    /// Logique d'interaction pour Indécis.xaml
    /// </summary>
    public partial class Indécis : Window
    {

        String Email;
        String listeprod;
        decimal prixtot;
        public Indécis(String email)
        {
            InitializeComponent();
            this.Email = email;
            this.listeprod = "";
            this.prixtot = 0;
            //on peut montrer les produits disponibles et ensuite bouton ajouter pour ajouter un element ou bouton  commander

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT * from produit ;";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string listee = "";
            while (reader.Read())// parcourt ligne par ligne
            {
                for (int i = 0; i < 3; i++)
                {
                    listee += reader.GetString(i) + "        ";  // récupération de la ième colonne
                }
                listee += '\n';
                listee += '\n';
            }
            connection.Close();
            liste.Text = listee;
        }
        private void clicksuivant(object sender, RoutedEventArgs e)
        {
            if (description.Text != "" && prixmax.Text != "")
            {
                Commander p = new Commander(this.Email, description.Text, prixmax.Text, "persoindecis");
                p.Show();
                this.Close();
            }
            else
            {
                reessayer.Text = "Vous n'avez pas renseigné de description ou prix";
            }

        }
    }
}
