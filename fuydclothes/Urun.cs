using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuydclothes
{
    internal class Urun
    {

        int urunid;
        string urunad;
        string urunmarka;
        string urunkategori;
        string urunrenk;
        int urunsbedenadet;
        int urunmbedenadet;
        int urunlbedenadet;
        int urunxlbedenadet;
        int urunxxlbedenadet;
        decimal urunfiyat;
        string urunpasifmi;

        public int Urun_ID { get => urunid; set => urunid = value; }

        public string Urun_Ad { get => urunad; set => urunad = value; }

        public string Urun_Marka { get => urunmarka; set => urunmarka = value; }

        public string Urun_Kategori { get => urunkategori; set => urunkategori = value; }

        public string Urun_Renk { get => urunrenk; set => urunrenk = value; }

        public int Urun_S_Beden_Adet { get => urunsbedenadet; set => urunsbedenadet = value; }

        public int Urun_M_Beden_Adet { get => urunmbedenadet; set => urunmbedenadet = value; }

        public int Urun_L_Beden_Adet { get => urunlbedenadet; set => urunlbedenadet = value; }

        public int Urun_XL_Beden_Adet { get => urunxlbedenadet; set => urunxlbedenadet = value; }

        public int Urun_XXL_Beden_Adet { get => urunxxlbedenadet; set => urunxxlbedenadet = value; }

        public decimal Urun_Fiyat { get => urunfiyat; set => urunfiyat = value; }

        public string Urun_PasifMi { get => urunpasifmi; set => urunpasifmi = value; }
    }
}
