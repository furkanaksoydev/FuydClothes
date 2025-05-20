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
    /// SiparisDetaylari.xaml etkileşim mantığı
    /// </summary>
    public partial class SiparisDetaylari : UserControl
    {
        public int siparisno;

        SiparisUrunleriClass siparisurunleri = new SiparisUrunleriClass();

        public SiparisDetaylari(int eskisiparisno)
        {
            InitializeComponent();

            siparisno = eskisiparisno;

            DataGSiparisUrunleri.ItemsSource = siparisurunleri.FillSiparisUrunleri(eskisiparisno);
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("Siparisler");
            }
        }

        private void sipariseYeniUrunKayitButton_Click(object sender, RoutedEventArgs e)
        {
            SiparisUrunleri st = DataGSiparisUrunleri.SelectedItem as SiparisUrunleri;

            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage4("SipariseYeniUrunKayit", siparisno);
            }
        }

        private void urunuGuncelleButton_Click(object sender, RoutedEventArgs e)
        {
            SiparisUrunleri st = DataGSiparisUrunleri.SelectedItem as SiparisUrunleri;
            int siparisid = st.Siparis_ID;
            int siparisurunid = st.Siparis_Urunleri_ID;
            string urunad = st.Urun_Ad;
            string urunbeden = st.Urun_Beden;
            decimal urunfiyat = st.Urun_Fiyat;

            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage5("SiparistekiUrunuGuncelle", siparisid, siparisurunid, urunad, urunbeden, urunfiyat);
            }
        }

        private void urunuSilButton_Click(object sender, RoutedEventArgs e)
        {
            SiparisUrunleri st = DataGSiparisUrunleri.SelectedItem as SiparisUrunleri;
            string id = Convert.ToString(st.Siparis_Urunleri_ID);
            string ad = Convert.ToString(st.Urun_Ad);

            MessageBoxResult dialogResult = MessageBox.Show(id + "' Sipariş ID'li ürünü sipariş içerisinden çıkarmak istediğinize emin misiniz?", "Ürünü siparişten çıkar", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                SiparisUrunleri st2 = DataGSiparisUrunleri.SelectedItem as SiparisUrunleri;
                int siparisurunid = st2.Siparis_Urunleri_ID;
                int siparisid = st2.Siparis_ID;
                string urunad = st2.Urun_Ad;
                string urunbeden = st2.Urun_Beden;
                decimal urunfiyat = st2.Urun_Fiyat;

                siparisurunleri.siparistekiUrunuSil(siparisurunid, siparisid, urunad, urunbeden, urunfiyat);

                MessageBox.Show("Ürün başarıyla siparişten çıkarılmıştır.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage3("SiparisDetaylari", siparisid);
                }
            }
        }
    }
}
