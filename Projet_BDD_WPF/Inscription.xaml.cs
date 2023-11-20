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
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    public partial class Inscription : Window
    {
        public Inscription()
        {
            InitializeComponent();
        }

        private void clicksave(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command2 = connection.CreateCommand();
            command2.CommandText = "select nom_client from client where courriel_client = @emailtext" ;
            command2.Parameters.AddWithValue("@emailtext", email.Text);
            MySqlDataReader reader;
            reader = command2.ExecuteReader();
            string mail="";
            while (reader.Read())
            {
                mail = reader.GetString(0);
                Console.WriteLine(mail);
            }
            connection.Close();
            if (mail== "")
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO client(courriel_client, nom_client, prenom_client, num_tel_client,mot_de_passe_client,adresse_facturation_client,carte_credit,statut_fidelite)  VALUES(@mail,@nom,@prenom, @tel,@mdp,@add, @carte, @fidel)";
                command.Parameters.AddWithValue("@mail", email.Text);
                command.Parameters.AddWithValue("@nom", nom.Text);
                command.Parameters.AddWithValue("@prenom", prenom.Text);
                command.Parameters.AddWithValue("@tel", tel.Text);
                command.Parameters.AddWithValue("@mdp", mdp.Text);
                command.Parameters.AddWithValue("@add", adresse.Text);
                command.Parameters.AddWithValue("@carte", carte.Text);
                command.Parameters.AddWithValue("@fidel", "none");
                MySqlDataReader reader2;
                reader2 = command.ExecuteReader();
                MessageBox.Show("Nouveau client enregistré");
                this.Close();
                ChoixCommande p = new ChoixCommande(email.Text);
                p.Show();
                //ouvrir nouvelle fenetre avec client connecté
            }
            else
            {
                MessageBox.Show("Email déjà utilisé, choisissez une autre adresse email.");
            }
            connection.Close();
        }

    }
}
