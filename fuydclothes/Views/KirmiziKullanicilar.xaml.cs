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
    /// KirmiziKullanicilar.xaml etkileşim mantığı
    /// </summary>
    public partial class KirmiziKullanicilar : UserControl
    {
        KullaniciClass kullanici = new KullaniciClass();

        public KirmiziKullanicilar()
        {
            InitializeComponent();

            string kirmizimi = "Kırmızı";

            DataGKirmiziKisiler.ItemsSource = kullanici.KisiKirmiziMiFiltre(kirmizimi);
        }

        private void GeriDon_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("Kullanicilar");
            }
        }

        private void kullaniciyiAktifeAlButton_Click(object sender, RoutedEventArgs e)
        {
            Kullanici st = DataGKirmiziKisiler.SelectedItem as Kullanici;
            string id = Convert.ToString(st.Kullanici_ID);

            MessageBoxResult dialogResult = MessageBox.Show(id + "' ID li kişiyi aktif kişiler listesine almak istediğinize emin misiniz?", "Kişiyi aktife al", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                kullanici.kullaniciKirmizidanCikar("Aktif", st.Kullanici_ID);

                MessageBox.Show("Kullanıcı başarıyla aktife alınmıştır.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("KirmiziKullanicilar");
                }

            }
            else if (dialogResult == MessageBoxResult.No)
            {
                MessageBox.Show("Kullanıcıyı aktife alma işlemi iptal edilmiştir.");
            }
        }

        private void araButton_Click(object sender, RoutedEventArgs e)
        {
            if (AraTxtBox.Text != "")
            {
                string telno = AraTxtBox.Text;

                DataGKirmiziKisiler.ItemsSource = kullanici.FillKirmiziDatagTelNoyaGore(telno);
            }
        }

        private void aramayiTemizleButton_Click(object sender, RoutedEventArgs e)
        {
            if (AraTxtBox.Text != "")
            {
                DataGKirmiziKisiler.ItemsSource = kullanici.kullanicilar;
                AraTxtBox.Text = "";

                string kirmizimi = "Kırmızı";

                DataGKirmiziKisiler.ItemsSource = kullanici.KisiKirmiziMiFiltre(kirmizimi);
            }
        }
    }
}
