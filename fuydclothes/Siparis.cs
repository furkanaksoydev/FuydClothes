using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuydclothes
{
    internal class Siparis
    {
        int siparisid;
        int kullaniciid;
        string kullaniciad;
        string kullanicisoyad;
        string kullanicitelno;
        string kullaniciadres;
        int urunsayisi;
        decimal toplamfiyat;
        string ulastimi;
        string iade;


        public int Siparis_ID { get => siparisid; set => siparisid = value; }

        public int Kullanici_ID { get => kullaniciid; set => kullaniciid = value; }

        public string Kullanici_Ad { get => kullaniciad; set => kullaniciad = value; }

        public string Kullanici_Soyad { get => kullanicisoyad; set => kullanicisoyad = value; }

        public string Kullanici_TelNo { get => kullanicitelno; set => kullanicitelno = value; }

        public string Kullanici_Adres { get => kullaniciadres; set => kullaniciadres = value; }
        
        public int Urun_Sayisi { get => urunsayisi; set => urunsayisi = value; }

        public decimal Toplam_Fiyat { get => toplamfiyat; set => toplamfiyat = value; }

        public string Ulasti_Mi { get => ulastimi; set => ulastimi = value; }

        public string Iade { get => iade; set => iade = value; }
    }
}
