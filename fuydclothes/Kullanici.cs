using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuydclothes
{
    internal class Kullanici
    {
        int kullaniciid;
        string ad;
        string soyad;
        string telNo;
        string adres;
        string kirmizimi;

        public int Kullanici_ID { get => kullaniciid; set => kullaniciid = value; }

        public string Kullanici_Ad { get => ad; set => ad = value; }

        public string Kullanici_Soyad { get => soyad; set => soyad = value; }

        public string Kullanici_TelNo { get => telNo; set => telNo = value; }

        public string Kullanici_Adres { get => adres; set => adres = value; }

        public string Kullanici_Kirmizimi { get => kirmizimi; set => kirmizimi = value; }
    }
}
