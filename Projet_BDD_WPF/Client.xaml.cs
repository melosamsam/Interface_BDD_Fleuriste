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
    /// Logique d'interaction pour Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        public Client()
        {
            InitializeComponent();
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT * from client ;";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string listee = "";
            while (reader.Read())// parcourt ligne par ligne
            {
                for (int i = 0; i < 8; i++)
                {
                    listee += reader.GetString(i) + "        ";  // récupération de la ième colonne
                }
                listee += '\n';
            }
            listeclient.Text = listee;
            connection.Close();
        }
        private void clickmodif(object sender, RoutedEventArgs e)
        {
            if (id.Text != "")
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " SELECT * from client where courriel_client=@val ;";
                command.Parameters.AddWithValue("@val", id.Text);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string listee = "";
                while (reader.Read())// parcourt ligne par ligne
                {
                    listee += reader.GetString(0);
                }
                connection.Close();
                if (listee != "")
                {
                    if (nom.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE client SET nom_client = @value WHERE courriel_client =@val2;";
                        command2.Parameters.AddWithValue("@value", nom.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }
                    if (prenom.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE client SET prenom_client = @value WHERE courriel_client =@val2;";
                        command2.Parameters.AddWithValue("@value", prenom.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }
                    if (tel.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE client SET num_tel_client = @value WHERE courriel_client =@val2;";
                        command2.Parameters.AddWithValue("@value", tel.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }
                    if (mdp.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE client SET mot_de_passe_client = @value WHERE courriel_client =@val2;";
                        command2.Parameters.AddWithValue("@value", mdp.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }
                    if (add.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE client SET adresse_facturation_client = @value WHERE courriel_client =@val2;";
                        command2.Parameters.AddWithValue("@value", add.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }
                    if (carte.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE client SET carte_credit = @value WHERE courriel_client =@val2;";
                        command2.Parameters.AddWithValue("@value", carte.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }

                    if (mail.Text != "")
                    {
                        connection.Open();
                        MySqlCommand command4 = connection.CreateCommand();
                        command4.CommandText = " SELECT * from client where courriel_client=@val ;";
                        command4.Parameters.AddWithValue("@val", mail.Text);
                        MySqlDataReader reader4;
                        reader4 = command4.ExecuteReader();
                        string listee4 = "";
                        while (reader4.Read())// parcourt ligne par ligne
                        {
                            listee4 += reader4.GetString(0);
                        }
                        connection.Close();

                        if (listee4 == "")
                        {
                            tryagain.Text = "";
                            connection.Open();
                            MySqlCommand command8 = connection.CreateCommand();
                            command8.CommandText = "UPDATE client SET courriel_client = @value WHERE courriel_client =@val2;";
                            command8.Parameters.AddWithValue("@value", mail.Text);
                            command8.Parameters.AddWithValue("@val2", id.Text);
                            MySqlDataReader reader8;
                            reader8 = command8.ExecuteReader();
                            connection.Close();
                        }
                        else
                        {
                            tryagain.Text = "Cet adresse email est déjà utilisée";
                        }
                    }


                    MessageBox.Show("Fiche client mise à jour");
                    nom.Text = "";
                    id.Text = "";
                    prenom.Text = "";
                    mail.Text = "";
                    carte.Text = "";
                    add.Text = "";
                    mdp.Text = "";
                    tel.Text = "";
                    tryagain.Text = "";
                    connection.Open();
                    MySqlCommand command3 = connection.CreateCommand();
                    command3.CommandText = " SELECT * from client ;";
                    MySqlDataReader reader3;
                    reader3 = command3.ExecuteReader();
                    string listee3 = "";
                    while (reader3.Read())// parcourt ligne par ligne
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            listee3 += reader3.GetString(i) + "        ";  // récupération de la ième colonne
                        }
                        listee3 += '\n';
                    }
                    listeclient.Text = listee3;
                    connection.Close();
                }
                else
                {
                    tryagain.Text = "Email non valide";
                }

            }
        }
        private void clicksuppr(object sender, RoutedEventArgs e)
        {
            if (id.Text != "")
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command12 = connection.CreateCommand();
                command12.CommandText = " SELECT * from client where courriel_client=@val ;";
                command12.Parameters.AddWithValue("@val", id.Text);
                MySqlDataReader reader12;
                reader12 = command12.ExecuteReader();
                string listee12 = "";
                while (reader12.Read())// parcourt ligne par ligne
                {
                    listee12 += reader12.GetString(0);
                }
                connection.Close();
                if (listee12 != "")
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM client WHERE courriel_client = @val;";
                    command.Parameters.AddWithValue("@val", id.Text);
                    MySqlDataReader reader;
                    reader = command.ExecuteReader();
                    string listee = "";
                    while (reader.Read())// parcourt ligne par ligne
                    {
                        listee += reader.GetString(0);
                    }
                    connection.Close();

                    MessageBox.Show("Client  supprimé de la bdd");
                    nom.Text = "";
                    id.Text = "";
                    prenom.Text = "";
                    mail.Text = "";
                    carte.Text = "";
                    add.Text = "";
                    mdp.Text = "";
                    tel.Text = "";
                    tryagain.Text = "";
                    connection.Open();
                    MySqlCommand command3 = connection.CreateCommand();
                    command3.CommandText = " SELECT * from client ;";
                    MySqlDataReader reader3;
                    reader3 = command3.ExecuteReader();
                    string listee3 = "";
                    while (reader3.Read())// parcourt ligne par ligne
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            listee3 += reader3.GetString(i) + "        ";  // récupération de la ième colonne
                        }
                        listee3 += '\n';
                    }
                    listeclient.Text = listee3;
                    connection.Close();
                }
                else
                {
                    tryagain.Text = "Veuillez renseigner un email valide";
                }
            }
            else
            {
                tryagain.Text = "Veuillez renseigner un email ";
            }
        }
        
    }
}
