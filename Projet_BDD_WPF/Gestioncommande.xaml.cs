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
    /// Logique d'interaction pour Gestioncommande.xaml
    /// </summary>
    public partial class Gestioncommande : Window
    {
        String Num;
        String Etat;
        public Gestioncommande(String num, String etat)
        {
            InitializeComponent();
            this.Num = num;
            this.Etat = etat;

            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT * from commande where @val=num_commande ;";
            command.Parameters.AddWithValue("@val", num);
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
            comchoisi.Text = listee;
            connection.Close();

        }
        private void clickvalider(object sender, RoutedEventArgs e)
        {
            //mettre à jour le contenu de la commande
            if (contenu.Text != "")
            {
                tryagainvalider.Text = "";
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE commande SET contenu_commande = @value WHERE num_commande =@val2;";
                command.Parameters.AddWithValue("@value", contenu.Text);
                command.Parameters.AddWithValue("@val2", this.Num);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                connection.Close();
                MessageBox.Show("Contenu mis à jour");
            }
            else
            {
                tryagainvalider.Text = "Veuillez remplir le contenu";
            }
        }

        private void clickajout(object sender, RoutedEventArgs e)
        {
            if (element.Text != "" && quantite.Text != "")
            {
                try
                {
                    int a = Convert.ToInt32(quantite.Text);
                    //chercher l'element dans la bdd
                    string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = " SELECT stock_produit from produit where @val=nom_produit ;";
                    command.Parameters.AddWithValue("@val", element.Text);
                    MySqlDataReader reader;
                    reader = command.ExecuteReader();
                    string stock = "";
                    while (reader.Read())// parcourt ligne par ligne
                    {
                        stock += reader.GetString(0);  // récupération de la 1ère colonne
                    }
                    connection.Close();
                    if (stock != "")
                    {
                        tryagainajout.Text = "";
                        // verifier que stock pas negatif sinon erreur
                        if (Convert.ToInt32(stock) >= Convert.ToInt32(quantite.Text))
                        {
                            //modifier le stock
                            int b = Convert.ToInt32(stock) - a;
                            connection.Open();
                            MySqlCommand command2 = connection.CreateCommand();
                            command2.CommandText = "UPDATE produit SET stock_produit = @value WHERE nom_produit =@val2;";
                            command2.Parameters.AddWithValue("@value", b);
                            command2.Parameters.AddWithValue("@val2", element.Text);
                            MySqlDataReader reader2;
                            reader2 = command2.ExecuteReader();
                            connection.Close();
                            MessageBox.Show("Stock mis à jour");
                            element.Text = "";
                            quantite.Text = "";
                        }
                        else
                        {
                            tryagainajout.Text = "Veuillez remplir le nom de l'élément et la quantité de manière valide";
                        }
                    }
                    else
                    {
                        tryagainajout.Text = "Veuillez remplir le nom de l'élément et la quantité de manière valide";
                    }
                }
                catch (FormatException)
                {
                    tryagainajout.Text = "Veuillez remplir le nom de l'élément et la quantité de manière valide";
                }
            }
            else
            {
                tryagainajout.Text = "Veuillez remplir le nom de l'élément et la quantité de manière valide";
            }
        }
        private void clickajouter(object sender, RoutedEventArgs e)
        {
            if (stand.Text != "")
            {
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = " SELECT stock_bouquet from standards where @val=nom_bouquet ;";
                command.Parameters.AddWithValue("@val", stand.Text);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string stock = "";
                while (reader.Read())// parcourt ligne par ligne
                {
                    stock += reader.GetString(0);  // récupération de la 1ère colonne
                }
                connection.Close();
                if (stock != "")
                {
                    tryagainajout2.Text = "";
                    // verifier que stock pas negatif sinon erreur
                    if (Convert.ToInt32(stock) >= 1)
                    {
                        //modifier le stock
                        int b = Convert.ToInt32(stock) - 1;
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = "UPDATE standards SET stock_bouquet = @value WHERE nom_bouquet =@val2;";
                        command2.Parameters.AddWithValue("@value", b);
                        command2.Parameters.AddWithValue("@val2", stand.Text);
                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        connection.Close();
                        MessageBox.Show("Stock mis à jour");
                        stand.Text = "";
                    }
                    else
                    {
                        tryagainajout2.Text = "Pas de stock suffisant pour effectuer cette commande";
                    }
                }
                else
                {
                    tryagainajout2.Text = "Veuillez remplir le nom de l'élément de manière valide";
                }
            }
            else
            {
                tryagainajout2.Text = "Veuillez remplir le nom de l'élément de manière valide";
            }
        }
        private void clickconfirmer(object sender, RoutedEventArgs e)
        {
            //changer d'état de la commande
            //au cas ou il y a eu une erreur dans le chgt on laisse la possibilté de remettre VINV et CPAC
            if (etat.Text == "CC" || etat.Text == "CAL" || etat.Text == "CL" || etat.Text == "CPAC")
            {
                tryagainconfirmer.Text = "";
                string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE commande SET etat_commande = @value WHERE num_commande =@val2;";
                command.Parameters.AddWithValue("@value", etat.Text);
                command.Parameters.AddWithValue("@val2", this.Num);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                connection.Close();
                MessageBox.Show("Etat mis à jour");
                etat.Text = "";
            }
            else
            {
                //reessayer
                tryagainconfirmer.Text = "Veuillez renseigner un nom d'état valide (CC,CAL,CL,CPAV,VINV)";
            }
        }
        private void clickconfir(object sender, RoutedEventArgs e)
        {
            //mise a jour prix de la commande
            //APPLIQUER FIDELITE verifier statut (pas changer)
            if (prix.Text !="")
            {
                try
                {
                    double prii = Convert.ToDouble(prix.Text);
                    //trouver statut fidelite client
                    string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();
                    MySqlCommand command2 = connection.CreateCommand();
                    command2.CommandText = "select statut_fidelite from client WHERE courriel_client=(select courriel_client from commande where num_commande=@value);";
                    command2.Parameters.AddWithValue("@value", this.Num);
                    MySqlDataReader reader2;
                    String fidel = "";
                    reader2 = command2.ExecuteReader();
                    while (reader2.Read())// parcourt ligne par ligne
                    {
                        fidel += reader2.GetString(0);  // récupération de la 1ère colonne
                    }
                    connection.Close();
                    double pr;
                    if (fidel == "or")
                    {
                        pr = prii * 85 / 100;
                    }
                    else
                    {
                        if (fidel == "bronze")
                        {
                            pr = prii * 95 / 100;
                        }
                        else
                        {
                            pr = prii;
                        }
                    }

                    tryagainconfir.Text = "";
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "UPDATE commande SET prix_commande = @value WHERE num_commande =@val2;";
                    command.Parameters.AddWithValue("@value", pr+"€");
                    command.Parameters.AddWithValue("@val2", this.Num);
                    MySqlDataReader reader;
                    reader = command.ExecuteReader();
                    connection.Close();
                    MessageBox.Show("Prix mis à jour");
                    prix.Text = "";
                }
                catch (FormatException)
                {
                    tryagainconfir.Text = "Veuillez renseigner un prix valide";
                }
            }
            else
            {
                //reessayer
                tryagainconfir.Text = "Veuillez renseigner un prix valide";
            }
        }
        private void clickterminer(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
