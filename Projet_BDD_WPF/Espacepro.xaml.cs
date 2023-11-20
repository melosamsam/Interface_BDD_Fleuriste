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
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace Projet_BDD_WPF
{
    /// <summary>
    /// Logique d'interaction pour Espacepro.xaml
    /// </summary>
    public partial class Espacepro : Window
    {
        public Espacepro()
        {
            InitializeComponent();

        }
        private void clickvalider(object sender, RoutedEventArgs e)
        {
            if (bdd.IsChecked == true)
            {
                bdd p = new bdd();
                p.Show();
            }
            else
            {
                if (commande.IsChecked == true)
                {
                    Modifcommande p = new Modifcommande();
                    p.Show();
                }
                else
                {
                    if (stat.IsChecked == true)
                    {
                        Stats p = new Stats();
                        p.Show();
                    }
                    else
                    {
                        if (xml.IsChecked == true)
                        {
                            //select* from client where courriel_client in(select courriel_client from(select* from commande where date_commande>= @val) as T group by courriel_client having count(*) > 1);
                            //@val= DateTime.Today.AddMonths(-1);
                            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleurs;UID=root;PASSWORD=root;";
                            MySqlConnection connection = new MySqlConnection(connectionString);
                            connection.Open();
                            MySqlCommand command8 = connection.CreateCommand();
                            command8.CommandText = "select * from client where courriel_client in(select courriel_client from(select* from commande where date_commande>= @val) as T group by courriel_client having count(*) > 1);";
                            command8.Parameters.AddWithValue("@val", DateTime.Today.AddMonths(-1));
                            MySqlDataReader reader8;
                            reader8 = command8.ExecuteReader();
                            string listee8 = "";
                            while (reader8.Read())// parcourt ligne par ligne
                            {
                                for(int i = 0; i < 8; i++)
                                {
                                    listee8 += reader8.GetString(i);
                                    listee8 += ",";
                                }
                                listee8 += '\n';
                            }
                            connection.Close();

                            String[] listesep = listee8.Split('\n');


                            XmlDocument docXml = new XmlDocument();

                            // Création de l'élément racine... qu'on ajoute au document
                            XmlElement racine = docXml.CreateElement("BDD_Clients");
                            docXml.AppendChild(racine);

                            // création de l'en-tête XML (no <=> pas de DTD associée)
                            XmlDeclaration xmldecl = docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
                            docXml.InsertBefore(xmldecl, racine);

                            for (int i =0; i < listesep.Length-1; i++)
                            {
                                XmlElement client = docXml.CreateElement("client");
                                racine.AppendChild(client);

                                // Création d'un élément... qu'on ajoute à un autre élément (en tant que sous-élément)
                                XmlElement courriel = docXml.CreateElement("courriel_client");
                                courriel.InnerText = listesep[i].Split(',')[0];
                                client.AppendChild(courriel);

                                XmlElement nom = docXml.CreateElement("nom");
                                nom.InnerText = listesep[i].Split(',')[1];
                                client.AppendChild(nom);

                                XmlElement prenom = docXml.CreateElement("prénom");
                                prenom.InnerText = listesep[i].Split(',')[2];
                                client.AppendChild(prenom);

                                XmlElement numtel = docXml.CreateElement("numéro_de_téléphone");
                                numtel.InnerText = listesep[i].Split(',')[3];
                                client.AppendChild(numtel);

                                XmlElement mdp = docXml.CreateElement("mot_de_passe");
                                mdp.InnerText = listesep[i].Split(',')[4];
                                client.AppendChild(mdp);

                                XmlElement add = docXml.CreateElement("adresse");
                                add.InnerText = listesep[i].Split(',')[5];
                                client.AppendChild(add);

                                XmlElement carte = docXml.CreateElement("carte_de_crédit");
                                carte.InnerText = listesep[i].Split(',')[6];
                                client.AppendChild(carte);

                                XmlElement fidel = docXml.CreateElement("statut_fidélité");
                                fidel.InnerText = listesep[i].Split(',')[7];
                                client.AppendChild(fidel);
                            }

                            


                            // enregistrement du document XML   ==> à retrouver dans le dossier bin\Debug de Visual Studio
                            docXml.Save("bd1.xml");
                            MessageBox.Show("fichier bd1.xml créé dans le bin/Debug");


                        }
                    }
                }
                
            }
        }
    }
}
