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
    /// SiparislerView.xaml etkileşim mantığı
    /// </summary>
    public partial class SiparislerView : UserControl
    {
        SiparisClass siparis = new SiparisClass();

        public SiparislerView()
        {
            InitializeComponent();

            string iademi = "Hayır";

            DataGSiparisler.ItemsSource = siparis.FillSiparisIademi(iademi);
        }

        private void DataGSiparisler_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Siparis st = DataGSiparisler.SelectedItem as Siparis;
            string siparisid = Convert.ToString(st.Siparis_ID.ToString());

            seciliKisiID.Text = siparisid;
        }

        private void AraButton_Click(object sender, RoutedEventArgs e)
        {
            if (AraTxtBox.Text != "")
            {
                int siparisid = Convert.ToInt32(AraTxtBox.Text);

                DataGSiparisler.ItemsSource = siparis.FillDataGIDyeGore(siparisid);
            }
        }

        private void AramayiTemizleButton_Click(object sender, RoutedEventArgs e)
        {
            if (AraTxtBox.Text != "")
            {
                DataGSiparisler.ItemsSource = siparis.siparisler;
                AraTxtBox.Text = "";

                string iademi = "Hayır";

                DataGSiparisler.ItemsSource = siparis.FillSiparisIademi(iademi);
            }
        }

        private void yeniSiparisKayitButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("YeniSiparisOlustur");
            }
        }

        private void iadelerButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("SiparisIadeleri");
            }
        }

        private void siparisDetaylariButton_Click(object sender, RoutedEventArgs e)
        {
            Siparis st = DataGSiparisler.SelectedItem as Siparis;
            int siparisid = st.Siparis_ID;

            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage3("SiparisDetaylari", siparisid);
            }
        }

        private void siparisiOnaylaButton_Click(object sender, RoutedEventArgs e)
        {
            Siparis st = DataGSiparisler.SelectedItem as Siparis;
            string id = Convert.ToString(st.Siparis_ID);

            MessageBoxResult dialogResult = MessageBox.Show(id + "' ID li siparişin ulaştığına ve siparişi onaylamak istediğinize emin misiniz?", "Siparişi onayla", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                siparis.siparisOnayla("Evet", st.Siparis_ID);

                MessageBox.Show("Sipariş başarıyla onaylanmıştır.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("Siparisler");
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                MessageBox.Show("Siparişi onaylama işlemi iptal edilmiştir.");
            }
        }

        private void siparisiIadeEtButton_Click(object sender, RoutedEventArgs e)
        {
            Siparis st = DataGSiparisler.SelectedItem as Siparis;
            string id = Convert.ToString(st.Siparis_ID);

            MessageBoxResult dialogResult = MessageBox.Show(id + "' ID li siparişi iade listesine almak istediğinize emin misiniz?", "Siparişi iade al", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                siparis.siparisIadeyeAl("İade", st.Siparis_ID);

                MessageBox.Show("Sipariş başarıyla iade alınmıştır.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("Siparisler");
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                MessageBox.Show("Sipariş iade etme işlemi iptal edilmiştir.");
            }
        }
    }
}
