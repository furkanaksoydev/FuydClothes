using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace fuydclothes
{
    internal class SiparisClass
    {
        SQLiteConnection connlist = new SQLiteConnection("Data Source=fuydclothes.db;Version=3;");
        public List<Siparis> siparisler { get; set; }

        public SiparisClass()
        {
            siparisler = new List<Siparis>();
            siparisleriGetir();
        }

        private void siparisleriGetir()
        {
            connlist.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Siparisler", connlist);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Siparis siparisveri = new Siparis
                {
                    Siparis_ID = Convert.ToInt32(reader[0]),
                    Kullanici_ID = Convert.ToInt32(reader[1]),
                    Kullanici_Ad = reader[2].ToString(),
                    Kullanici_Soyad = reader[3].ToString(),
                    Kullanici_TelNo = reader[4].ToString(),
                    Kullanici_Adres = reader[5].ToString(),
                    Urun_Sayisi = Convert.ToInt32(reader[6]),
                    Toplam_Fiyat = Convert.ToDecimal(reader[7]),
                    Ulasti_Mi = reader[8].ToString(),
                    Iade = reader[9].ToString()
                };

                siparisler.Add(siparisveri);
            }

            reader.Close();
            connlist.Close();
        }

        public List<Siparis> FillSiparisIademi(string iademi)
        {
            return siparisler.Where(x => x.Iade == iademi).ToList();
        }

        public List<Siparis> FillDataGIDyeGore(int siparisid)
        {
            return siparisler.Where(x => x.Siparis_ID == siparisid && x.Iade == "Hayır").ToList();
        }

        public List<Siparis> FillIadeDataGIDyeGore(int siparisid)
        {
            return siparisler.Where(x => x.Siparis_ID == siparisid && x.Iade == "İade").ToList();
        }

        public string GetSiparisIDyeGoreTelNo(int id)
        {
            return siparisler.FirstOrDefault(x => x.Siparis_ID == id)?.Kullanici_TelNo;
        }

        public void siparisIadeyeAl(string gelensiparisiademi, int gelensiparisid)
        {
            connlist.Open();
            string query = "UPDATE Siparisler SET Iade=@p1 WHERE Siparis_ID=@p2";
            SQLiteCommand cmd = new SQLiteCommand(query, connlist);
            cmd.Parameters.AddWithValue("@p1", gelensiparisiademi);
            cmd.Parameters.AddWithValue("@p2", gelensiparisid);
            cmd.ExecuteNonQuery();
            connlist.Close();
        }

        public void siparisOnayla(string gelensiparisulastimi, int gelensiparisid)
        {
            connlist.Open();
            string query = "UPDATE Siparisler SET Ulasti_Mi=@p1 WHERE Siparis_ID=@p2";
            SQLiteCommand cmd = new SQLiteCommand(query, connlist);
            cmd.Parameters.AddWithValue("@p1", gelensiparisulastimi);
            cmd.Parameters.AddWithValue("@p2", gelensiparisid);
            cmd.ExecuteNonQuery();
            connlist.Close();
        }

        public int siparisEkle(int sipariskullaniciid, string sipariskullaniciad, string sipariskullanicisoyad, string sipariskullanicitelno, string sipariskullaniciadres, int siparisurunsayisi, decimal siparistoplamfiyat, string siparisulastimi, string siparisiade)
        {
            int yeniSiparisID = 0;

            connlist.Open();

            string query = @"INSERT INTO Siparisler 
                             (Kullanici_ID, Kullanici_Ad, Kullanici_Soyad, Kullanici_TelNo, Kullanici_Adres, Urun_Sayisi, Toplam_Fiyat, Ulasti_Mi, Iade) VALUES (@Kullanici_ID, @Kullanici_Ad, @Kullanici_Soyad, @Kullanici_TelNo, @Kullanici_Adres, @Urun_Sayisi, @Toplam_Fiyat, @Ulasti_Mi, @Iade);SELECT last_insert_rowid();";

            SQLiteCommand cmd = new SQLiteCommand(query, connlist);
            cmd.Parameters.AddWithValue("@Kullanici_ID", sipariskullaniciid);
            cmd.Parameters.AddWithValue("@Kullanici_Ad", sipariskullaniciad);
            cmd.Parameters.AddWithValue("@Kullanici_Soyad", sipariskullanicisoyad);
            cmd.Parameters.AddWithValue("@Kullanici_TelNo", sipariskullanicitelno);
            cmd.Parameters.AddWithValue("@Kullanici_Adres", sipariskullaniciadres);
            cmd.Parameters.AddWithValue("@Urun_Sayisi", siparisurunsayisi);
            cmd.Parameters.AddWithValue("@Toplam_Fiyat", siparistoplamfiyat);
            cmd.Parameters.AddWithValue("@Ulasti_Mi", siparisulastimi);
            cmd.Parameters.AddWithValue("@Iade", siparisiade);

            yeniSiparisID = Convert.ToInt32(cmd.ExecuteScalar());
            connlist.Close();

            return yeniSiparisID;
        }
    }
}