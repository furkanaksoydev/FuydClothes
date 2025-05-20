using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuydclothes
{
    internal class SiparisUrunleri
    {
        int siparisurunleriid;
        int siparisid;
        int urunid;
        string urunad;
        string urunbeden;
        decimal urunfiyat;

        public int Siparis_Urunleri_ID { get => siparisurunleriid; set => siparisurunleriid = value; }

        public int Siparis_ID { get => siparisid; set => siparisid = value; }

        public int Urun_ID { get => urunid; set => urunid = value; }

        public string Urun_Ad { get => urunad; set => urunad = value; }

        public string Urun_Beden { get => urunbeden; set => urunbeden = value; }

        public decimal Urun_Fiyat { get => urunfiyat; set => urunfiyat = value; }
    }
}
