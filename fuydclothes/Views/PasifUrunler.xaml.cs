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
    /// PasifUrunler.xaml etkileşim mantığı
    /// </summary>
    public partial class PasifUrunler : UserControl
    {
        UrunClass urun = new UrunClass();

        public PasifUrunler()
        {
            InitializeComponent();

            string aktifmi = "Pasif";

            DataGPasifUrunler.ItemsSource = urun.FillAktifUrunFiltre(aktifmi);
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("Urunler");
            }
        }

        private void urunVerileriniGuncelleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void urunuAktifeAlButton_Click(object sender, RoutedEventArgs e)
        {
            Urun st = DataGPasifUrunler.SelectedItem as Urun;
            string id = Convert.ToString(st.Urun_ID);

            MessageBoxResult dialogResult = MessageBox.Show(id + "' ID li ürünü aktif ürünler listesine almak istediğinize emin misiniz?", "Ürünü aktife al", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                urun.urunuPasiftenCikar("Aktif", st.Urun_ID);

                MessageBox.Show("Ürün başarıyla aktife alınmıştır.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("PasifUrunler");
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                MessageBox.Show("Ürünü aktife alma işlemi iptal edilmiştir.");
            }
        }
    }
}
