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

        private LojaDataContext dc = new LojaDataContext();

        private void buttonSelect_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = (from f in dc.Fabricantes select f);
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            Fabricante f = new Fabricante();
            f.id = int.Parse(textBoxID.Text);
            f.Descricao = textBoxDescricao.Text;
            dc.Fabricantes.InsertOnSubmit(f);
            dc.SubmitChanges();
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Fabricante r = (from f in dc.Fabricantes
                            where f.id == int.Parse(textBoxID.Text)
                            select f).Single();
            r.Descricao = textBoxDescricao.Text;
            dc.SubmitChanges();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Fabricante r = (from f in dc.Fabricantes
                            where f.id == int.Parse(textBoxID.Text)
                            select f).Single();
            deleteChilds(r.id);
            dc.Fabricantes.DeleteOnSubmit(r);
            dc.SubmitChanges();
        }

        private void deleteChilds(int id)
        {
            var carrosdofabricanteQuery = (from v in dc.Veiculos
                                           where v.Fabricante.id == id
                                           select v);
            foreach (var carro in carrosdofabricanteQuery) dc.Veiculos.DeleteOnSubmit(carro);
        }
    }
}
