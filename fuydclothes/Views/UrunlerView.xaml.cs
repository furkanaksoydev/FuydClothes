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
    /// UrunlerView.xaml etkileşim mantığı
    /// </summary>
    public partial class UrunlerView : UserControl
    {
        UrunClass urun = new UrunClass();

        public UrunlerView()
        {
            InitializeComponent();

            string aktifmi = "Aktif";

            DataGUrunler.ItemsSource = urun.FillAktifUrunFiltre(aktifmi);

            kategoriCmbBox.ItemsSource = urun.FillKategoriCombobox();
        }

        private void urunVerileriniGuncelleButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGUrunler.SelectedItem is Urun st)
            {
                int urunid = st.Urun_ID;
                string urunad = st.Urun_Ad;
                string urunmarka = st.Urun_Marka;
                string urunkategori = st.Urun_Kategori;
                string urunrenk = st.Urun_Renk;
                int urunsbedenstok = st.Urun_S_Beden_Adet;
                int urunmbedenstok = st.Urun_M_Beden_Adet;
                int urunlbedenstok = st.Urun_L_Beden_Adet;
                int urunxlbedenstok = st.Urun_XL_Beden_Adet;
                int urunxxlbedenstok = st.Urun_XXL_Beden_Adet;
                decimal urunfiyat = st.Urun_Fiyat;
                string urunpasifmi = st.Urun_PasifMi;

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage2("UrunGuncelle", urunid, urunad, urunmarka, urunkategori, urunrenk, urunsbedenstok, urunmbedenstok, urunlbedenstok, urunxlbedenstok, urunxxlbedenstok, urunfiyat, urunpasifmi);
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce bir ürün seçiniz.");
            }
        }

        private void DataGUrunler_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Urun st = DataGUrunler.SelectedItem as Urun;
            string urunid = Convert.ToString(st.Urun_ID.ToString());

            seciliUrunID.Text = urunid;
        }

        private void urunuPasifeAlButton_Click(object sender, RoutedEventArgs e)
        {
            Urun st = DataGUrunler.SelectedItem as Urun;
            string id = Convert.ToString(st.Urun_ID);

            MessageBoxResult dialogResult = MessageBox.Show(id + "' ID li ürünü pasif ürünler listesine almak istediğinize emin misiniz?", "Ürünü pasife al", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                urun.urunuPasiftenCikar("Pasif", st.Urun_ID);

                MessageBox.Show("Ürün başarıyla pasife alınmıştır.");

                if (Application.Current.MainWindow is MainWindow mainWin)
                {
                    mainWin.LoadPage("Urunler");
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                MessageBox.Show("Ürünü pasife alma işlemi iptal edilmiştir.");
            }
        }

        private void araButton_Click(object sender, RoutedEventArgs e)
        {
            if (kategoriCmbBox.Text != "")
            {
                string kategori = kategoriCmbBox.Text;

                DataGUrunler.ItemsSource = urun.FillDataGKategoriyeGore(kategori);
            }
        }

        private void aramayiTemizleButton_Click(object sender, RoutedEventArgs e)
        {
            if (kategoriCmbBox.Text != "")
            {
                DataGUrunler.ItemsSource = urun.urunler;
                kategoriCmbBox.Text = "";

                string aktifmi = "Aktif";

                DataGUrunler.ItemsSource = urun.FillAktifUrunFiltre(aktifmi);
            }
        }

        private void urunIDAraButton_Click(object sender, RoutedEventArgs e)
        {
            if (urunIDTxtBox.Text != "")
            {
                int urunid = Convert.ToInt32(urunIDTxtBox.Text);

                DataGUrunler.ItemsSource = urun.FillDataGUrunIDyeGore(urunid);
            }
        }

        private void urunIDAramayiTemizleButton_Kopyala_Click(object sender, RoutedEventArgs e)
        {
            if (urunIDTxtBox.Text != "")
            {
                DataGUrunler.ItemsSource = urun.urunler;
                urunIDTxtBox.Text = "";

                string aktifmi = "Aktif";

                DataGUrunler.ItemsSource = urun.FillAktifUrunFiltre(aktifmi);
            }
        }

        private void yeniUrunKayitButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("YeniUrunOlustur");
            }
        }

        private void pasifUrunlerButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                mainWin.LoadPage("PasifUrunler");
            }
        }
    }
}
