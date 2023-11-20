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
    /// Logique d'interaction pour bdd.xaml
    /// </summary>
    public partial class bdd : Window
    {
        public bdd()
        {
            InitializeComponent();
        }
        private void clickval(object sender, RoutedEventArgs e)
        {
            if (client.IsChecked == true)
            {
                Client p = new Client();
                p.Show();
            }
            else
            {
                if(produit.IsChecked == true)
                {
                    Produit p = new Produit();
                    p.Show();
                }
                else
                {
                    if (standard.IsChecked == true)
                    {
                        bddstandard p = new bddstandard();
                        p.Show();
                    }
                }
            }
        }
    }
}
