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
    /// Interaction logic for WndConsVeiculo.xaml
    /// </summary>
    public partial class WndConsVeiculo : Window
    {
        public WndConsVeiculo()
        {
            InitializeComponent();
        }

        private LojaDataContext dc = new LojaDataContext();


        private void Window_Activated(object sender, EventArgs e)
        {
            selectFabricantes();
            
        }

        private void selectFabricantes()
        {
            var r = from f in dc.Fabricantes
                    orderby f.Descricao
                    select f;
            cbFab.ItemsSource = r.ToList();
            cbFab.SelectedValuePath = "id";
            cbFab.DisplayMemberPath = "Descricao";

        }

        private void buttonConsultar_Click(object sender, RoutedEventArgs e)
        {

            var r = from f in dc.Veiculos
                    where f.DataVenda == null && f.idFabricante == (int)cbFab.SelectedValue
                    select new
                    {
                        f.Modelo,
                        f.Ano,
                        f.Fabricante.Descricao,
                        f.DataCompra,
                        f.ValorCompra
                    };
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = r.ToList();
        }
    }
}
