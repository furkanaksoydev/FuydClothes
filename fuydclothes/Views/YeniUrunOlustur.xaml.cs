using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// YeniUrunOlustur.xaml etkileşim mantığı
    /// </summary>
    public partial class YeniUrunOlustur : UserControl
    {
        UrunClass urun = new UrunClass();

        public YeniUrunOlustur()
        {
            InitializeComponent();

            RenkCmbBox.Items.Add("Siyah");
            RenkCmbBox.Items.Add("Beyaz");
            RenkCmbBox.Items.Add("Gri");
            RenkCmbBox.Items.Add("Taş Grisi");
            RenkCmbBox.Items.Add("Bej");
            RenkCmbBox.Items.Add("Krem");
            RenkCmbBox.Items.Add("Haki");
            RenkCmbBox.Items.Add("Yeşil");
            RenkCmbBox.Items.Add("Koyu Yeşil");
            RenkCmbBox.Items.Add("Mavi");
            RenkCmbBox.Items.Add("Bebe Mavisi");
            RenkCmbBox.Items.Add("Açık Mavi");
            RenkCmbBox.Items.Add("Lacivert");
            RenkCmbBox.Items.Add("Kırmızı");
            RenkCmbBox.Items.Add("Pembe");
            RenkCmbBox.Items.Add("Mor");
            RenkCmbBox.Items.Add("Turuncu");
            RenkCmbBox.Items.Add("Sarı");

            KategoriCmbBox.ItemsSource = urun.FillKategoriCombobox();
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Yeni ürün oluşturma işlemini iptal ederek 'Ürünler' ekranına dönmek istediğinize emin misiniz?", "Ürünler ekranına dön", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Ürünler ekranına dönülüyor.");

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

        private bool IsValidDecimal(string text)
        {
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.GetCultureInfo("tr-TR"), out _);
        }

        private void SBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void MBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void LBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void XLBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void XXLBedenTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void FiyatTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            e.Handled = !IsValidDecimal(newText);
        }

        private void urunuKaydetButton_Click(object sender, RoutedEventArgs e)
        {
            if (UrunAdTxtBox.Text == "")
            {
                MessageBox.Show("Ürün Ad kısmı boş bırakılamaz !");
            }

            else if (urun.urunAdiVarMi(UrunAdTxtBox.Text))
            {
                MessageBox.Show("Bu ürün adı zaten mevcut. Lütfen farklı bir ürün adı giriniz.");
            }


            else if (UrunMarkaTxtBox.Text == "")
            {
                MessageBox.Show("Ürün Marka kısmı boş bırakılamaz !");
            }

            else if (KategoriCmbBox.Text == "")
            {
                MessageBox.Show("Lütfen ekleyeceğiniz ürün stoğu için bir 'Kategori' seçiniz.");
            }

            else if (RenkCmbBox.Text == "")
            {
                MessageBox.Show("Lütfen ekleyeceğiniz ürün stoğu için bir 'Renk' seçiniz.");
            }

            else
            {
                urun.urunEkle(UrunAdTxtBox.Text, UrunMarkaTxtBox.Text, KategoriCmbBox.Text, RenkCmbBox.Text, (Convert.ToInt32(SBedenTxtBox.Text)), (Convert.ToInt32(MBedenTxtBox.Text)), (Convert.ToInt32(LBedenTxtBox.Text)), (Convert.ToInt32(XLBedenTxtBox.Text)), (Convert.ToInt32(XXLBedenTxtBox.Text)), (Convert.ToDecimal(FiyatTxtBox.Text)));

                MessageBox.Show("Yeni ürün stoğa başarıyla kaydedilmiştir.");


                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("Urunler");
                }
            }
        }
    }
}
