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
    /// KullanicilarView.xaml etkileşim mantığı
    /// </summary>
    public partial class KullanicilarView : UserControl
    {
        KullaniciClass kullanici = new KullaniciClass();

        public KullanicilarView()
        {
            InitializeComponent();

            string kirmizimi = "Aktif";

            DataGKisiler.ItemsSource = kullanici.KisiKirmiziMiFiltre(kirmizimi);
        }

        private void DataGKisiler_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Kullanici st = DataGKisiler.SelectedItem as Kullanici;
            string kullanicitelno = Convert.ToString(st.Kullanici_TelNo.ToString());

            seciliKisiTelNo.Text = kullanicitelno;
        }

        private void yeniKullaniciKayitButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("YeniKullaniciOlustur");
            }
        }

        private void kirmiziListeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("KirmiziKullanicilar");
            }
        }


        private void duzenleButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGKisiler.SelectedItem is Kullanici st)
            {
                // Kullanıcı bilgilerini al
                int kullaniciid = st.Kullanici_ID;
                string kullaniciad = st.Kullanici_Ad;
                string kullanicisoyad = st.Kullanici_Soyad;
                string kullanicitelno = st.Kullanici_TelNo;
                string kullaniciadres = st.Kullanici_Adres;
                string kullanicikirmizimi = st.Kullanici_Kirmizimi;

                // Parametrelerle LoadPage'i çağır tamamdır ben galiba çözdüm SORUN ÇÖZÜLDÜ GELDİĞİNDE ANLATICAM BAKİMMM
                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("KullaniciGuncelle", kullaniciid, kullaniciad, kullanicisoyad, kullanicitelno, kullaniciadres, kullanicikirmizimi);
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce bir kullanıcı seçiniz.");
            }
        }

        private void kirmiziyaAlButton_Click(object sender, RoutedEventArgs e)
        {
            Kullanici st = DataGKisiler.SelectedItem as Kullanici;
            string id = Convert.ToString(st.Kullanici_ID);
            string telno = Convert.ToString(st.Kullanici_TelNo);

            MessageBoxResult dialogResult = MessageBox.Show(telno + "' Telefon Numaralı kişiyi kırmızı listeye almak istediğinize emin misiniz?", "Kişiyi kırmızı listeye al", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                kullanici.kullaniciKirmizidanCikar("Kırmızı", st.Kullanici_ID);

                MessageBox.Show("Kişi başarıyla kırmızı listeye alınmıştır.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("Kullanicilar");
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                MessageBox.Show("Kişiyi kırmızı listeye alma işlemi iptal edilmiştir.");
            }
        }

        private void AraButton_Click(object sender, RoutedEventArgs e)
        {
            if (AraTxtBox.Text != "")
            {
                string telno = AraTxtBox.Text;

                DataGKisiler.ItemsSource = kullanici.FillDatagTelNoyaGore(telno);
            }
        }

        private void AramayiTemizleButton_Click(object sender, RoutedEventArgs e)
        {
            if (AraTxtBox.Text != "")
            {
                DataGKisiler.ItemsSource = kullanici.kullanicilar;
                AraTxtBox.Text = "";

                string aktifmi = "Aktif";

                DataGKisiler.ItemsSource = kullanici.KisiKirmiziMiFiltre(aktifmi);
            }
        }
    }
}
