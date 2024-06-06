using Date;
using System;
using System.Collections.Generic;
using System.IO;

namespace ObslugaPlyt
{
    internal class Program
    {
        static List<Plyta> plyty = new List<Plyta>();

        static void Main(string[] args)
        {
            while (true)
            {

                Console.WriteLine("\n----- MENU GŁÓWNE -----");
                Console.WriteLine("1. Wciśnij '1', jeżeli chcesz dodać nową płytę");
                Console.WriteLine("2. Wciśnij '2', jeżeli chcesz wyświetlić wszystkie płyty");
                Console.WriteLine("3. Wciśnij '3', jeżeli chcesz wyświetlić szczegóły płyty");
                Console.WriteLine("4. Wciśnij '4', jeżeli chcesz wyświetlic wykonawców na płycie");
                Console.WriteLine("5. Wciśnij '5', jeżeli chcesz wyświetlić szczegóły utworu");
                Console.WriteLine("6. Wciśnij '6', jeżeli chcesz zapisać bazę danych");
                Console.WriteLine("7. Wciśnij '7', jeżeli chcesz wyjść z programu");
                Console.Write("Wybierz opcję: ");

                string opcja = Console.ReadLine();

                switch (opcja)
                {
                    case "1":
                        DodajNowaPlyte();
                        break;

                    case "2":
                        WyswietlWszystkiePlyty();
                        break;

                    case "3":
                        WyswietlSzczegolyPlyty();
                        break;

                    case "4":
                        WyswietlWykonawcowNaPlycie();
                        break;

                    case "5":
                        WyswietlSzczegolyUtworu();
                        break;

                    case "6":
                        ZapiszBazeDanych();
                        break;

                    case "7":
                        ZapiszBazeDanych();
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void DodajNowaPlyte()
        {
            do
            {
                try
                {
                    Console.WriteLine("\n----- DODAWANIE NOWEJ PŁYTY -----");

                    Console.Write("Podaj tytuł płyty: ");
                    string tytul = Console.ReadLine();

                    Console.Write("Podaj typ płyty (CD/DVD): ");
                    string typ = Console.ReadLine();
                    if (!(typ.Equals("CD", StringComparison.OrdinalIgnoreCase) || typ.Equals("DVD", StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new Exception("Niepoprawny typ płyty");
                    }

                    Console.Write("Podaj numer płyty: ");
                    int numerPlyty = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Podaj spis utworów (oddzielając utwory przecinkami): ");
                    string utworyInput = Console.ReadLine();
                    string[] utworySplit = utworyInput.Split(',');

                    List<Utwor> utwory = new List<Utwor>();

                    int numerUtworu = 1;
                    int sumaminut = 0;
                    List<string> wykonawcy = new List<string>();
                    foreach (string utworTitle in utworySplit)
                    {
                        Console.Write($"Podaj czas trwania utworu '{utworTitle}' (w minutach): ");
                        int czasMinuty = Convert.ToInt32(Console.ReadLine());

                        Console.Write($"Podaj czas trwania utworu '{utworTitle}' (w sekundach): ");
                        int czasSekundy = Convert.ToInt32(Console.ReadLine());
                        if (czasSekundy >= 60)
                        {
                            czasMinuty += czasSekundy / 60;
                            czasSekundy %= 60;
                        }
                        sumaminut += czasMinuty;

                        Console.Write($"Podaj kompozytora dla utworu '{utworTitle}': ");
                        string kompozytor = Console.ReadLine();

                        Console.Write($"Podaj wykonawcę dla utworu '{utworTitle}': ");
                        string wykonawca = Console.ReadLine();

                        Utwor utwor = new Utwor(utworTitle, czasMinuty, czasSekundy, kompozytor, numerUtworu);
                        utwor.Wykonawcy.Add(wykonawca);
                        wykonawcy.Add(wykonawca);
                        utwory.Add(utwor);
                        numerUtworu++;

                    }

                    Plyta nowaPlyta = new Plyta(tytul, typ, numerPlyty, sumaminut, new List<Utwor>(utwory), new List<string>(wykonawcy));
                    plyty.Add(nowaPlyta);
                    wykonawcy.Clear();
                    Console.WriteLine("Płyta została dodana pomyślnie!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                    break;
                }

                Console.Write("Czy chcesz dodać kolejną płytę? (T/N): ");
                string odpowiedz = Console.ReadLine().ToUpper();

                if (odpowiedz != "T")
                {
                    break;
                }
            } while (true);
        }

        static void WyswietlWszystkiePlyty()
        {
            Console.WriteLine("\n----- WSZYSTKIE PŁYTY -----");
            if (plyty.Count == 0)
            {
                Console.WriteLine("Brak płyt w bazie danych.");
                return;
            }

            Console.WriteLine("Lista płyt:");
            foreach (Plyta plyta in plyty)
            {
                Console.WriteLine($"{plyta.Tytul} (nr {plyta.NumerPlyty})");
            }
        }

        static void WyswietlSzczegolyPlyty()
        {
            Console.Write("\nPodaj numer płyty, dla której chcesz wyświetlić szczegóły: ");
            int numerPlyty = Convert.ToInt32(Console.ReadLine());

            Plyta znalezionaPlyta = plyty.Find(plyta => plyta.NumerPlyty == numerPlyty);

            if (znalezionaPlyta.Equals(null))
            {
                Console.WriteLine("Nie znaleziono płyty o podanym numerze.");
                return;
            }

            Console.WriteLine($"----- SZCZEGÓŁY PŁYTY {znalezionaPlyta.Tytul} -----");
            Console.WriteLine($"Tytuł: {znalezionaPlyta.Tytul}");
            Console.WriteLine($"Typ: {znalezionaPlyta.Typ}");
            Console.WriteLine($"Czas trwania: {znalezionaPlyta.SumaMinut} min");

            Console.WriteLine("Lista utworów:");
            foreach (var utwor in znalezionaPlyta.Utwory)
            {
                Console.WriteLine($"{utwor.NumerUtworu}. {utwor.Tytul}");
            }
        }

        static void WyswietlWykonawcowNaPlycie()
        {
            Console.Write("\nPodaj numer płyty, dla której chcesz wyświetlić wykonawców: ");
            int numerPlyty = Convert.ToInt32(Console.ReadLine());

            Plyta znalezionaPlyta = plyty.Find(plyta => plyta.NumerPlyty == numerPlyty);

            if (znalezionaPlyta.Equals(null))
            {
                Console.WriteLine("Nie znaleziono płyty o podanym numerze.");
                return;
            }


            Console.WriteLine($"----- WYKONAWCY PŁYTY {znalezionaPlyta.Tytul} -----");
            Console.WriteLine("Lista wykonawców:");
            foreach (string wykonawca in znalezionaPlyta.Wykonawcy)
            {
                Console.WriteLine($"- {wykonawca}");
            }
        }

        static void WyswietlSzczegolyUtworu()
        {
            Console.Write("\nPodaj numer płyty, dla której chcesz wyświetlić szczegóły: ");
            int numerPlyty = Convert.ToInt32(Console.ReadLine());

            Plyta znalezionaPlyta = plyty.Find(plyta => plyta.NumerPlyty == numerPlyty);

            if (znalezionaPlyta.Equals(null))
            {
                Console.WriteLine("Nie znaleziono płyty o podanym numerze.");
                return;
            }

            Console.WriteLine("Lista utworów:");
            foreach (var utwor in znalezionaPlyta.Utwory)
            {
                Console.WriteLine($"{utwor.NumerUtworu}. {utwor.Tytul}");
            }
            Console.Write("Podaj numer utworu, którego szczegóły chcesz wyświetlić: ");
            int numerUtworu = Convert.ToInt32(Console.ReadLine());

            Utwor znalezionyUtwor = znalezionaPlyta.Utwory.Find(utwor => utwor.NumerUtworu == numerUtworu);

            if (znalezionyUtwor.Equals(null))
            {
                Console.WriteLine("Nie znaleziono utworu o podanym numerze.");
                return;
            }

            Console.WriteLine($"----- SZCZEGÓŁY UTWORU {znalezionyUtwor.Tytul} NA PŁYCIE {znalezionaPlyta.Tytul} -----");
            Console.WriteLine($"Tytuł: {znalezionyUtwor.Tytul}");
            Console.WriteLine($"Czas trwania: {znalezionyUtwor.CzasTrwaniaMinuty} min {znalezionyUtwor.CzasTrwaniaSekundy} sek ");
            Console.WriteLine("Wykonawcy:");
            foreach (string wykonawca in znalezionyUtwor.Wykonawcy)
            {
                Console.WriteLine($"- {wykonawca}");
            }
            Console.WriteLine($"Kompozytor: {znalezionyUtwor.Kompozytor}");
        }

        static void ZapiszBazeDanych()
        {
            Console.Write("Czy na pewno chcesz zapisać zmiany w bazie danych do pliku? (T/N): ");
            string odpowiedz = Console.ReadLine().ToUpper();

            if (odpowiedz == "T")
            {
                using (StreamWriter sw = new StreamWriter("baza.txt"))
                {
                    foreach (var plyta in plyty)
                    {
                        sw.WriteLine("\nTytuł plyty: " + plyta.Tytul);
                        sw.WriteLine("Typ: " + plyta.Typ);
                        sw.WriteLine();

                        foreach (var utwor in plyta.Utwory)
                        {
                            sw.WriteLine($"  Tytuł utworu: {utwor.Tytul}");
                            sw.WriteLine($"  Czas trwania: {utwor.CzasTrwaniaMinuty} min {utwor.CzasTrwaniaSekundy} sek");
                            sw.WriteLine($"  Kompozytor: {utwor.Kompozytor}");
                            sw.WriteLine($"  ID: {utwor.NumerUtworu}");

                            sw.WriteLine("  Wykonawcy:");
                            foreach (var wykonawca in utwor.Wykonawcy)
                            {
                                sw.WriteLine($"  - {wykonawca}");
                            }
                            sw.WriteLine();
                        }
                    }
                }

                Console.WriteLine("Zmiany zostały zapisane do pliku baza.txt.");
            }
            else
            {
                Console.WriteLine("Zapisanie zmian zostało anulowane.");
            }
        }

    }
}



