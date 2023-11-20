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
    /// Logique d'interaction pour Produit.xaml
    /// </summary>
    public partial class Produit : Window
    {
        public Produit()
        {
            InitializeComponent();
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
                for (int i = 0; i < 4; i++)
                {
                    listee += reader.GetString(i) + "        ";  // récupération de la ième colonne
                }
                listee += '\n';
            }
            listeproduit.Text = listee;
            connection.Close();

            connection.Open();
            MySqlCommand command2 = connection.CreateCommand();
            command2.CommandText = " SELECT stock_produit from produit;";
            MySqlDataReader reader2;
            reader2 = command2.ExecuteReader();
            int listee2;
            while (reader2.Read())// parcourt ligne par ligne
            {
                listee2 = Convert.ToInt32(reader2.GetString(0));
                if (listee2 < 10)
                {
                    MessageBox.Show("L'un des produits est à un seuil critique de stock");
                }
            }
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
                command.CommandText = " SELECT * from produit where nom_produit=@val ;";
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
                    if (dispo.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE produit SET disponibilité_produit = @value WHERE nom_produit =@val2;";
                        command2.Parameters.AddWithValue("@value", dispo.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }
                    if (stock.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE produit SET stock_produit = @value WHERE nom_produit =@val2;";
                        command2.Parameters.AddWithValue("@value", stock.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }
                    if (prix.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE standards SET prix_produit = @value WHERE nom_produit =@val2;";
                        command2.Parameters.AddWithValue("@value", prix.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }

                    if (nom.Text != "")
                    {
                        connection.Open();
                        MySqlCommand command4 = connection.CreateCommand();
                        command4.CommandText = "SELECT * from produit where nom_produit=@val";
                        command4.Parameters.AddWithValue("@val", nom.Text);
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
                            MySqlCommand command10 = connection.CreateCommand();
                            command10.CommandText = "UPDATE produit SET nom_produit = @value WHERE nom_produit =@val2;";
                            command10.Parameters.AddWithValue("@value", nom.Text);
                            command10.Parameters.AddWithValue("@val2", id.Text);
                            MySqlDataReader reader10;
                            reader10 = command10.ExecuteReader();
                            connection.Close();
                        }
                        else
                        {
                            tryagain.Text = "Ce nom est déjà utilisé";
                        }
                    }
                    MessageBox.Show("Fiche produit mise à jour");
                    nom.Text = "";
                    prix.Text = "";
                    dispo.Text = "";
                    id.Text = "";
                    stock.Text = "";
                    tryagain.Text = "";
                    connection.Open();
                    MySqlCommand command8 = connection.CreateCommand();
                    command8.CommandText = " SELECT * from produit ;";
                    MySqlDataReader reader8;
                    reader8 = command8.ExecuteReader();
                    string listee8 = "";
                    while (reader8.Read())// parcourt ligne par ligne
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            listee8 += reader8.GetString(i) + "        ";  // récupération de la ième colonne
                        }
                        listee8 += '\n';
                    }
                    listeproduit.Text = listee8;
                }
                else
                {
                    tryagain.Text = "Nom non valide";
                }
            }
        }
        private void clickajout(object sender, RoutedEventArgs e)
        {
            if (nom.Text != "")
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO `fleurs`.`produit` (`nom_produit`, `prix_produit`, `disponibilité_produit`,`stock_produit`) VALUES (@nom,@prix,@dispo,@stock);";
                command.Parameters.AddWithValue("@nom", nom.Text);
                command.Parameters.AddWithValue("@prix", prix.Text);
                command.Parameters.AddWithValue("@dispo", dispo.Text);
                command.Parameters.AddWithValue("@stock", stock.Text);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string listee = "";
                while (reader.Read())// parcourt ligne par ligne
                {
                    listee += reader.GetString(0);
                }
                connection.Close();

                MessageBox.Show("Produit ajouté à la bdd");
                nom.Text = "";
                prix.Text = "";
                dispo.Text = "";
                id.Text = "";
                stock.Text = "";
                tryagain.Text = "";
                connection.Open();
                MySqlCommand command8 = connection.CreateCommand();
                command8.CommandText = " SELECT * from produit ;";
                MySqlDataReader reader8;
                reader8 = command8.ExecuteReader();
                string listee8 = "";
                while (reader8.Read())// parcourt ligne par ligne
                {
                    for (int i = 0; i < 4; i++)
                    {
                        listee8 += reader8.GetString(i) + "        ";  // récupération de la ième colonne
                    }
                    listee8 += '\n';
                }
                listeproduit.Text = listee8;

            }
            else
            {
                tryagain.Text = "Veuillez renseigner un nom";
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
                command12.CommandText = " SELECT * from produit where nom_produit=@val ;";
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
                    command.CommandText = "DELETE FROM produit WHERE nom_produit = @val;";
                    command.Parameters.AddWithValue("@val", id.Text);
                    MySqlDataReader reader;
                    reader = command.ExecuteReader();
                    string listee = "";
                    while (reader.Read())// parcourt ligne par ligne
                    {
                        listee += reader.GetString(0);
                    }
                    connection.Close();

                    MessageBox.Show("Produit  supprimé de la bdd");
                    nom.Text = "";
                    prix.Text = "";
                    dispo.Text = "";
                    id.Text = "";
                    stock.Text = "";
                    tryagain.Text = "";
                    connection.Open();
                    MySqlCommand command8 = connection.CreateCommand();
                    command8.CommandText = " SELECT * from produit ;";
                    MySqlDataReader reader8;
                    reader8 = command8.ExecuteReader();
                    string listee8 = "";
                    while (reader8.Read())// parcourt ligne par ligne
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            listee8 += reader8.GetString(i) + "        ";  // récupération de la ième colonne
                        }
                        listee8 += '\n';
                    }
                    listeproduit.Text = listee8; ;
                    connection.Close();
                }
                else
                {
                    tryagain.Text = "Veuillez renseigner un nom valide";
                }
            }
            else
            {
                tryagain.Text = "Veuillez renseigner un nom ";
            }

        }
    }
}
