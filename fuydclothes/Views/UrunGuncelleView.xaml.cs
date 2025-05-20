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
    /// UrunGuncelleView.xaml etkileşim mantığı
    /// </summary>
    public partial class UrunGuncelleView : UserControl
    {
        public int urunid;
        public string urunad;
        public string urunmarka;
        public string urunkategori;
        public string urunrenk;
        public int urunsbedenadet;
        public int urunmbedenadet;
        public int urunlbedenadet;
        public int urunxlbedenadet;
        public int urunxxlbedenadet;
        public decimal urunfiyat;
        public string urunpasifmi;

        UrunClass urunler = new UrunClass();

        public UrunGuncelleView(int gelenurunid, string gelenurunad, string gelenurunmarka, string gelenurunkategori, string gelenurunrenk, int gelenurunsbedenadet, int gelenurunmbedenadet, int gelenurunlbedenadet, int gelenurunxlbedenadet, int gelenurunxxlbedenadet, decimal gelenurunfiyat, string gelenurunpasifmi)
        {
            InitializeComponent();

            urunid = gelenurunid;
            urunad = gelenurunad;
            urunmarka = gelenurunmarka;
            urunkategori = gelenurunkategori;
            urunrenk = gelenurunrenk;
            urunsbedenadet = gelenurunsbedenadet;
            urunmbedenadet = gelenurunmbedenadet;
            urunlbedenadet = gelenurunlbedenadet;
            urunxlbedenadet = gelenurunxlbedenadet;
            urunxxlbedenadet = gelenurunxxlbedenadet;
            urunfiyat = gelenurunfiyat;
            urunpasifmi = gelenurunpasifmi;

            yeniUrun_RenkCmbBox.Items.Add("Siyah");
            yeniUrun_RenkCmbBox.Items.Add("Beyaz");
            yeniUrun_RenkCmbBox.Items.Add("Gri");
            yeniUrun_RenkCmbBox.Items.Add("Taş Grisi");
            yeniUrun_RenkCmbBox.Items.Add("Bej");
            yeniUrun_RenkCmbBox.Items.Add("Krem");
            yeniUrun_RenkCmbBox.Items.Add("Haki");
            yeniUrun_RenkCmbBox.Items.Add("Yeşil");
            yeniUrun_RenkCmbBox.Items.Add("Koyu Yeşil");
            yeniUrun_RenkCmbBox.Items.Add("Mavi");
            yeniUrun_RenkCmbBox.Items.Add("Bebe Mavisi");
            yeniUrun_RenkCmbBox.Items.Add("Açık Mavi");
            yeniUrun_RenkCmbBox.Items.Add("Lacivert");
            yeniUrun_RenkCmbBox.Items.Add("Kırmızı");
            yeniUrun_RenkCmbBox.Items.Add("Pembe");
            yeniUrun_RenkCmbBox.Items.Add("Mor");
            yeniUrun_RenkCmbBox.Items.Add("Turuncu");
            yeniUrun_RenkCmbBox.Items.Add("Sarı");

            yeniUrun_UrunPasifMiCmbBox.Items.Add("Aktif");
            yeniUrun_UrunPasifMiCmbBox.Items.Add("Pasif");

            yeniUrun_KategoriCmbBox.ItemsSource = urunler.FillKategoriCombobox();

            eskiUrun_IDTxtBox.Text = urunid.ToString();
            eskiUrun_AdTxtBox.Text = urunad;
            eskiUrun_MarkaTxtBox.Text = urunmarka;
            eskiUrun_KategoriTxtBox.Text = urunkategori;
            eskiUrun_RenkTxtBox.Text = urunrenk;
            eskiUrun_SBedenTxtBox.Text = urunsbedenadet.ToString();
            eskiUrun_MBedenTxtBox.Text = urunmbedenadet.ToString();
            eskiUrun_LBedenTxtBox.Text = urunlbedenadet.ToString();
            eskiUrun_XLBedenTxtBox.Text = urunxlbedenadet.ToString();
            eskiUrun_XXLBedenTxtBox.Text = urunxxlbedenadet.ToString();
            eskiUrun_FiyatTxtBox.Text = urunfiyat.ToString();
            eskiUrun_UrunPasifMiTxtBox.Text = urunpasifmi;

            yeniUrun_IDTxtBox.Text = urunid.ToString();
            yeniUrun_AdTxtBox.Text = urunad;
            yeniUrun_MarkaTxtBox.Text = urunmarka;
            yeniUrun_KategoriCmbBox.Text = urunkategori;
            yeniUrun_RenkCmbBox.Text = urunrenk;
            yeniUrun_SBedenTxtBox.Text = urunsbedenadet.ToString();
            yeniUrun_MBedenTxtBox.Text = urunmbedenadet.ToString();
            yeniUrun_LBedenTxtBox.Text = urunlbedenadet.ToString();
            yeniUrun_XLBedenTxtBox.Text = urunxlbedenadet.ToString();
            yeniUrun_XXLBedenTxtBox.Text = urunxxlbedenadet.ToString();
            yeniUrun_FiyatTxtBox.Text = urunfiyat.ToString();
            yeniUrun_UrunPasifMiCmbBox.Text = urunpasifmi;
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Değişiklikleri iptal ederek 'Ürünler' ekranına dönmek istediğinize emin misiniz?", "Ürünler ekranına dön", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    mainWin.LoadPage("Urunler");
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    MessageBox.Show("Ürünler ekranına geri dönme işlemi iptal edilmiştir.");
                }
            }
        }

        private bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        private void yeniUrun_SBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void yeniUrun_MBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void yeniUrun_LBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void yeniUrun_XLBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void yeniUrun_XXLBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void kaydetButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                if (yeniUrun_AdTxtBox.Text.Length > 50 || yeniUrun_AdTxtBox.Text.Length < 5)
                {
                    MessageBox.Show("Lütfen 'Ürün Ad' kısmını en fazla 50 harf, en az 5 harften oluşacak şekilde giriniz.");
                }

                else if (yeniUrun_MarkaTxtBox.Text.Length > 50 || yeniUrun_MarkaTxtBox.Text.Length < 3)
                {
                    MessageBox.Show("Lütfen 'Ürün Marka' kısmını en fazla 50 harf, en az 5 harften oluşacak şekilde giriniz.");
                }

                else
                {
                    urunler.urunGuncelleVeSiparisleriDuzelt(yeniUrun_AdTxtBox.Text, yeniUrun_MarkaTxtBox.Text, yeniUrun_KategoriCmbBox.Text, yeniUrun_RenkCmbBox.Text, Convert.ToInt32(yeniUrun_SBedenTxtBox.Text), Convert.ToInt32(yeniUrun_MBedenTxtBox.Text), Convert.ToInt32(yeniUrun_LBedenTxtBox.Text), Convert.ToInt32(yeniUrun_XLBedenTxtBox.Text), Convert.ToInt32(yeniUrun_XXLBedenTxtBox.Text), Convert.ToDecimal(yeniUrun_FiyatTxtBox.Text), yeniUrun_UrunPasifMiCmbBox.Text, urunid);

                    MessageBox.Show("Yeni ürün bilgileri başarıyla güncellenmiştir. Ürünler ekranına yönlendiriliyorsunuz!");

                    mainWin.LoadPage("Urunler");
                }
            }
        }

        private void duzenlemeleriGeriAlButton_Click(object sender, RoutedEventArgs e)
        {
            string id = Convert.ToString(urunid);

            MessageBoxResult dialogResult = MessageBox.Show(id + "' ID li ürün için yapmış olduğunuz düzenlemeleri geri almak istediğinize emin misiniz?", "Düzenlemeleri geri al", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                yeniUrun_IDTxtBox.Text = urunid.ToString();
                yeniUrun_AdTxtBox.Text = urunad;
                yeniUrun_MarkaTxtBox.Text = urunmarka;
                yeniUrun_KategoriCmbBox.Text = urunkategori;
                yeniUrun_RenkCmbBox.Text = urunrenk;
                yeniUrun_SBedenTxtBox.Text = urunsbedenadet.ToString();
                yeniUrun_MBedenTxtBox.Text = urunmbedenadet.ToString();
                yeniUrun_LBedenTxtBox.Text = urunlbedenadet.ToString();
                yeniUrun_XLBedenTxtBox.Text = urunxlbedenadet.ToString();
                yeniUrun_XXLBedenTxtBox.Text = urunxxlbedenadet.ToString();
                yeniUrun_FiyatTxtBox.Text = urunfiyat.ToString();
                yeniUrun_UrunPasifMiCmbBox.Text = urunpasifmi;

                MessageBox.Show("Düzenlemeler başarıyla geri alınmıştır.");
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                MessageBox.Show("Düzenlemeleri geri alma işlemi iptal edilmiştir.");
            }
        }
    }
}
