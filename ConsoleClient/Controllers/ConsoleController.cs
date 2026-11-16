using System.Globalization;
using Core.Models;
using Core.Services;

namespace ConsoleClient.Controllers
{
    public class ConsoleController
    {
        private readonly SajamService _service;

        public ConsoleController()
        {
            _service = new SajamService();
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n==========================================");
                Console.WriteLine("   INFORMACIONI SISTEM SAJMA KNJIGA");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Posetioci");
                Console.WriteLine("2. Autori");
                Console.WriteLine("3. Knjige");
                Console.WriteLine("4. Kupovine");
                Console.WriteLine("5. Izdavaci");
                Console.WriteLine("6. Adrese");
                Console.WriteLine("0. Izlaz");
                Console.Write("Izaberite entitet: ");

                string? izbor = Console.ReadLine();
                switch (izbor)
                {
                    case "1": MeniPosetioci(); break;
                    case "2": MeniAutori(); break;
                    case "3": MeniKnjige(); break;
                    case "4": MeniKupovine(); break;
                    case "5": MeniIzdavaci(); break;
                    case "6": MeniAdrese(); break;
                    case "0": return;
                    default: Console.WriteLine("Nepoznata opcija!"); break;
                }
            }
        }

        private string IzaberiAkciju(string nazivEntiteta)
        {
            Console.WriteLine($"\n--- {nazivEntiteta.ToUpper()} ---");
            Console.WriteLine("1. Prikazi sve");
            Console.WriteLine("2. Dodaj novi");
            Console.WriteLine("3. Izmeni postojeci");
            Console.WriteLine("4. Obrisi");
            Console.WriteLine("0. Nazad");
            Console.Write("Izaberite opciju: ");
            return Console.ReadLine() ?? "";
        }

        // ================= 1. POSETIOCI =================
        private void MeniPosetioci()
        {
            while (true)
            {
                switch (IzaberiAkciju("Posetioci"))
                {
                    case "1":
                        var lista = _service.Posetioci.GetAll();
                        if (lista.Count == 0) Console.WriteLine("Nema unetih posetilaca.");
                        lista.ForEach(Console.WriteLine);
                        break;
                    case "2":
                        Posetilac p = UnosPodatakaPosetioca();
                        _service.Posetioci.Add(p);
                        Console.WriteLine("Posetilac uspesno dodat i sacuvan!");
                        break;
                    case "3":
                        int idIzmena = UcitajInt("Unesite ID posetioca za izmenu: ");
                        var postojeci = _service.Posetioci.GetById(idIzmena);
                        if (postojeci == null) { Console.WriteLine("Posetilac nije pronadjen."); break; }
                        Posetilac izmenjen = UnosPodatakaPosetioca();
                        izmenjen.Id = idIzmena;
                        _service.Posetioci.Update(izmenjen);
                        Console.WriteLine("Posetilac uspesno izmenjen!");
                        break;
                    case "4":
                        int idBrisanje = UcitajInt("Unesite ID posetioca za brisanje: ");
                        Console.WriteLine(_service.Posetioci.Remove(idBrisanje) != null ? "Posetilac obrisan!" : "Nije pronadjen.");
                        break;
                    case "0": return;
                }
            }
        }

        private Posetilac UnosPodatakaPosetioca()
        {
            Console.Write("Ime: "); string ime = Console.ReadLine() ?? "";
            Console.Write("Prezime: "); string prezime = Console.ReadLine() ?? "";
            DateTime datum = UcitajDatum("Datum rodjenja (dd.MM.yyyy): ");
            int adresaId = UcitajInt("ID Adrese: ");
            Console.Write("Kontakt telefon: "); string tel = Console.ReadLine() ?? "";
            Console.Write("Email: "); string email = Console.ReadLine() ?? "";
            Console.Write("Broj clanske karte (npr. SK-1-2023): "); string karta = Console.ReadLine() ?? "";
            int godClanstva = UcitajInt("Trenutna godina clanstva: ");
            Console.Write("Status (R - Redovan, V - VIP): ");
            string st = (Console.ReadLine() ?? "R").Trim().ToUpper();
            StatusPosetioca status = st == "V" ? StatusPosetioca.VIP : StatusPosetioca.Redovan;
            double ocena = UcitajDouble("Prosecna ocena recenzija: ");

            return new Posetilac
            {
                Ime = ime,
                Prezime = prezime,
                DatumRodjenja = datum,
                AdresaId = adresaId,
                KontaktTelefon = tel,
                Email = email,
                BrojClanskeKarte = karta,
                TrenutnaGodinaClanstva = godClanstva,
                Status = status,
                ProsecnaOcenaRecenzija = ocena
            };
        }

        // ================= 2. AUTORI =================
        private void MeniAutori()
        {
            while (true)
            {
                switch (IzaberiAkciju("Autori"))
                {
                    case "1":
                        var lista = _service.Autori.GetAll();
                        if (lista.Count == 0) Console.WriteLine("Nema unetih autora.");
                        lista.ForEach(Console.WriteLine);
                        break;
                    case "2":
                        _service.Autori.Add(UnosPodatakaAutora());
                        Console.WriteLine("Autor uspesno dodat!");
                        break;
                    case "3":
                        int id = UcitajInt("Unesite ID autora za izmenu: ");
                        if (_service.Autori.GetById(id) == null) { Console.WriteLine("Autor nije pronadjen."); break; }
                        Autor izmenjen = UnosPodatakaAutora();
                        izmenjen.Id = id;
                        _service.Autori.Update(izmenjen);
                        Console.WriteLine("Autor uspesno izmenjen!");
                        break;
                    case "4":
                        int idDel = UcitajInt("Unesite ID autora za brisanje: ");
                        Console.WriteLine(_service.Autori.Remove(idDel) != null ? "Autor obrisan!" : "Nije pronadjen.");
                        break;
                    case "0": return;
                }
            }
        }

        private Autor UnosPodatakaAutora()
        {
            Console.Write("Ime: "); string ime = Console.ReadLine() ?? "";
            Console.Write("Prezime: "); string prezime = Console.ReadLine() ?? "";
            DateTime datum = UcitajDatum("Datum rodjenja (dd.MM.yyyy): ");
            int adresaId = UcitajInt("ID Adrese: ");
            Console.Write("Telefon: "); string tel = Console.ReadLine() ?? "";
            Console.Write("Email: "); string email = Console.ReadLine() ?? "";
            Console.Write("Broj licne karte: "); string lk = Console.ReadLine() ?? "";
            int iskustvo = UcitajInt("Godine iskustva: ");

            return new Autor
            {
                Ime = ime,
                Prezime = prezime,
                DatumRodjenja = datum,
                AdresaId = adresaId,
                Telefon = tel,
                Email = email,
                BrojLicneKarte = lk,
                GodineIskustva = iskustvo
            };
        }

        // ================= 3. KNJIGE =================
        private void MeniKnjige()
        {
            while (true)
            {
                switch (IzaberiAkciju("Knjige"))
                {
                    case "1":
                        var lista = _service.Knjige.GetAll();
                        if (lista.Count == 0) Console.WriteLine("Nema unetih knjiga.");
                        lista.ForEach(Console.WriteLine);
                        break;
                    case "2":
                        _service.Knjige.Add(UnosPodatakaKnjige());
                        Console.WriteLine("Knjiga uspesno dodata!");
                        break;
                    case "3":
                        int id = UcitajInt("Unesite ID knjige za izmenu: ");
                        if (_service.Knjige.GetById(id) == null) { Console.WriteLine("Knjiga nije pronadjena."); break; }
                        Knjiga izmenjena = UnosPodatakaKnjige();
                        izmenjena.Id = id;
                        _service.Knjige.Update(izmenjena);
                        Console.WriteLine("Knjiga uspesno izmenjena!");
                        break;
                    case "4":
                        int idDel = UcitajInt("Unesite ID knjige za brisanje: ");
                        Console.WriteLine(_service.Knjige.Remove(idDel) != null ? "Knjiga obrisana!" : "Nije pronadjena.");
                        break;
                    case "0": return;
                }
            }
        }

        private Knjiga UnosPodatakaKnjige()
        {
            Console.Write("ISBN: "); string isbn = Console.ReadLine() ?? "";
            Console.Write("Naziv: "); string naziv = Console.ReadLine() ?? "";
            Console.Write("Zanr: "); string zanr = Console.ReadLine() ?? "";
            int godina = UcitajInt("Godina izdanja: ");
            double cena = UcitajDouble("Cena: ");
            int strane = UcitajInt("Broj strana: ");
            int izdavacId = UcitajInt("ID Izdavaca: ");

            return new Knjiga
            {
                ISBN = isbn,
                Naziv = naziv,
                Zanr = zanr,
                GodinaIzdanja = godina,
                Cena = cena,
                BrojStrana = strane,
                IzdavacId = izdavacId
            };
        }

        // ================= 4. KUPOVINE =================
        private void MeniKupovine()
        {
            while (true)
            {
                switch (IzaberiAkciju("Kupovine"))
                {
                    case "1":
                        var lista = _service.Kupovine.GetAll();
                        if (lista.Count == 0) Console.WriteLine("Nema unetih kupovina.");
                        lista.ForEach(Console.WriteLine);
                        break;
                    case "2":
                        _service.Kupovine.Add(UnosPodatakaKupovine());
                        Console.WriteLine("Kupovina uspesno dodata!");
                        break;
                    case "3":
                        int id = UcitajInt("Unesite ID kupovine za izmenu: ");
                        if (_service.Kupovine.GetById(id) == null) { Console.WriteLine("Kupovina nije pronadjena."); break; }
                        Kupovina izmenjena = UnosPodatakaKupovine();
                        izmenjena.Id = id;
                        _service.Kupovine.Update(izmenjena);
                        Console.WriteLine("Kupovina uspesno izmenjena!");
                        break;
                    case "4":
                        int idDel = UcitajInt("Unesite ID kupovine za brisanje: ");
                        Console.WriteLine(_service.Kupovine.Remove(idDel) != null ? "Kupovina obrisana!" : "Nije pronadjena.");
                        break;
                    case "0": return;
                }
            }
        }

        private Kupovina UnosPodatakaKupovine()
        {
            int posetilacId = UcitajInt("ID Posetioca: ");
            int knjigaId = UcitajInt("ID Knjige: ");
            DateTime datum = UcitajDatum("Datum kupovine (dd.MM.yyyy): ");
            int ocena;
            do
            {
                ocena = UcitajInt("Ocena (1-5): ");
            } while (ocena < 1 || ocena > 5);
            Console.Write("Komentar: "); string komentar = Console.ReadLine() ?? "";

            return new Kupovina
            {
                PosetilacId = posetilacId,
                KnjigaId = knjigaId,
                DatumKupovine = datum,
                Ocena = ocena,
                Komentar = komentar
            };
        }

        // ================= 5. IZDAVACI =================
        private void MeniIzdavaci()
        {
            while (true)
            {
                switch (IzaberiAkciju("Izdavaci"))
                {
                    case "1":
                        var lista = _service.Izdavaci.GetAll();
                        if (lista.Count == 0) Console.WriteLine("Nema unetih izdavaca.");
                        lista.ForEach(Console.WriteLine);
                        break;
                    case "2":
                        _service.Izdavaci.Add(UnosPodatakaIzdavaca());
                        Console.WriteLine("Izdavac uspesno dodat!");
                        break;
                    case "3":
                        int id = UcitajInt("Unesite ID izdavaca za izmenu: ");
                        if (_service.Izdavaci.GetById(id) == null) { Console.WriteLine("Izdavac nije pronadjen."); break; }
                        Izdavac izmenjen = UnosPodatakaIzdavaca();
                        izmenjen.Id = id;
                        _service.Izdavaci.Update(izmenjen);
                        Console.WriteLine("Izdavac uspesno izmenjen!");
                        break;
                    case "4":
                        int idDel = UcitajInt("Unesite ID izdavaca za brisanje: ");
                        Console.WriteLine(_service.Izdavaci.Remove(idDel) != null ? "Izdavac obrisan!" : "Nije pronadjen.");
                        break;
                    case "0": return;
                }
            }
        }

        private Izdavac UnosPodatakaIzdavaca()
        {
            Console.Write("Sifra izdavaca: "); string sifra = Console.ReadLine() ?? "";
            Console.Write("Naziv izdavaca: "); string naziv = Console.ReadLine() ?? "";
            int sefId = UcitajInt("ID Sefa izdavaca (Autora): ");

            return new Izdavac
            {
                Sifra = sifra,
                Naziv = naziv,
                SefIzdavacaId = sefId
            };
        }

        // ================= 6. ADRESE =================
        private void MeniAdrese()
        {
            while (true)
            {
                switch (IzaberiAkciju("Adrese"))
                {
                    case "1":
                        var lista = _service.Adrese.GetAll();
                        if (lista.Count == 0) Console.WriteLine("Nema unetih adresa.");
                        lista.ForEach(Console.WriteLine);
                        break;
                    case "2":
                        _service.Adrese.Add(UnosPodatakaAdrese());
                        Console.WriteLine("Adresa uspesno dodata!");
                        break;
                    case "3":
                        int id = UcitajInt("Unesite ID adrese za izmenu: ");
                        if (_service.Adrese.GetById(id) == null) { Console.WriteLine("Adresa nije pronadjena."); break; }
                        Adresa izmenjena = UnosPodatakaAdrese();
                        izmenjena.Id = id;
                        _service.Adrese.Update(izmenjena);
                        Console.WriteLine("Adresa uspesno izmenjena!");
                        break;
                    case "4":
                        int idDel = UcitajInt("Unesite ID adrese za brisanje: ");
                        Console.WriteLine(_service.Adrese.Remove(idDel) != null ? "Adresa obrisana!" : "Nije pronadjena.");
                        break;
                    case "0": return;
                }
            }
        }

        private Adresa UnosPodatakaAdrese()
        {
            Console.Write("Ulica: "); string ulica = Console.ReadLine() ?? "";
            Console.Write("Broj: "); string broj = Console.ReadLine() ?? "";
            Console.Write("Grad: "); string grad = Console.ReadLine() ?? "";
            Console.Write("Drzava: "); string drzava = Console.ReadLine() ?? "";

            return new Adresa { Ulica = ulica, Broj = broj, Grad = grad, Drzava = drzava };
        }

        // ================= POMOCNE METODE ZA UNOS =================
        private int UcitajInt(string poruka)
        {
            while (true)
            {
                Console.Write(poruka);
                if (int.TryParse(Console.ReadLine(), out int vrednost)) return vrednost;
                Console.WriteLine("Greska: Unesite ceo broj!");
            }
        }

        private double UcitajDouble(string poruka)
        {
            while (true)
            {
                Console.Write(poruka);
                if (double.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out double vrednost)) return vrednost;
                Console.WriteLine("Greska: Unesite validan broj (npr. 1290.50)!");
            }
        }

        private DateTime UcitajDatum(string poruka)
        {
            while (true)
            {
                Console.Write(poruka);
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datum))
                    return datum;
                Console.WriteLine("Greska: Unesite datum u formatu dd.MM.yyyy (npr. 15.05.2002)!");
            }
        }
    }
}