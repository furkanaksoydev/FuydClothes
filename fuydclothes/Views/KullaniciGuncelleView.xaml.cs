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
    /// KullaniciGuncelleView.xaml etkileşim mantığı
    /// </summary>
    public partial class KullaniciGuncelleView : UserControl
    {
        public int kullaniciid;
        public string kullaniciad;
        public string kullanicisoyad;
        public string kullanicitelno;
        public string kullaniciadres;
        public string kullanicikirmizimi;

        KullaniciClass kullanicilar = new KullaniciClass();

        public KullaniciGuncelleView(int eskiid, string eskiad, string eskisoyad, string eskitelno, string eskiadres, string eskikirmizimi)
        {
            InitializeComponent();

            kullaniciid = eskiid;
            kullaniciad = eskiad;
            kullanicisoyad = eskisoyad;
            kullanicitelno = eskitelno;
            kullaniciadres = eskiadres;
            kullanicikirmizimi = eskikirmizimi;

            yeniKullanici_AktifMiCmbBox.Items.Add("Aktif");
            yeniKullanici_AktifMiCmbBox.Items.Add("Kırmızı");

            eskiKullanici_IDTxtBox.Text = kullaniciid.ToString();
            eskiKullanici_AdTxtBox.Text = kullaniciad;
            eskiKullanici_SoyadTxtBox.Text = kullanicisoyad;
            eskiKullanici_TelNoTxtBox.Text = kullanicitelno;
            eskiKullanici_AdresTxtBox.Text = kullaniciadres;
            eskiKullanici_AktifMiTxtBox.Text = kullanicikirmizimi;

            yeniKullanici_IDTxtBox.Text = kullaniciid.ToString();
            yeniKullanici_AdTxtBox.Text = kullaniciad;
            yeniKullanici_SoyadTxtBox.Text = kullanicisoyad;
            yeniKullanici_TelNoTxtBox.Text = kullanicitelno;
            yeniKullanici_AdresTxtBox.Text = kullaniciadres;
            yeniKullanici_AktifMiCmbBox.Text = kullanicikirmizimi;
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                MessageBoxResult dialogResult = MessageBox.Show("'Kullanıcı' ekranına dönmek istediğinize emin misiniz?", "Kullanıcılar ekranına dön", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    mainWin.LoadPage("Kullanicilar");
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    MessageBox.Show("Kullanıcılar ekranına geri dönme işlemi iptal edilmiştir.");
                }
            }
        }

        private bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        private void yeniKullanici_TelNoTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextLetter(string text)
        {
            return text.All(c => Char.IsLetter(c));
        }

        private void yeniKullanici_AdTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextLetter(e.Text);
        }

        private void yeniKullanici_SoyadTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextLetter(e.Text);
        }

        private void kaydetButton_Click(object sender, RoutedEventArgs e)
        {
            if (yeniKullanici_AdTxtBox.Text.Length > 20 || yeniKullanici_AdTxtBox.Text.Length < 3)
            {
                MessageBox.Show("Lütfen 'Kullanıcı Ad' kısmını en fazla 20 harf, en az 3 harften oluşacak şekilde giriniz.");
            }

            else if (yeniKullanici_SoyadTxtBox.Text.Length > 20 || yeniKullanici_SoyadTxtBox.Text.Length < 2)
            {
                MessageBox.Show("Lütfen 'Kullanıcı Soyad' kısmını en fazla 20 harf, en az 2 harften oluşacak şekilde giriniz.");
            }

            else if (yeniKullanici_TelNoTxtBox.Text.Length != 11)
            {
                MessageBox.Show("Lütfen 'Kullanıcı Telefon Numarası' kısmını 11 haneden oluşacak şekilde giriniz.");
            }

            else if (kullanicilar.kullaniciGuncellemeTelefonNumarasiKontrolu(yeniKullanici_TelNoTxtBox.Text, kullaniciid))
            {
                MessageBox.Show("Bu telefon numarası zaten başka bir kullanıcıya kayıtlı. Lütfen farklı bir numara giriniz.");
            }

            else
            {
                kullanicilar.kullaniciGuncelleVeSiparisleriDuzelt(yeniKullanici_AdTxtBox.Text, yeniKullanici_SoyadTxtBox.Text, yeniKullanici_TelNoTxtBox.Text, yeniKullanici_AdresTxtBox.Text, yeniKullanici_AktifMiCmbBox.Text, kullaniciid);

                MessageBox.Show("Yeni kişi bilgileri başarıyla güncellenmiştir. Kullanıcı ekranına yönlendiriliyorsunuz!");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("Kullanicilar");
                }
            }
        }

        private void duzenlemeleriGeriAlButton_Click(object sender, RoutedEventArgs e)
        {
            string id = Convert.ToString(kullaniciid);

            MessageBoxResult dialogResult = MessageBox.Show(id + "' ID li kişi için yapmış olduğunuz düzenlemeleri geri almak istediğinize emin misiniz?", "Düzenlemeleri geri al", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                yeniKullanici_IDTxtBox.Text = kullaniciid.ToString();
                yeniKullanici_AdTxtBox.Text = kullaniciad;
                yeniKullanici_SoyadTxtBox.Text = kullanicisoyad;
                yeniKullanici_TelNoTxtBox.Text = kullanicitelno;
                yeniKullanici_AdresTxtBox.Text = kullaniciadres;
                yeniKullanici_AktifMiCmbBox.Text = kullanicikirmizimi;

                MessageBox.Show("Düzenlemeler başarıyla geri alınmıştır.");
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                MessageBox.Show("Düzenlemeleri geri alma işlemi iptal edilmiştir.");
            }
        }
    }
}
