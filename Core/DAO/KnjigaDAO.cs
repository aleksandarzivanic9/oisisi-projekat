using Core.Models;
using Core.Serialization;

namespace Core.DAO
{
    public class KnjigaDAO
    {
        private readonly List<Knjiga> _knjige;
        private readonly Storage<Knjiga> _storage;

        public KnjigaDAO()
        {
            _storage = new Storage<Knjiga>("knjige.csv");
            _knjige = _storage.Load();
        }

        private int GenerateId() => _knjige.Count == 0 ? 1 : _knjige.Max(k => k.Id) + 1;

        public Knjiga Add(Knjiga knjiga)
        {
            knjiga.Id = GenerateId();
            _knjige.Add(knjiga);
            _storage.Save(_knjige);
            return knjiga;
        }

        public Knjiga? Update(Knjiga knjiga)
        {
            Knjiga? stara = GetById(knjiga.Id);
            if (stara == null) return null;

            stara.ISBN = knjiga.ISBN;
            stara.Naziv = knjiga.Naziv;
            stara.Zanr = knjiga.Zanr;
            stara.GodinaIzdanja = knjiga.GodinaIzdanja;
            stara.Cena = knjiga.Cena;
            stara.BrojStrana = knjiga.BrojStrana;
            stara.IzdavacId = knjiga.IzdavacId;

            _storage.Save(_knjige);
            return stara;
        }

        public Knjiga? Remove(int id)
        {
            Knjiga? knjiga = GetById(id);
            if (knjiga == null) return null;

            _knjige.Remove(knjiga);
            _storage.Save(_knjige);
            return knjiga;
        }

        public Knjiga? GetById(int id) => _knjige.FirstOrDefault(k => k.Id == id);
        public List<Knjiga> GetAll() => _knjige;
    }
}