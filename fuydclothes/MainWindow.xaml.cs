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

namespace fuydclothes
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadPage("AnaEkran");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Uygulamayı kapatmak istediğinize emin misiniz?",
                                         "Çıkış Onayı",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string pageName)
            {
                LoadPage(pageName);
            }
        }

        public void LoadPage(string pageName)
        {
            switch (pageName)
            {
                case "AnaEkran":
                    MainContentControl.Content = new Views.AnaEkranView();
                    break;
                case "Kullanicilar":
                    MainContentControl.Content = new Views.KullanicilarView();
                    break;
                case "YeniKullaniciOlustur":
                    MainContentControl.Content = new Views.YeniKullaniciOlustur();
                    break;
                case "KirmiziKullanicilar":
                    MainContentControl.Content = new Views.KirmiziKullanicilar();
                    break;
                case "Siparisler":
                    MainContentControl.Content = new Views.SiparislerView();
                    break;
                case "YeniSiparisOlustur":
                    MainContentControl.Content = new Views.YeniSiparisOlustur();
                    break;
                case "SiparisIadeleri":
                    MainContentControl.Content = new Views.SiparisIadeleri();
                    break;
                case "Urunler":
                    MainContentControl.Content = new Views.UrunlerView();
                    break;
                case "YeniUrunOlustur":
                    MainContentControl.Content = new Views.YeniUrunOlustur();
                    break;
                case "PasifUrunler":
                    MainContentControl.Content = new Views.PasifUrunler();
                    break;
            }
        }


        public void LoadPage(string pageName, int kullaniciid, string kullaniciad, string kullanicisoyad, string kullanicitelno, string kullaniciadres, string kullanicikirmizimi)
        {
            switch (pageName)
            {
                case "AnaEkran":
                    MainContentControl.Content = new Views.AnaEkranView();
                    break;
                case "Kullanicilar":
                    MainContentControl.Content = new Views.KullanicilarView();
                    break;
                case "Siparisler":
                    MainContentControl.Content = new Views.SiparislerView();
                    break;
                case "Urunler":
                    MainContentControl.Content = new Views.UrunlerView();
                    break;
                case "KullaniciGuncelle":
                    // Parametreleri KullaniciGuncelleView'e geçiyoruz
                    MainContentControl.Content = new Views.KullaniciGuncelleView(
                        kullaniciid,
                        kullaniciad,
                        kullanicisoyad,
                        kullanicitelno,
                        kullaniciadres,
                        kullanicikirmizimi
                    );
                    break;
            }
        }

        public void LoadPage2(string pageName, int gelenurunid, string gelenurunad, string gelenurunmarka, string gelenurunkategori, string gelenurunrenk, int gelenurunsbedenadet, int gelenurunmbedenadet, int gelenurunlbedenadet, int gelenurunxlbedenadet, int gelenurunxxlbedenadet, decimal gelenurunfiyat, string gelenurunpasifmi)
        {
            switch (pageName)
            {
                case "AnaEkran":
                    MainContentControl.Content = new Views.AnaEkranView();
                    break;
                case "Kullanicilar":
                    MainContentControl.Content = new Views.KullanicilarView();
                    break;
                case "Siparisler":
                    MainContentControl.Content = new Views.SiparislerView();
                    break;
                case "Urunler":
                    MainContentControl.Content = new Views.UrunlerView();
                    break;
                case "UrunGuncelle":
                    MainContentControl.Content = new Views.UrunGuncelleView(
                        gelenurunid,
                        gelenurunad,
                        gelenurunmarka,
                        gelenurunkategori,
                        gelenurunrenk,
                        gelenurunsbedenadet,
                        gelenurunmbedenadet,
                        gelenurunlbedenadet,
                        gelenurunxlbedenadet,
                        gelenurunxxlbedenadet,
                        gelenurunfiyat,
                        gelenurunpasifmi
                    );
                    break;
            }
        }

        public void LoadPage3(string pageName, int kullaniciid)
        {
            switch (pageName)
            {
                case "AnaEkran":
                    MainContentControl.Content = new Views.AnaEkranView();
                    break;
                case "Kullanicilar":
                    MainContentControl.Content = new Views.KullanicilarView();
                    break;
                case "Siparisler":
                    MainContentControl.Content = new Views.SiparislerView();
                    break;
                case "Urunler":
                    MainContentControl.Content = new Views.UrunlerView();
                    break;
                case "SiparisDetaylari":
                    // Parametreleri KullaniciGuncelleView'e geçiyoruz
                    MainContentControl.Content = new Views.SiparisDetaylari(
                        kullaniciid
                    );
                    break;
            }
        }

        public void LoadPage4(string pageName, int gelensiparisid)
        {
            switch (pageName)
            {
                case "AnaEkran":
                    MainContentControl.Content = new Views.AnaEkranView();
                    break;
                case "Kullanicilar":
                    MainContentControl.Content = new Views.KullanicilarView();
                    break;
                case "Siparisler":
                    MainContentControl.Content = new Views.SiparislerView();
                    break;
                case "Urunler":
                    MainContentControl.Content = new Views.UrunlerView();
                    break;
                case "SipariseYeniUrunKayit":
                    // Parametreleri KullaniciGuncelleView'e geçiyoruz
                    MainContentControl.Content = new Views.SipariseYeniUrunKayit(
                        gelensiparisid
                    );
                    break;
            }
        }

        public void LoadPage5(string pageName, int gelensiparisid, int gelenurunsiparisid, string gelenurunad, string gelensiparisbeden, decimal gelenurunfiyat)
        {
            switch (pageName)
            {
                case "AnaEkran":
                    MainContentControl.Content = new Views.AnaEkranView();
                    break;
                case "Kullanicilar":
                    MainContentControl.Content = new Views.KullanicilarView();
                    break;
                case "Siparisler":
                    MainContentControl.Content = new Views.SiparislerView();
                    break;
                case "Urunler":
                    MainContentControl.Content = new Views.UrunlerView();
                    break;
                case "SiparistekiUrunuGuncelle":
                    // Parametreleri KullaniciGuncelleView'e geçiyoruz
                    MainContentControl.Content = new Views.SiparistekiUrunuGuncelle(
                        gelensiparisid,
                        gelenurunsiparisid,
                        gelenurunad,
                        gelensiparisbeden,
                        gelenurunfiyat
                    );
                    break;
            }
        }

    }
}