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
    /// Logique d'interaction pour Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        public Stats()
        {
            InitializeComponent();
            //requete synchronisée
            //nom des clients ayant effectué plus de commande que la moyenne
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select distinct courriel_client from commande as C2 where (select count(*) from commande as C3 group by courriel_client having C3.courriel_client=C2.courriel_client)>(select avg(count) from (select count(*) as count, courriel_client from commande as C1 group by courriel_client) as T1);";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string listee = "";
            while (reader.Read())// parcourt ligne par ligne
            {
                listee += reader.GetString(0);
                listee += '\n';
            }
            listesupmoy.Text = listee;
            connection.Close();

            //requete autojointure
            //nom des clients commandant depuis le meme magasin
            connection.Open();
            MySqlCommand command2 = connection.CreateCommand();
            command2.CommandText = "select distinct Com1.courriel_client, Com2.courriel_client from commande Com1, commande Com2 where Com1.nom_magasin=Com2.nom_magasin and Com1.courriel_client<Com2.courriel_client;";
            MySqlDataReader reader2;
            reader2 = command2.ExecuteReader();
            string listee2 = "";
            while (reader2.Read())// parcourt ligne par ligne
            {
                listee2 += reader2.GetString(0) + " et ";
                listee2 += reader2.GetString(1);
                listee2 += '\n';
            }
            listemememag.Text = listee2;
            connection.Close();

            //requete Union
            //client ayant à la fois fait des commandes personnalisées et personnalisées indécises
            //(select distinct courriel_client from commande where type_commande="perso") union (select distinct courriel_client from commande where type_commande="persoindecis");
            connection.Open();
            MySqlCommand command3 = connection.CreateCommand();
            command3.CommandText = "(select distinct courriel_client from commande where type_commande=@val1) union (select distinct courriel_client from commande where type_commande=@val2);";
            command3.Parameters.AddWithValue("@val1", "perso");
            command3.Parameters.AddWithValue("@val2", "persoindecis");
            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();
            string listee3 = "";
            while (reader3.Read())// parcourt ligne par ligne
            {
                listee3 += reader3.GetString(0);
                listee3 += '\n';
            }
            listeperso.Text = listee3;
            connection.Close();


            //prix moyen du bouquet acheté
            connection.Open();
            MySqlCommand command4 = connection.CreateCommand();
            command4.CommandText = "select avg(prix_commande) from commande where type_commande=@val;";
            command4.Parameters.AddWithValue("@val", "standard");
            MySqlDataReader reader4;
            reader4 = command4.ExecuteReader();
            string listee4 = "";
            while (reader4.Read())// parcourt ligne par ligne
            {
                listee4 += reader4.GetString(0);
            }
            avgbouq.Text = Convert.ToString(Math.Round(Convert.ToDouble(listee4), 2));
            connection.Close();


            //prix moyen d'une commande
            //select avg(prix_commande) from commande;
            connection.Open();
            MySqlCommand command5 = connection.CreateCommand();
            command5.CommandText = "select avg(prix_commande) from commande";
            MySqlDataReader reader5;
            reader5 = command5.ExecuteReader();
            string listee5 = "";
            while (reader5.Read())// parcourt ligne par ligne
            {
                listee5 += reader5.GetString(0);
            }
            avgcom.Text = Convert.ToString(Math.Round(Convert.ToDouble(listee5), 2));
            connection.Close();

            //meilleur client du mois
            connection.Open();
            MySqlCommand command6 = connection.CreateCommand();
            command6.CommandText = "select distinct courriel_client from commande as C1 where (select count(*) from (select * from commande C2 where date_commande>=@val1) as T2 group by courriel_client having T2.courriel_client=C1.courriel_client)>=all(select count(*) from (select * from commande C2 where date_commande>=@val2) as T2 group by courriel_client);";
            command6.Parameters.AddWithValue("@val1", DateTime.Today.AddMonths(-1));
            command6.Parameters.AddWithValue("@val2", DateTime.Today.AddMonths(-1));
            MySqlDataReader reader6;
            reader6 = command6.ExecuteReader();
            string listee6 = "";
            while (reader6.Read())// parcourt ligne par ligne
            {
                listee6 += reader6.GetString(0);
            }
            bestcmonth.Text = listee6;
            connection.Close();

            //meilleur client de l'année
            connection.Open();
            MySqlCommand command7 = connection.CreateCommand();
            command7.CommandText = "select distinct courriel_client from commande as C1 where (select count(*) from (select * from commande C2 where date_commande>=@val1) as T2 group by courriel_client having T2.courriel_client=C1.courriel_client)>=all(select count(*) from (select * from commande C2 where date_commande>=@val2) as T2 group by courriel_client);";
            command7.Parameters.AddWithValue("@val1", DateTime.Today.AddYears(-1));
            command7.Parameters.AddWithValue("@val2", DateTime.Today.AddYears(-1));
            MySqlDataReader reader7;
            reader7 = command7.ExecuteReader();
            string listee7 = "";
            while (reader7.Read())// parcourt ligne par ligne
            {
                listee7 += reader7.GetString(0);
            }
            bestcyear.Text = listee7;
            connection.Close();

            //bouquet ayant eu le plus de succès
            connection.Open();
            MySqlCommand command8 = connection.CreateCommand();
            command8.CommandText = "select distinct contenu_commande from commande as  C1 where (select count(*) from (select * from commande C2 where type_commande=@val1) as T1 group by contenu_commande having T1.contenu_commande=C1.contenu_commande)>=all(select count(*) from (select * from commande C2 where type_commande=@val2) as T1 group by contenu_commande);";
            command8.Parameters.AddWithValue("@val1", "standard");
            command8.Parameters.AddWithValue("@val2", "standard");
            MySqlDataReader reader8;
            reader8 = command8.ExecuteReader();
            string listee8 = "";
            while (reader8.Read())// parcourt ligne par ligne
            {
                listee8 += reader8.GetString(0);
            }
            bouqstand.Text = listee8.Split(":")[0];
            connection.Close();

            //magasin ayant le plus de commande
            connection.Open();
            MySqlCommand command9 = connection.CreateCommand();
            command9.CommandText = "select distinct nom_magasin from commande C2 where (SELECT count(*) from commande C1 group by nom_magasin having C1.nom_magasin=C2.nom_magasin)>=all(SELECT count(*) from commande group by nom_magasin);";
            MySqlDataReader reader9;
            reader9 = command9.ExecuteReader();
            string listee9 = "";
            while (reader9.Read())// parcourt ligne par ligne
            {
                listee9 += reader9.GetString(0);
            }
            magasin.Text = listee9;
            connection.Close();


            //on choisit le magasin et en fonction de ça on montre liste client du magasin

            //export xml
            //client ayant commandé plusieurs fois dans le dernier mois
            //select * from client where courriel_client in(select courriel_client from (select * from commande where date_commande>=@val) as T group by courriel_client having count(*)>1);
            //@val= DateTime.Today.AddMonths(-1);

        }
    }
}
