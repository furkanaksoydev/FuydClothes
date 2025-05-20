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
    /// YeniKullaniciOlustur.xaml etkileşim mantığı
    /// </summary>
    public partial class YeniKullaniciOlustur : UserControl
    {
        KullaniciClass kullanici = new KullaniciClass();

        public YeniKullaniciOlustur()
        {
            InitializeComponent();
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Yeni kullanıcı oluşturma işlemini iptal ederek 'Kullanıcı' ekranına dönmek istediğinize emin misiniz?", "Kullanıcılar ekranına dön", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Kullanıcı ekranına dönülüyor.");

                    mainWin.LoadPage("Kullanicilar");
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    MessageBox.Show("Kullanıcı ekranına geri dönme işlemi iptal edilmiştir.");
                }
            }
        }

        private void kullaniciTelNoTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        private bool IsTextLetter(string text)
        {
            return text.All(c => Char.IsLetter(c));
        }

        private void kullaniciAdTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextLetter(e.Text);
        }

        private void kullaniciSoyadTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextLetter(e.Text);
        }

        private void kullaniciyiKaydetButton_Click(object sender, RoutedEventArgs e)
        {
            if (kullaniciAdTxtBox.Text.Length > 20 || kullaniciAdTxtBox.Text.Length < 3)
            {
                MessageBox.Show("Lütfen 'Kullanıcı Ad' kısmını en fazla 20 harf, en az 3 harften oluşacak şekilde giriniz.");
            }

            else if (kullaniciSoyadTxtBox.Text.Length > 20 || kullaniciSoyadTxtBox.Text.Length < 2)
            {
                MessageBox.Show("Lütfen 'Kullanıcı Soyad' kısmını en fazla 20 harf, en az 2 harften oluşacak şekilde giriniz.");
            }

            else if (kullaniciTelNoTxtBox.Text.Length != 11)
            {
                MessageBox.Show("Lütfen 'Kullanıcı Telefon Numarası' kısmını 11 haneden oluşacak şekilde giriniz.");
            }

            else if (kullanici.telefonVarMi(kullaniciTelNoTxtBox.Text))
            {
                MessageBox.Show("Bu telefon numarası zaten başka bir kullanıcı tarafından kullanılıyor. Lütfen farklı bir numara giriniz.");
            }

            else if (kullaniciAdresTxtBox.Text == "")
            {
                MessageBox.Show("Kullanıcı Adres kısmı boş bırakılamaz !");
            }

            else
            {
                kullanici.kullaniciEkle(kullaniciAdTxtBox.Text, kullaniciSoyadTxtBox.Text, kullaniciTelNoTxtBox.Text, kullaniciAdresTxtBox.Text);

                MessageBox.Show("Yeni kişi başarıyla kaydedilmiştir.");


                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("Kullanicilar");
                }
            }
        }
    }
}
