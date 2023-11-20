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

namespace Projet_BDD_WPF
{
    /// <summary>
    /// Logique d'interaction pour ChoixCommande.xaml
    /// </summary>
    public partial class ChoixCommande : Window
    {
        String Email;
        public ChoixCommande(String email)
        {
            this.Email = email;
            InitializeComponent();
        }

        private void clickvalider(object sender, RoutedEventArgs e)
        {
            if (perso.IsChecked==true)
            {
                //ouvrir fenetre personnalisée
                Personnalisée p = new Personnalisée(this.Email);
                p.Show();
                //fermer la fenetre 
            }
            else
            {
                if (standard.IsChecked == true)
                {
                    Standard p = new Standard(this.Email);
                    p.Show();
                    //fermer la fenetre 
                }
            }
            
        }
    }
}
