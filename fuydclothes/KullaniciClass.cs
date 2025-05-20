using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace fuydclothes
{
    internal class KullaniciClass
    {
        // Bağlantı dizesi SQLite için
        SQLiteConnection connlist = new SQLiteConnection("Data Source=fuydclothes.db;Version=3;");

        public List<Kullanici> kullanicilar { get; set; }

        public List<Siparis> siparisler { get; set; }

        public KullaniciClass()
        {
            kullanicilar = new List<Kullanici>();
            KullanicilariGetir();
        }

        private void KullanicilariGetir()
        {
            connlist.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Kullanici", connlist);

            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Kullanici kullanici = new Kullanici();

                kullanici.Kullanici_ID = Convert.ToInt32(reader[0]);
                kullanici.Kullanici_Ad = reader[1].ToString();
                kullanici.Kullanici_Soyad = reader[2].ToString();
                kullanici.Kullanici_TelNo = reader[3].ToString();
                kullanici.Kullanici_Adres = reader[4].ToString();
                kullanici.Kullanici_Kirmizimi = reader[5].ToString();

                kullanicilar.Add(kullanici);
            }

            reader.Close();
            connlist.Close();
        }

        public List<Kullanici> KisiKirmiziMiFiltre(string kirmizimi)
        {
            return kullanicilar.Where(x => x.Kullanici_Kirmizimi == kirmizimi).ToList();
        }
        public List<Kullanici> FillDatagTelNoyaGore(string telno)
        {
            return kullanicilar.Where(x => x.Kullanici_TelNo == telno && x.Kullanici_Kirmizimi == "Aktif").ToList();
        }

        public Kullanici KullaniciBilgileriniGetirTelIle(string tel)
        {
            return kullanicilar.FirstOrDefault(x => x.Kullanici_TelNo == tel && x.Kullanici_Kirmizimi == "Aktif");
        }

        public List<Kullanici> FillKirmiziDatagTelNoyaGore(string telno)
        {
            return kullanicilar.Where(x => x.Kullanici_TelNo == telno && x.Kullanici_Kirmizimi == "Kırmızı").ToList();
        }

        public List<string> FillSipariseKullaniciEklemekIcinAktifTelNolari()
        {
            return kullanicilar.Where(x => x.Kullanici_Kirmizimi == "Aktif").Select(x => x.Kullanici_TelNo).Distinct().ToList();
        }

        public bool telefonVarMi(string gelentelno)
        {
            connlist.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Kullanici WHERE Kullanici_TelNo = @telNo", connlist);
            cmd.Parameters.AddWithValue("@telNo", gelentelno);

            var count = Convert.ToInt32(cmd.ExecuteScalar());

            connlist.Close();

            return count > 0;
        }

        public void kullaniciGuncelleVeSiparisleriDuzelt(string gelenad, string gelensoyad, string gelentelno, string gelenadres, string gelenkirmizimi, int gelenid)
        {
            connlist.Open();

            string kayit = "UPDATE Kullanici SET Kullanici_Ad=@p1, Kullanici_Soyad=@p2, Kullanici_TelNo=@p3, Kullanici_Adres=@p4, Kullanici_Kirmizimi=@p5 WHERE Kullanici_ID=@p6";

            SQLiteCommand cmd = new SQLiteCommand(kayit, connlist);
            cmd.Parameters.AddWithValue("@p1", gelenad);
            cmd.Parameters.AddWithValue("@p2", gelensoyad);
            cmd.Parameters.AddWithValue("@p3", gelentelno);
            cmd.Parameters.AddWithValue("@p4", gelenadres);
            cmd.Parameters.AddWithValue("@p5", gelenkirmizimi);
            cmd.Parameters.AddWithValue("@p6", gelenid);

            cmd.ExecuteNonQuery();

            var cmd2 = new SQLiteCommand("UPDATE Siparisler SET Kullanici_Ad = @kullaniciad, Kullanici_Soyad = @kullanicisoyad, Kullanici_TelNo = @kullanicitelno, Kullanici_Adres = @kullaniciadres WHERE Kullanici_ID = @kullaniciid", connlist);
            cmd2.Parameters.AddWithValue("@kullaniciad", gelenad);
            cmd2.Parameters.AddWithValue("@kullanicisoyad", gelensoyad);
            cmd2.Parameters.AddWithValue("@kullanicitelno", gelentelno);
            cmd2.Parameters.AddWithValue("@kullaniciadres", gelenadres);
            cmd2.Parameters.AddWithValue("@kullaniciid", gelenid);
            cmd2.ExecuteNonQuery();

            connlist.Close();
        }

        public bool kullaniciGuncellemeTelefonNumarasiKontrolu(string gelentelno, int gelenkullaniciid)
        {
            connlist.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Kullanici WHERE Kullanici_TelNo = @telNo AND Kullanici_ID != @gelenid", connlist);
            cmd.Parameters.AddWithValue("@telNo", gelentelno);
            cmd.Parameters.AddWithValue("@gelenid", gelenkullaniciid);

            var count = Convert.ToInt32(cmd.ExecuteScalar());

            connlist.Close();

            return count > 0;
        }

        public void kullaniciEkle(string ad, string soyad, string telno, string adres)
        {
            connlist.Open();

            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Kullanici (Kullanici_Ad, Kullanici_Soyad, Kullanici_TelNo, Kullanici_Adres, Kullanici_Kirmizimi) VALUES (@Kullanici_Ad, @Kullanici_Soyad, @Kullanici_TelNo, @Kullanici_Adres, @Kullanici_Kirmizimi)", connlist);
            cmd.Parameters.AddWithValue("@Kullanici_Ad", ad);
            cmd.Parameters.AddWithValue("@Kullanici_Soyad", soyad);
            cmd.Parameters.AddWithValue("@Kullanici_TelNo", telno);
            cmd.Parameters.AddWithValue("@Kullanici_Adres", adres);
            cmd.Parameters.AddWithValue("@Kullanici_Kirmizimi", "Aktif");

            cmd.ExecuteNonQuery();

            connlist.Close();
        }

        public void kullaniciKirmizidanCikar(string gelenkullanicikirmizimi, int geleneskikullaniciid)
        {
            connlist.Open();

            string kayit = "UPDATE Kullanici SET Kullanici_Kirmizimi=@p1 WHERE Kullanici_ID=@p2";

            SQLiteCommand cmd = new SQLiteCommand(kayit, connlist);
            cmd.Parameters.AddWithValue("@p1", gelenkullanicikirmizimi);
            cmd.Parameters.AddWithValue("@p2", geleneskikullaniciid);

            cmd.ExecuteNonQuery();

            connlist.Close();
        }
    }
}