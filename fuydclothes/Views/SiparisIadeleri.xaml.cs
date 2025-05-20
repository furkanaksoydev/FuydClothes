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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fuydclothes.Views
{
    /// <summary>
    /// SiparisIadeleri.xaml etkileşim mantığı
    /// </summary>
    public partial class SiparisIadeleri : UserControl
    {
        SiparisClass siparis = new SiparisClass();

        public SiparisIadeleri()
        {
            InitializeComponent();

            string iademi = "İade";

            DataGIadeSiparisler.ItemsSource = siparis.FillSiparisIademi(iademi);
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("Siparisler");
            }
        }

        private void iadeSiparisDetayButton_Click(object sender, RoutedEventArgs e)
        {
            Siparis st = DataGIadeSiparisler.SelectedItem as Siparis;
            int siparisid = st.Siparis_ID;

            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage3("SiparisDetaylari", siparisid);
            }
        }

        private void AraButton_Click(object sender, RoutedEventArgs e)
        {
            if (AraTxtBox.Text != "")
            {
                int siparisid = Convert.ToInt32(AraTxtBox.Text);

                DataGIadeSiparisler.ItemsSource = siparis.FillIadeDataGIDyeGore(siparisid);
            }
        }

        private void AramayiTemizleButton_Click(object sender, RoutedEventArgs e)
        {
            if (AraTxtBox.Text != "")
            {
                DataGIadeSiparisler.ItemsSource = siparis.siparisler;
                AraTxtBox.Text = "";

                string iademi = "İade";

                DataGIadeSiparisler.ItemsSource = siparis.FillSiparisIademi(iademi);
            }
        }

        private void siparisiAktifeAlButton_Click(object sender, RoutedEventArgs e)
        {
            Siparis st = DataGIadeSiparisler.SelectedItem as Siparis;
            string id = Convert.ToString(st.Siparis_ID);

            MessageBoxResult dialogResult = MessageBox.Show(id + "' ID li siparişi aktif siparişler listesine almak istediğinize emin misiniz?", "Siparişi aktife al", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                siparis.siparisIadeyeAl("Hayır", st.Siparis_ID);

                MessageBox.Show("Sipariş başarıyla aktife alınmıştır.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("SiparisIadeleri");
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                MessageBox.Show("Siparişi aktife alma işlemi iptal edilmiştir.");
            }
        }
    }
}
