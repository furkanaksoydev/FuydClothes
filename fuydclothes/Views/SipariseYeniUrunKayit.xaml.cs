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
    /// SipariseYeniUrunKayit.xaml etkileşim mantığı
    /// </summary>
    public partial class SipariseYeniUrunKayit : UserControl
    {
        KullaniciClass kullanicilar = new KullaniciClass();
        UrunClass urunler = new UrunClass();
        SiparisUrunleriClass siparisurunleri = new SiparisUrunleriClass();
        SiparisClass siparis = new SiparisClass();

        int siparisid;

        public SipariseYeniUrunKayit(int gelensiparisid)
        {
            InitializeComponent();

            siparisid = gelensiparisid;

            siparisIDTxtBox.Text = Convert.ToString(siparisid);

            kullaniciTelNoTxtBox.Text = siparis.GetSiparisIDyeGoreTelNo(siparisid);

            foreach (var urun in urunler.FillSipariseUrunEklemekIcinUrunAdlariCmbBox())
            {
                urunAdCmbBox.Items.Add(urun);
            }
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage3("SiparisDetaylari", siparisid);
            }
        }

        private void urunAdCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string seciliUrunad = urunAdCmbBox.SelectedItem.ToString();
            fiyatTxtBox.Text = urunler.UrunAdaGoreUrunFiyatiniGetir(seciliUrunad).ToString("0.00");
        }

        private void siparisiKaydetButton_Click(object sender, RoutedEventArgs e)
        {
            if (urunAdCmbBox.Text == "")
            {
                MessageBox.Show("Ürün ad kısmı boş bırakılamaz !");
            }

            else if (bedenCmbBox.Text == "")
            {
                MessageBox.Show("Ürün için tanımlayacağınız beden kısmı boş bırakılamaz !");
            }

            else
            {

                int urunID = urunler.UrunIDGetir(urunAdCmbBox.Text);

                siparisurunleri.sipariseTekUrunEkle(Convert.ToInt32(siparisIDTxtBox.Text), urunID, urunAdCmbBox.Text, bedenCmbBox.Text, Convert.ToDecimal(fiyatTxtBox.Text));

                MessageBox.Show("Siparişe yeni ürün başarıyla kaydedilmiştir.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage3("SiparisDetaylari", siparisid);
                }
            }
        }
    }
}
