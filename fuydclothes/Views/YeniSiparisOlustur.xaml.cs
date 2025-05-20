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
    /// YeniSiparisOlustur.xaml etkileşim mantığı
    /// </summary>
    public partial class YeniSiparisOlustur : UserControl
    {
        KullaniciClass kullanicilar = new KullaniciClass();
        UrunClass urunler = new UrunClass();
        SiparisUrunleriClass siparisurunleri = new SiparisUrunleriClass();
        SiparisClass siparis = new SiparisClass();


        private List<string> tumKullanicilarinTelefonlari;
        private string secilenKullaniciTel = null;

        private List<string> tumUrunler;
        private string secilenUrun = null;

        public YeniSiparisOlustur()
        {
            InitializeComponent();

            tumKullanicilarinTelefonlari = kullanicilar.FillSipariseKullaniciEklemekIcinAktifTelNolari();

            tumUrunler = urunler.FillSipariseUrunEklemekIcinUrunAdlariCmbBox();

            siparisDetaylariDataGrid.ItemsSource = siparisurunleri.siparisDetayListesi;
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Yeni sipariş oluşturma işlemini iptal ederek 'Siparişler' ekranına dönmek istediğinize emin misiniz?", "Siparişler ekranına dön", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Siparişler ekranına dönülüyor.");

                    mainWin.LoadPage("Siparisler");
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    MessageBox.Show("Siparişler ekranına geri dönme işlemi iptal edilmiştir.");
                }
            }
        }

        // KULLANICI ARAMA KISMI

        private bool secimYapildiMi = false;
        private string oncekiGecerliTel = null;
        private bool secimZorunluKontrolEdildi = false;

        private void kullaniciTelAramaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tumKullanicilarinTelefonlari == null)
                return;

            string arama = kullaniciTelAramaTextBox.Text.ToLower();

            List<string> filtreli;

            if (string.IsNullOrWhiteSpace(arama))
            {
                filtreli = tumKullanicilarinTelefonlari;
            }
            else
            {
                filtreli = tumKullanicilarinTelefonlari
                    .Where(tel => tel.ToLower().Contains(arama))
                    .ToList();
            }

            if (filtreli.Any())
            {
                kullaniciListBox.ItemsSource = filtreli;
                kullaniciPopup.IsOpen = true;
            }
            else
            {
                kullaniciPopup.IsOpen = false;
            }

            secilenKullaniciTel = null;
        }

        private void kullaniciTelAramaTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tumKullanicilarinTelefonlari == null || !tumKullanicilarinTelefonlari.Any())
                return;

            kullaniciListBox.ItemsSource = tumKullanicilarinTelefonlari;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                kullaniciPopup.IsOpen = true;
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void kullaniciTelAramaTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (kullaniciPopup.IsOpen || secimZorunluKontrolEdildi)
                return;

            string mevcut = kullaniciTelAramaTextBox.Text.Trim();


            if (string.IsNullOrWhiteSpace(mevcut))  // Eğer boşsa biz şuan lostfocusta olduğumuz için önceden bir veri vardır ve bu veriyi kullanıcı silmiştir, bu yüzden uyarı vermiyoruz.
                return;

            // Eğer listede yoksa uyarı ver
            if (!tumKullanicilarinTelefonlari.Contains(mevcut))
            {
                secimZorunluKontrolEdildi = true;

                MessageBox.Show("Lütfen geçerli bir telefon numarası giriniz veya listeden seçiniz.");
                kullaniciListBox.ItemsSource = tumKullanicilarinTelefonlari;
                kullaniciPopup.IsOpen = true;
                secilenKullaniciTel = null;

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    kullaniciTelAramaTextBox.Focus();
                    kullaniciTelAramaTextBox.SelectAll();
                    secimZorunluKontrolEdildi = false;
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        private void kullaniciListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (kullaniciListBox.SelectedItem is string secilenTel)
            {
                kullaniciTelAramaTextBox.Text = secilenTel;
                secilenKullaniciTel = secilenTel;
                kullaniciPopup.IsOpen = false;

                var bilgiler = kullanicilar.KullaniciBilgileriniGetirTelIle(secilenTel);

                if (bilgiler != null)
                {
                    gelenKullaniciIDTxtBox.Text = bilgiler.Kullanici_ID.ToString();
                    gelenAdTxtBox.Text = bilgiler.Kullanici_Ad;
                    gelenSoyadTxtBox.Text = bilgiler.Kullanici_Soyad;
                    gelenTelNoTxtBox.Text = bilgiler.Kullanici_TelNo;
                    gelenAdresTxtBox.Text = bilgiler.Kullanici_Adres;
                }
                else
                {
                    MessageBox.Show("Bu telefon numarasıyla eşleşen aktif bir kullanıcı bulunamadı.");
                }

                kullaniciListBox.SelectedIndex = -1;

                oncekiGecerliTel = secilenTel;
                secimYapildiMi = true;
            }
        }

        private void kullaniciTelAramaTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            kullaniciTelAramaTextBox.SelectAll();
        }

        // ÜRÜN ARAMA KISMI
        private void aramaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tumUrunler == null)
                return;

            string arama = aramaTextBox.Text.ToLower();

            List<string> filtreli;

            if (string.IsNullOrWhiteSpace(arama))
            {

                filtreli = tumUrunler;  // Hiçbir şey yazılmadıysa tüm ürünleri gösteriyoruz.
            }
            else
            {
                filtreli = tumUrunler.Where(u => u.ToLower().Contains(arama)).ToList();  // AramaTxtBox içerisine yazdığımız harfleri ürün adının içinde bulunduran ürünleri listeliyoruz.
            }

            if (filtreli.Any())
            {
                urunListBox.ItemsSource = filtreli;
                urunPopup.IsOpen = true;
            }
            else
            {
                urunPopup.IsOpen = false;
            }

            secilenUrun = null;
        }


        private void aramaTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tumUrunler == null || !tumUrunler.Any())
                return;

            urunListBox.ItemsSource = tumUrunler;

            Dispatcher.BeginInvoke(new Action(() => {urunPopup.IsOpen = true;}), System.Windows.Threading.DispatcherPriority.Background);
        }


        private void urunListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (urunListBox.SelectedItem is string urun)
            {
                aramaTextBox.Text = urun;
                secilenUrun = urun;
                urunPopup.IsOpen = false;

                string seciliUrunad = aramaTextBox.Text;
                fiyatTextBox.Text = urunler.UrunAdaGoreUrunFiyatiniGetir(seciliUrunad).ToString("0.00");


                urunListBox.SelectedIndex = -1;  // Bir ürün seçtiğimizde herhangi bir harf değişikliğinde aynı ürünü tekrar seçemiyoruz, buna çözüm olması adına seçili indexi resetliyoruz.
            }
        }

        private void aramaTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            aramaTextBox.SelectAll();  // Mouse ile TextBoxa çift tıklandığında tüm yazı satırını seçiyoruz.
        }

        private void aramaTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            urunPopup.IsOpen = false;  // aramaTextBox ı açıkken başka bir yere tıklanırsa listboxın kapanmasını sağlıyoruz.
        }

        private void sipariseUrunEkleButton_Click(object sender, RoutedEventArgs e)
        {
            var secilenTel = gelenTelNoTxtBox.Text;  // Seçilen telefon numarasının üzerine farklı bir sayı girilmesi veya olan sayıların değiştirilmesi gibi bir durumda yazılmış olan telefon numarası bizim listemizde bulunmadığı için bu durumu engelliyoruz ki veritabanımızda olmayan bir telefon numarası ile sipariş kaydı yapılmasın.

            if (!tumKullanicilarinTelefonlari.Contains(secilenTel))
            {
                MessageBox.Show("Lütfen listeden geçerli bir telefon numarası seçiniz.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else if (!tumUrunler.Contains(aramaTextBox.Text))  // Seçilen ürünün üzerine farklı bir harf, yazı gibi birşey eklendiğinde yazılmış olan ürün adı bizim listemizde bulunmadığı için bu durumu engelliyoruz ki siparişimize veritabanımızda olmayan bir ürün kaydı yapılmasın.
            {
                MessageBox.Show("Lütfen listeden geçerli bir ürün seçiniz.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else if (aramaTextBox.Text == null || bedenComboBox.SelectedItem == null || string.IsNullOrEmpty(fiyatTextBox.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            else
            {

                SiparisUrunleri yeniUrun = new SiparisUrunleri
                {
                    Urun_ID = urunler.UrunIDGetir(aramaTextBox.Text),
                    Urun_Ad = aramaTextBox.Text,
                    Urun_Beden = (bedenComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Urun_Fiyat = decimal.Parse(fiyatTextBox.Text)
                };

                siparisurunleri.siparisDetayListesi.Add(yeniUrun);

                siparisDetaylariDataGrid.Items.Refresh();

                MessageBox.Show("Ürün listeye eklendi.");
            }
        }

        private void siparisiKaydetButton_Click(object sender, RoutedEventArgs e)
        {
            if (siparisurunleri.siparisDetayListesi.Count == 0 || string.IsNullOrEmpty(gelenKullaniciIDTxtBox.Text))
            {
                MessageBox.Show("Lütfen önce müşteri seçin ve en az bir ürün ekleyin.");
                return;
            }

            else
            {
                int musteriID = int.Parse(gelenKullaniciIDTxtBox.Text);
                string musteriAd = gelenAdTxtBox.Text;
                string musteriSoyad = gelenSoyadTxtBox.Text;
                string musteriTel = gelenTelNoTxtBox.Text;
                string musteriAdres = gelenAdresTxtBox.Text;

                int urunSayisi = siparisurunleri.siparisDetayListesi.Count;
                decimal toplamFiyat = siparisurunleri.siparisDetayListesi.Sum(x => x.Urun_Fiyat);


                int yeniSiparisID = siparis.siparisEkle(musteriID, musteriAd, musteriSoyad, musteriTel, musteriAdres, urunSayisi, toplamFiyat, "Hayır", "Hayır"); //ÖNCE SİPARİŞ OLARAK EKLİYORUZ

                siparisurunleri.sipariseUrunEkle(yeniSiparisID, siparisurunleri.siparisDetayListesi); // SONRA SİPARİŞE ÜRÜNLERİ EKLİYORUZ

                MessageBox.Show("Sipariş başarıyla kaydedildi.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("YeniSiparisOlustur");
                }
            }
        }

        private void siparistekiUrunuSilButton_Click(object sender, RoutedEventArgs e)
        {
            var silButonu = sender as Button;
            var silinecekUrun = silButonu?.DataContext as SiparisUrunleri;

            if (silinecekUrun != null)
            {
                siparisurunleri.siparisDetayListesi.Remove(silinecekUrun);
                siparisDetaylariDataGrid.Items.Refresh();
            }
        }
    }
}
