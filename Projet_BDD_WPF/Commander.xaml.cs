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
    /// Logique d'interaction pour Commander.xaml
    /// </summary>
    public partial class Commander : Window
    {
        String Email;
        String type;
        String fidelite;
        String Produit;
        public Commander(String email,String produit,String prixtot, String typeproduit)
        {
            
            this.Email = email;
            this.type=typeproduit;
            this.Produit = produit;
            InitializeComponent();

            //chercher date première commande
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command2 = connection.CreateCommand();
            command2.CommandText = " select date_commande from commande where courriel_client = @val;";
            command2.Parameters.AddWithValue("@val", email);
            MySqlDataReader reader2;
            reader2 = command2.ExecuteReader();
            string date1 = "";
            bool test = true;
            int count = 0;
            while (reader2.Read())// parcourt ligne par ligne
            {
                if (test == true)
                {
                    date1 = reader2.GetString(0);  // récupération de la 1ère colonne
                    test = false;
                }
                count++;
            }
            connection.Close();
            //determiner statut fidelite
            if (date1 != "")
            {
                var t = (DateTime.Today.Date) - Convert.ToDateTime(date1);
                double nbbouqprmois = count / (t.TotalDays / 30);
                if (nbbouqprmois >= 5)
                {
                    //fidelite or
                    connection.Open();
                    MySqlCommand command3 = connection.CreateCommand();
                    command3.CommandText = "UPDATE client SET statut_fidelite = @value WHERE courriel_client =@val2;";
                    command3.Parameters.AddWithValue("@value", "or");
                    command3.Parameters.AddWithValue("@val2", this.Email);
                    MySqlDataReader reader3;
                    reader3 = command3.ExecuteReader();
                    connection.Close();
                    this.fidelite = "or";
                }
                else
                {
                    if (nbbouqprmois >= 1)
                    {
                        //fidelite bronze
                        connection.Open();
                        MySqlCommand command4 = connection.CreateCommand();
                        command4.CommandText = "UPDATE client SET statut_fidelite = @value WHERE courriel_client =@val2;";
                        command4.Parameters.AddWithValue("@value", "bronze");
                        command4.Parameters.AddWithValue("@val2", this.Email);
                        MySqlDataReader reader4;
                        reader4 = command4.ExecuteReader();
                        connection.Close();
                        this.fidelite = "bronze";
                    }
                    else
                    {
                        //fidelite none
                        connection.Open();
                        MySqlCommand command5 = connection.CreateCommand();
                        command5.CommandText = "UPDATE client SET statut_fidelite = @value WHERE courriel_client =@val2;";
                        command5.Parameters.AddWithValue("@value", "none");
                        command5.Parameters.AddWithValue("@val2", this.Email);
                        MySqlDataReader reader5;
                        reader5 = command5.ExecuteReader();
                        connection.Close();
                        this.fidelite = "none";
                    }
                }
            }
            else
            {
                //fidelite none
                connection.Open();
                MySqlCommand command6 = connection.CreateCommand();
                command6.CommandText = "UPDATE client SET statut_fidelite = @value WHERE courriel_client =@val2;";
                command6.Parameters.AddWithValue("@value", "none");
                command6.Parameters.AddWithValue("@val2", this.Email);
                MySqlDataReader reader6;
                reader6 = command6.ExecuteReader();
                connection.Close();
                this.fidelite = "none";
            }
            


            datecom.Text =Convert.ToString(DateTime.Today.ToShortDateString()); //pour la suite reecrire direct datetime.today plutot qu'utiliser ce string et convertir en date
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select num_commande from commande where num_commande>=all(select num_commande from commande)";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string num = "";
            while (reader.Read())
            {
                num = reader.GetString(0);
            }
            int num_com;
            try{
                Convert.ToInt32(num);
                num_com = Convert.ToInt32(num) + 1;
            }
            catch
            {
                num_com = 1;
            }
            numcom.Text = Convert.ToString(num_com);
            connection.Close();

            
            //init contenu et prix
            if (typeproduit == "standard")
            {
                prix.Text = prixtot;

                connection.Open();
                MySqlCommand command3 = connection.CreateCommand();
                command3.CommandText = "select composition_fleurs from standards where nom_bouquet=@val";
                command3.Parameters.AddWithValue("@val", produit);
                MySqlDataReader reader3;
                reader3 = command3.ExecuteReader();
                string cont = "";
                while (reader3.Read())
                {
                    cont = reader3.GetString(0);
                }
                contenu.Text =produit+" : "+ cont;
                connection.Close();
            }
            else
            {
                if (typeproduit == "perso")
                {
                    contenu.Text = String.Join(", ",produit.Split('\n'));
                    prix.Text =prixtot+"€";
                }
                else
                {
                    if (typeproduit == "persoindecis")
                    {
                        prix.Text = prixtot;
                        contenu.Text = produit;
                    }
                }
            }

        }

        private void clickcommander(object sender, RoutedEventArgs e)
        { 
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO commande(num_commande,courriel_client, date_commande, adresse_livraison, message,date_livraison,type_commande,contenu_commande,etat_commande, prix_commande,nom_magasin)  VALUES(@num,@mail,@datecom,@add, @message,@dateliv, @type,@contenu, @etat, @prix,@magasin)";
            command.Parameters.AddWithValue("@num", numcom.Text);
            command.Parameters.AddWithValue("@mail", Email);
            command.Parameters.AddWithValue("@datecom", DateTime.Today);
            command.Parameters.AddWithValue("@add", addliv.Text);
            command.Parameters.AddWithValue("@message", message.Text);
            command.Parameters.AddWithValue("@dateliv", dateliv.SelectedDate);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@contenu", contenu.Text);
            command.Parameters.AddWithValue("@magasin", magasin.Text);



            if (type == "standard")
            {
                var t = (dateliv.SelectedDate)-(DateTime.Today.Date);
                if (t.Value.TotalDays < 3)
                {
                    MessageBox.Show("Attention, au vu du bref délai de livraison, vous assumez le risque d'une potentielle pénurie");
                    command.Parameters.AddWithValue("@etat", "VINV");
                }
                else
                {
                    command.Parameters.AddWithValue("@etat", "CC");
                }
            }
            else
            {
                if (type == "perso")
                {
                    command.Parameters.AddWithValue("@etat","CPAV");
                }
                else
                {
                    if (type == "persoindecis")
                    {
                        command.Parameters.AddWithValue("@etat", "CPAV");
                    }
                }
            }
            command.Parameters.AddWithValue("@prix", prix.Text);
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            connection.Close();

            if (type == "standard")
            {
                var tbis = (dateliv.SelectedDate) - (DateTime.Today.Date);
                
                if (tbis.Value.TotalDays >= 3)
                {
                    
                    connection.Open();
                    MySqlCommand command3 = connection.CreateCommand();
                    command3.CommandText = "select stock_bouquet from standards where nom_bouquet=@val";
                    command3.Parameters.AddWithValue("@val", this.Produit);
                    MySqlDataReader reader3;
                    reader3 = command3.ExecuteReader();
                    string stock = "";
                    while (reader3.Read())
                    {
                        stock = reader3.GetString(0);
                    }
                    connection.Close();

                    int b = Convert.ToInt32(stock) - 1;
                    connection.Open();
                    MySqlCommand command2 = connection.CreateCommand();
                    command2.CommandText = "UPDATE standards SET stock_bouquet = @value WHERE nom_bouquet =@val2;";
                    command2.Parameters.AddWithValue("@value", b);
                    command2.Parameters.AddWithValue("@val2", this.Produit);
                    MySqlDataReader reader2;
                    reader2 = command2.ExecuteReader();
                    connection.Close();
                }

            }
            MessageBox.Show("Nouvelle commande enregistré");
            this.Close();
            
            
        }
    }
}
