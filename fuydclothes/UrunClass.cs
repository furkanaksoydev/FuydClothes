using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace fuydclothes
{
    internal class UrunClass
    {
        SQLiteConnection connlist = new SQLiteConnection("Data Source=fuydclothes.db;Version=3;");
        public List<Urun> urunler { get; set; }
        public List<SiparisUrunleri> siparisUrunleri { get; set; } // Ürün güncellediğimde siparişlerimdeki ürünleriminde güncellenmesi adına bu classı kullanmak için ekledim.
        public List<Siparis> siparisler { get; set; } // Ürün güncellediğimde siparişler tablomdaki toplam fiyatımında güncellenmesi adına bu classı kullanmak için ekledim.

        public UrunClass()
        {
            urunler = new List<Urun>(); // ürünlerimi bir liste içerisinde topladım.
            urunleriGetir();
        }

        public bool urunuPasiftenCikar(string gelenAktiflik, int gelenUrunID)
        {
            connlist.Open();
            using (var cmd = new SQLiteCommand("UPDATE Urunler SET Urun_PasifMi = @p1 WHERE Urun_ID = @p2", connlist))
            {
                cmd.Parameters.AddWithValue("@p1", gelenAktiflik);
                cmd.Parameters.AddWithValue("@p2", gelenUrunID);

                int affectedRows = cmd.ExecuteNonQuery();

                connlist.Close();

                return affectedRows > 0;
            }
        }

        private void urunleriGetir()
        {
            connlist.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Urunler", connlist);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Urun urunveri = new Urun
                {
                    Urun_ID = Convert.ToInt32(reader[0]),
                    Urun_Ad = reader[1].ToString(),
                    Urun_Marka = reader[2].ToString(),
                    Urun_Kategori = reader[3].ToString(),
                    Urun_Renk = reader[4].ToString(),
                    Urun_S_Beden_Adet = Convert.ToInt32(reader[5]),
                    Urun_M_Beden_Adet = Convert.ToInt32(reader[6]),
                    Urun_L_Beden_Adet = Convert.ToInt32(reader[7]),
                    Urun_XL_Beden_Adet = Convert.ToInt32(reader[8]),
                    Urun_XXL_Beden_Adet = Convert.ToInt32(reader[9]),
                    Urun_Fiyat = Convert.ToDecimal(reader[10]),
                    Urun_PasifMi = reader[11].ToString()
                };

                urunler.Add(urunveri);
            }

            reader.Close();
            connlist.Close();
        }

        public decimal UrunAdaGoreUrunFiyatiniGetir(string urunAd)
        {
            var urun = urunler.FirstOrDefault(x => x.Urun_Ad == urunAd);
            return urun?.Urun_Fiyat ?? 0;
        }

        public int UrunIDGetir(string urunAd)
        {
            var urun = urunler.FirstOrDefault(u => u.Urun_Ad == urunAd);
            return urun != null ? urun.Urun_ID : -1; // Ürün bulunamazsa -1 döner
        }

        public List<Urun> FillAktifUrunFiltre(string aktifmi) =>
            urunler.Where(x => x.Urun_PasifMi == aktifmi).ToList();

        public List<string> FillSipariseUrunEklemekIcinUrunAdlariCmbBox() =>
            urunler.Select(x => x.Urun_Ad).Distinct().ToList();

        public List<string> FillKategoriCombobox() =>
            urunler.Select(x => x.Urun_Kategori).Distinct().ToList();

        public List<Urun> FillDataGKategoriyeGore(string kategori) =>
            urunler.Where(x => x.Urun_Kategori == kategori && x.Urun_PasifMi == "Aktif").ToList();

        public List<Urun> FillDataGUrunIDyeGore(int urunid) =>
            urunler.Where(x => x.Urun_ID == urunid && x.Urun_PasifMi == "Aktif").ToList();

        public bool urunAdiVarMi(string urunAdi)
        {
            connlist.Open();
            var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Urunler WHERE Urun_Ad = @ad", connlist);
            cmd.Parameters.AddWithValue("@ad", urunAdi);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            connlist.Close();
            return count > 0;
        }

        public void urunGuncelleVeSiparisleriDuzelt(string ad, string marka, string kategori, string renk,int s, int m, int l, int xl, int xxl, decimal fiyat, string pasifmi, int id) // Burada bir ürün güncellendiğinde o ürünün ilgili olduğu sipariş içerisindeki durumu, buna bağlı olarak fiyatta bir değişiklik yapıldıysa o ürünün içinde bulunduğu bütün siparişlerimde toplam fiyatı güncellemem gerektiği için burada 3 tabloma ayrı ayrı işlem gönderiyorum ki veri organizasyonu düzgün bir biçimde sağlansın.
        {
            connlist.Open();

            var cmd = new SQLiteCommand(@"UPDATE Urunler SET Urun_Ad = @ad, Urun_Marka = @marka, Urun_Kategori = @kategori, Urun_Renk = @renk, Urun_S_Beden_Adet = @s, Urun_M_Beden_Adet = @m, Urun_L_Beden_Adet = @l, Urun_XL_Beden_Adet = @xl, Urun_XXL_Beden_Adet = @xxl, Urun_Fiyat = @fiyat, Urun_PasifMi = @pasif WHERE Urun_ID = @id", connlist);

            cmd.Parameters.AddWithValue("@ad", ad);
            cmd.Parameters.AddWithValue("@marka", marka);
            cmd.Parameters.AddWithValue("@kategori", kategori);
            cmd.Parameters.AddWithValue("@renk", renk);
            cmd.Parameters.AddWithValue("@s", s);
            cmd.Parameters.AddWithValue("@m", m);
            cmd.Parameters.AddWithValue("@l", l);
            cmd.Parameters.AddWithValue("@xl", xl);
            cmd.Parameters.AddWithValue("@xxl", xxl);
            cmd.Parameters.AddWithValue("@fiyat", fiyat);
            cmd.Parameters.AddWithValue("@pasif", pasifmi);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            var cmd2 = new SQLiteCommand("UPDATE Siparis_Urunleri SET Urun_Ad = @ad, Urun_Fiyat = @fiyat WHERE Urun_ID = @id", connlist);
            cmd2.Parameters.AddWithValue("@ad", ad);
            cmd2.Parameters.AddWithValue("@fiyat", fiyat);
            cmd2.Parameters.AddWithValue("@id", id);
            cmd2.ExecuteNonQuery();

            var getSiparisIDsCmd = new SQLiteCommand("SELECT DISTINCT Siparis_ID FROM Siparis_Urunleri WHERE Urun_ID = @id", connlist);
            getSiparisIDsCmd.Parameters.AddWithValue("@id", id);

            using (var reader = getSiparisIDsCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int siparisId = reader.GetInt32(0);

                    var hesaplaCmd = new SQLiteCommand(@"SELECT Urun_ID, Urun_Fiyat, COUNT(*) as Adet FROM Siparis_Urunleri WHERE Siparis_ID = @siparisId GROUP BY Urun_ID, Urun_Fiyat", connlist);
                    hesaplaCmd.Parameters.AddWithValue("@siparisId", siparisId);

                    decimal toplamFiyat = 0;
                    using (var urunReader = hesaplaCmd.ExecuteReader())
                    {
                        while (urunReader.Read())
                        {
                            decimal urunFiyat = urunReader.GetDecimal(1);
                            int adet = urunReader.GetInt32(2);
                            toplamFiyat += urunFiyat * adet;
                        }
                    }

                    var updateSiparisCmd = new SQLiteCommand("UPDATE Siparisler SET Toplam_Fiyat = @toplam WHERE Siparis_ID = @siparisId", connlist);
                    updateSiparisCmd.Parameters.AddWithValue("@toplam", toplamFiyat);
                    updateSiparisCmd.Parameters.AddWithValue("@siparisId", siparisId);
                    updateSiparisCmd.ExecuteNonQuery();
                }
            }
            connlist.Close();
        }

        public void urunEkle(string urunad, string urunmarka, string urunkategori, string urunrenk, int urunsbeden, int urunmbeden, int urunlbeden, int urunxlbeden, int urunxxlbeden, decimal urunfiyat)
        {
            using (var conn = new SQLiteConnection("Data Source=fuydclothes.db;Version=3;"))
            {
                conn.Open();

                string query = @"INSERT INTO Urunler (Urun_Ad, Urun_Marka, Urun_Kategori, Urun_Renk,Urun_S_Beden_Adet, Urun_M_Beden_Adet, Urun_L_Beden_Adet,Urun_XL_Beden_Adet, Urun_XXL_Beden_Adet, Urun_Fiyat, Urun_PasifMi) VALUES (@Urun_Ad, @Urun_Marka, @Urun_Kategori, @Urun_Renk, @Urun_S_Beden_Adet, @Urun_M_Beden_Adet, @Urun_L_Beden_Adet, @Urun_XL_Beden_Adet, @Urun_XXL_Beden_Adet, @Urun_Fiyat, @Urun_PasifMi)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Urun_Ad", urunad);
                    cmd.Parameters.AddWithValue("@Urun_Marka", urunmarka);
                    cmd.Parameters.AddWithValue("@Urun_Kategori", urunkategori);
                    cmd.Parameters.AddWithValue("@Urun_Renk", urunrenk);
                    cmd.Parameters.AddWithValue("@Urun_S_Beden_Adet", urunsbeden);
                    cmd.Parameters.AddWithValue("@Urun_M_Beden_Adet", urunmbeden);
                    cmd.Parameters.AddWithValue("@Urun_L_Beden_Adet", urunlbeden);
                    cmd.Parameters.AddWithValue("@Urun_XL_Beden_Adet", urunxlbeden);
                    cmd.Parameters.AddWithValue("@Urun_XXL_Beden_Adet", urunxxlbeden);
                    cmd.Parameters.AddWithValue("@Urun_Fiyat", urunfiyat);
                    cmd.Parameters.AddWithValue("@Urun_PasifMi", "Aktif");

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
