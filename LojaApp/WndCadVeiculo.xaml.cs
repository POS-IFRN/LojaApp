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
    /// Interaction logic for WndCadVeiculo.xaml
    /// </summary>
    public partial class WndCadVeiculo : Window
    {
        public WndCadVeiculo()
        {
            InitializeComponent();
        }

        private LojaDataContext dc = new LojaDataContext();

        private void Window_Activated(object sender, EventArgs e)
        {

            selectFabricantes();
            dpDataCompra.SelectedDate = DateTime.Now;
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

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Veiculo v = new Veiculo();
            v.id = int.Parse(textboxId.Text);
            v.Modelo = textboxModelo.Text;
            v.Ano = int.Parse(textboxAno.Text);
            v.idFabricante = Convert.ToInt16(cbFab.SelectedValue);
            v.DataCompra = dpDataCompra.SelectedDate;
            v.ValorCompra = decimal.Parse(textboxVCompra.Text);
            v.PrecoVenda = decimal.Parse(textboxPVenda.Text);
            dc.Veiculos.InsertOnSubmit(v);
            dc.SubmitChanges();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            var r = from v in dc.Veiculos
                    select new
                    {
                        v.id,
                        v.Modelo,
                        v.Ano,
                        v.Fabricante.Descricao,
                        v.DataCompra,
                        v.PrecoVenda
                    };
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = r.ToList();

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Veiculo r = (from v in dc.Veiculos
                     where v.id == int.Parse(textboxId.Text)
                     select v).Single();
            if (textboxModelo.Text != "") r.Modelo = textboxModelo.Text;
            if (textboxAno.Text != "") r.Ano = int.Parse(textboxModelo.Text);
            if (textboxPVenda.Text != "") r.PrecoVenda = decimal.Parse(textboxPVenda.Text);
            if (textboxVCompra.Text != "") r.ValorCompra = decimal.Parse(textboxVCompra.Text);
            if (dpDataCompra.SelectedDate.ToString() != "") r.DataCompra = dpDataCompra.SelectedDate;
            dc.SubmitChanges();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Veiculo r = (from v in dc.Veiculos
                         where v.id == int.Parse(textboxId.Text)
                         select v).Single();
            dc.Veiculos.DeleteOnSubmit(r);
            dc.SubmitChanges();
        }
    }
}
