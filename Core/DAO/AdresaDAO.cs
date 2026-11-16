using Core.Models;
using Core.Serialization;

namespace Core.DAO
{
    public class AdresaDAO
    {
        private readonly List<Adresa> _adrese;
        private readonly Storage<Adresa> _storage;

        public AdresaDAO()
        {
            _storage = new Storage<Adresa>("adrese.csv");
            _adrese = _storage.Load();
        }

        private int GenerateId() => _adrese.Count == 0 ? 1 : _adrese.Max(a => a.Id) + 1;

        public Adresa Add(Adresa adresa)
        {
            adresa.Id = GenerateId();
            _adrese.Add(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public Adresa? Update(Adresa adresa)
        {
            Adresa? stara = GetById(adresa.Id);
            if (stara == null) return null;

            stara.Ulica = adresa.Ulica;
            stara.Broj = adresa.Broj;
            stara.Grad = adresa.Grad;
            stara.Drzava = adresa.Drzava;

            _storage.Save(_adrese);
            return stara;
        }

        public Adresa? Remove(int id)
        {
            Adresa? adresa = GetById(id);
            if (adresa == null) return null;

            _adrese.Remove(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public Adresa? GetById(int id) => _adrese.FirstOrDefault(a => a.Id == id);
        public List<Adresa> GetAll() => _adrese;
    }
}