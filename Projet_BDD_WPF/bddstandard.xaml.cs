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
    /// Logique d'interaction pour bddstandard.xaml
    /// </summary>
    public partial class bddstandard : Window
    {
        public bddstandard()
        {
            InitializeComponent();
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT * from standards ;";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string listee = "";
            while (reader.Read())// parcourt ligne par ligne
            {
                for (int i = 0; i < 5; i++)
                {
                    listee += reader.GetString(i) + "        ";  // récupération de la ième colonne
                }
                listee += '\n';
            }
            listestandard.Text = listee;
            connection.Close();

            connection.Open();
            MySqlCommand command2 = connection.CreateCommand();
            command2.CommandText = " SELECT stock_bouquet from standards;";
            MySqlDataReader reader2;
            reader2 = command2.ExecuteReader();
            int listee2;
            while (reader2.Read())// parcourt ligne par ligne
            {
                listee2 =Convert.ToInt32(reader2.GetString(0));
                if (listee2 < 10)
                {
                    MessageBox.Show("L'un des bouquets est à un seuil critique de stock");
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
                command.CommandText = " SELECT * from standards where nom_bouquet=@val ;";
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
                    if (compo.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE standards SET composition_fleurs = @value WHERE nom_bouquet =@val2;";
                        command2.Parameters.AddWithValue("@value", compo.Text);
                        command2.Parameters.AddWithValue("@val2", id.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                    }
                    if (cate.Text != "")
                    {
                        tryagain.Text = "";
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE standards SET categorie_bouquet = @value WHERE nom_bouquet =@val2;";
                        command2.Parameters.AddWithValue("@value", cate.Text);
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
                        command2.CommandText = "UPDATE standards SET stock_bouquet = @value WHERE nom_bouquet =@val2;";
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
                        command2.CommandText = "UPDATE standards SET prix_bouquet_standards = @value WHERE nom_bouquet =@val2;";
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
                        command4.CommandText = "SELECT * from standards where nom_bouquet=@val";
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
                            command10.CommandText = "UPDATE standards SET nom_bouquet = @value WHERE nom_bouquet =@val2;";
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
                    cate.Text = "";
                    prix.Text = "";
                    compo.Text = "";
                    id.Text = "";
                    stock.Text = "";
                    tryagain.Text = "";
                    connection.Open();
                    MySqlCommand command8 = connection.CreateCommand();
                    command8.CommandText = " SELECT * from standards ;";
                    MySqlDataReader reader8;
                    reader8 = command8.ExecuteReader();
                    string listee8 = "";
                    while (reader8.Read())// parcourt ligne par ligne
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            listee8 += reader8.GetString(i) + "        ";  // récupération de la ième colonne
                        }
                        listee8 += '\n';
                    }
                    listestandard.Text = listee8;
                    connection.Close();
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
                command.CommandText = "INSERT INTO `fleurs`.`standards` (`nom_bouquet`, `composition_fleurs`, `prix_bouquet_standards`, `categorie_bouquet`,`stock_bouquet`) VALUES (@nom,@compo,@prix,@cate,@stock);";
                command.Parameters.AddWithValue("@nom", nom.Text);
                command.Parameters.AddWithValue("@compo", compo.Text);
                command.Parameters.AddWithValue("@prix", prix.Text);
                command.Parameters.AddWithValue("@cate", cate.Text);
                command.Parameters.AddWithValue("@stock", stock.Text);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string listee = "";
                while (reader.Read())// parcourt ligne par ligne
                {
                    listee += reader.GetString(0);
                }
                connection.Close();

                MessageBox.Show("Bouquet ajouter à la bdd");
                nom.Text = "";
                cate.Text = "";
                prix.Text = "";
                compo.Text = "";
                id.Text = "";
                stock.Text = "";
                tryagain.Text = "";
                tryagain.Text = "";
                connection.Open();
                MySqlCommand command8 = connection.CreateCommand();
                command8.CommandText = " SELECT * from standards ;";
                MySqlDataReader reader8;
                reader8 = command8.ExecuteReader();
                string listee8 = "";
                while (reader8.Read())// parcourt ligne par ligne
                {
                    for (int i = 0; i < 5; i++)
                    {
                        listee8 += reader8.GetString(i) + "        ";  // récupération de la ième colonne
                    }
                    listee8 += '\n';
                }
                listestandard.Text = listee8;
                connection.Close();

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
                command12.CommandText = " SELECT * from standards where nom_bouquet=@val ;";
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
                    command.CommandText = "DELETE FROM standards WHERE nom_bouquet = @val;";
                    command.Parameters.AddWithValue("@val", id.Text);
                    MySqlDataReader reader;
                    reader = command.ExecuteReader();
                    string listee = "";
                    while (reader.Read())// parcourt ligne par ligne
                    {
                        listee += reader.GetString(0);
                    }
                    connection.Close();

                    MessageBox.Show("Bouquet  supprimé de la bdd");
                    nom.Text = "";
                    cate.Text = "";
                    prix.Text = "";
                    compo.Text = "";
                    id.Text = "";
                    stock.Text = "";
                    tryagain.Text = "";
                    tryagain.Text = "";
                    connection.Open();
                    MySqlCommand command8 = connection.CreateCommand();
                    command8.CommandText = " SELECT * from standards ;";
                    MySqlDataReader reader8;
                    reader8 = command8.ExecuteReader();
                    string listee8 = "";
                    while (reader8.Read())// parcourt ligne par ligne
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            listee8 += reader8.GetString(i) + "        ";  // récupération de la ième colonne
                        }
                        listee8 += '\n';
                    }
                    listestandard.Text = listee8;
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
