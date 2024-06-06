using System;
using System.Collections.Generic;

namespace Date
{
    public struct Plyta
    {
        public string Tytul;
        public string Typ;
        public List<Utwor> Utwory;
        public List<string> Wykonawcy;
        public int NumerPlyty;
        public int SumaMinut;

        public Plyta(string tytul, string typ, int numerPlyty, int sumaminut, List<Utwor> utwory, List<string> wykonawcy)
        {
            Tytul = tytul;
            Typ = typ;
            NumerPlyty = numerPlyty;
            Utwory = utwory;
            Wykonawcy = wykonawcy;
            SumaMinut = sumaminut;
        }
    }
    public struct Utwor
    {
        public string Tytul;
        public int CzasTrwaniaMinuty;
        public int CzasTrwaniaSekundy;
        public List<string> Wykonawcy;
        public string Kompozytor;
        public int NumerUtworu;
        public string Wykonawca;

        public Utwor(string tytul, int czasTrwaniaMinuty, int czasTrwaniaSekundy, string kompozytor, int numerUtworu)
        {
            Tytul = tytul;
            CzasTrwaniaMinuty = czasTrwaniaMinuty;
            CzasTrwaniaSekundy = czasTrwaniaSekundy;
            Kompozytor = kompozytor;
            NumerUtworu = numerUtworu;
            Wykonawcy = new List<string>();
        }
    }

}
