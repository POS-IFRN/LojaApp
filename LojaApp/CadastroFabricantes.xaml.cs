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

namespace LojaApp
{
    /// <summary>
    /// Interaction logic for CadastroFabricantes.xaml
    /// </summary>
    public partial class CadastroFabricantes : Window
    {
        public CadastroFabricantes()
        {
            InitializeComponent();
        }

        private void buttonSelect_Click(object sender, RoutedEventArgs e)
        {
            LojaDataContext dc = new LojaDataContext();
            dataGrid.ItemsSource = (from f in dc.Fabricantes select f);
        }
    }
}
