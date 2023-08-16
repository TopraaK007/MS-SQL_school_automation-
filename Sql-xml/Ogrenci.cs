    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Sql_xml
    {
        public class Ogrenci
        {
            private int numara;
            private string ad;
            private string soyad;
            private string bolum;
            private string cinsiyet;
            private DateTime date;

            public Ogrenci(int numara, string ad, string soyad, DateTime date, string bolum, string cinsiyet)
            {
                Numara = numara;
                Ad = ad;
                Soyad = soyad;
                DateTime = date;
                Bolum = bolum;
                Cinsiyet = cinsiyet;
            

            }

            public Ogrenci() { }



            public int Numara { get { return numara; } set { numara = value; } }
            public string Ad { get { return ad; } set { ad = value; } }

            public string Soyad { get { return soyad; } set { soyad = value; } }

             public DateTime DateTime { get { return date; } set { date = value; } }

            public string Bolum { get { return bolum; } set { bolum = value; } }

            public string Cinsiyet { get { return cinsiyet; } set { cinsiyet = value; } }

       


        }
    }

