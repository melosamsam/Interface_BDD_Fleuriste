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
    /// Logique d'interaction pour Personnalisée.xaml
    /// </summary>
    public partial class Personnalisée : Window
    {
        String Email;
        String listeprod;
        decimal prixtot;
        public Personnalisée(String email)
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
        private void clickchoix(object sender, RoutedEventArgs e)
        {
            if (choix.Text != "")
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " SELECT prix_produit from produit where @value=nom_produit;";
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
                    this.listeprod += choix.Text;
                    this.listeprod += '\n';
                    prodchoisi.Text += choix.Text;
                    prodchoisi.Text += '\n';
                    choix.Text = "";
                    //retourner le prix du dit produit
                    this.prixtot+= Convert.ToDecimal(pri);
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
        private void clicksuivant(object sender, RoutedEventArgs e)
        {
            if (this.listeprod != "") 
            {
                Commander p = new Commander(this.Email, this.listeprod,Convert.ToString(this.prixtot), "perso");
                p.Show();
                this.Close();
            }
            else
            {
                reessayer.Text = "Vous n'avez pas selectionné de produit";
            }
            
        }

        private void clickindecis(object sender, RoutedEventArgs e)
        {
            Indécis p = new Indécis(this.Email);
            p.Show();
            this.Close();
        }
    }
}
