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
    /// SiparistekiUrunuGuncelle.xaml etkileşim mantığı
    /// </summary>
    public partial class SiparistekiUrunuGuncelle : UserControl
    {
        SiparisUrunleriClass siparisurunleri = new SiparisUrunleriClass();

        public int siparisid;
        public int urunsiparisid;
        public string urunad;
        public string beden;
        public decimal urunfiyat;

        public SiparistekiUrunuGuncelle(int gelensiparisid, int gelenurunsiparisid, string gelenurunad, string gelensiparisbeden, decimal gelenurunfiyat)
        {
            InitializeComponent();

            siparisid = gelensiparisid;
            urunsiparisid = gelenurunsiparisid;
            urunad = gelenurunad;
            beden = gelensiparisbeden;
            urunfiyat = gelenurunfiyat;

            SiparisIDTxtBox.Text = Convert.ToString(siparisid);

            UrunAdTxtBox.Text = urunad;

            UrunBedenCmbBox.Items.Add("S");
            UrunBedenCmbBox.Items.Add("M");
            UrunBedenCmbBox.Items.Add("L");
            UrunBedenCmbBox.Items.Add("XL");
            UrunBedenCmbBox.Items.Add("XXL");

            UrunBedenCmbBox.Text = beden;

            UrunFiyatTxtBox.Text = Convert.ToString(urunfiyat);
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage3("SiparisDetaylari", siparisid);
            }
        }

        private void urunuKaydetButton_Click(object sender, RoutedEventArgs e)
        {
            if (UrunBedenCmbBox.Text == beden)
            {
                MessageBox.Show("Siparişte güncellenecek olan ürün için beden bilgisi eski beden ile aynı !");
            }

            else
            {
                siparisurunleri.siparistekiUrunuGuncelle(urunsiparisid, UrunAdTxtBox.Text, UrunBedenCmbBox.Text, Convert.ToDecimal(UrunFiyatTxtBox.Text), Convert.ToInt32(SiparisIDTxtBox.Text));

                MessageBox.Show("Sipariş içerisindeki ürün bilgileri başarıyla kaydedilmiştir.");


                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage3("SiparisDetaylari", siparisid);
                }
            }
        }
    }
}
