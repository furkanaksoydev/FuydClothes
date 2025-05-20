using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Data.SQLite;

namespace fuydclothes
{
    internal class SiparisUrunleriClass
    {
        SQLiteConnection connlist = new SQLiteConnection("Data Source=fuydclothes.db;Version=3;");
        public List<SiparisUrunleri> siparisurunleri { get; set; }

        public SiparisUrunleriClass()
        {
            siparisurunleri = new List<SiparisUrunleri>();
            siparisUrunleriniGetir();
        }

        public List<SiparisUrunleri> siparisDetayListesi { get; set; } = new List<SiparisUrunleri>();

        private void siparisUrunleriniGetir()
        {
            connlist.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Siparis_Urunleri", connlist);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SiparisUrunleri siparisveri = new SiparisUrunleri
                {
                    Siparis_Urunleri_ID = Convert.ToInt32(reader[0]),
                    Siparis_ID = Convert.ToInt32(reader[1]),
                    Urun_ID = Convert.ToInt32(reader[2]),
                    Urun_Ad = reader[3].ToString(),
                    Urun_Beden = reader[4].ToString(),
                    Urun_Fiyat = Convert.ToDecimal(reader[5])
                };

                siparisurunleri.Add(siparisveri);
            }

            reader.Close();
            connlist.Close();
        }

        public void siparistekiUrunuSil(int siparisUrunleriId, int siparisId, string urunAd, string urunBeden, decimal urunFiyat)
        {
            connlist.Open();
            var deleteCmd = new SQLiteCommand("DELETE FROM Siparis_Urunleri WHERE Siparis_Urunleri_ID = @id", connlist);
            deleteCmd.Parameters.AddWithValue("@id", siparisUrunleriId);
            deleteCmd.ExecuteNonQuery();

            var updateCmd = new SQLiteCommand("UPDATE Siparisler SET Urun_Sayisi = Urun_Sayisi - 1, Toplam_Fiyat = Toplam_Fiyat - @fiyat WHERE Siparis_ID = @sid", connlist);
            updateCmd.Parameters.AddWithValue("@fiyat", urunFiyat);
            updateCmd.Parameters.AddWithValue("@sid", siparisId);
            updateCmd.ExecuteNonQuery();

            connlist.Close();
        }

        public void siparistekiUrunuGuncelle(int siparisUrunleriId, string urunAd, string urunBeden, decimal urunFiyat, int siparisId)
        {
            connlist.Open();
            var cmd = new SQLiteCommand("UPDATE Siparis_Urunleri SET Urun_Ad = @ad, Urun_Beden = @beden, Urun_Fiyat = @fiyat, Siparis_ID = @sid WHERE Siparis_Urunleri_ID = @id", connlist);
            cmd.Parameters.AddWithValue("@ad", urunAd);
            cmd.Parameters.AddWithValue("@beden", urunBeden);
            cmd.Parameters.AddWithValue("@fiyat", urunFiyat);
            cmd.Parameters.AddWithValue("@sid", siparisId);
            cmd.Parameters.AddWithValue("@id", siparisUrunleriId);
            cmd.ExecuteNonQuery();
            connlist.Close();
        }

        public void sipariseUrunEkle(int siparisID, List<SiparisUrunleri> urunler)
        {
            connlist.Open();
            foreach (var urun in urunler)
            {
                var cmd = new SQLiteCommand("INSERT INTO Siparis_Urunleri (Siparis_ID, Urun_ID, Urun_Ad, Urun_Beden, Urun_Fiyat) VALUES (@siparisid, @urunid, @urunad, @urunbeden, @urunfiyat)", connlist);
                cmd.Parameters.AddWithValue("@siparisid", siparisID);
                cmd.Parameters.AddWithValue("@urunid",urun.Urun_ID);
                cmd.Parameters.AddWithValue("@urunad", urun.Urun_Ad);
                cmd.Parameters.AddWithValue("@urunbeden", urun.Urun_Beden);
                cmd.Parameters.AddWithValue("@urunfiyat", urun.Urun_Fiyat);
                cmd.ExecuteNonQuery();
            }
            connlist.Close();
        }

        public void sipariseTekUrunEkle(int siparisID, int urunID, string urunAd, string urunBeden, decimal urunFiyat)
        {
            connlist.Open();

            var cmd = new SQLiteCommand("INSERT INTO Siparis_Urunleri (Siparis_ID, Urun_ID, Urun_Ad, Urun_Beden, Urun_Fiyat) VALUES (@siparisid, @urunid, @urunad, @urunbeden, @urunfiyat)", connlist);
            cmd.Parameters.AddWithValue("@siparisid", siparisID);
            cmd.Parameters.AddWithValue("@urunid", urunID);
            cmd.Parameters.AddWithValue("@urunad", urunAd);
            cmd.Parameters.AddWithValue("@urunbeden", urunBeden);
            cmd.Parameters.AddWithValue("@urunfiyat", urunFiyat);
            cmd.ExecuteNonQuery();

            var updateCmd = new SQLiteCommand("UPDATE Siparisler SET Toplam_Fiyat = Toplam_Fiyat + @fiyat, Urun_Sayisi = Urun_Sayisi + 1 WHERE Siparis_ID = @sid", connlist);
            updateCmd.Parameters.AddWithValue("@fiyat", urunFiyat);
            updateCmd.Parameters.AddWithValue("@sid", siparisID);
            updateCmd.ExecuteNonQuery();

            connlist.Close();
        }

        public List<SiparisUrunleri> FillSiparisUrunleri(int siparisid)
        {
            return siparisurunleri.Where(x => x.Siparis_ID == siparisid).ToList();
        }
    }
}
