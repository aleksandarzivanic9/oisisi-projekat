using Core.Models;
using Core.Serialization;

namespace Core.DAO
{
    public class IzdavacDAO
    {
        private readonly List<Izdavac> _izdavaci;
        private readonly Storage<Izdavac> _storage;

        public IzdavacDAO()
        {
            _storage = new Storage<Izdavac>("izdavaci.csv");
            _izdavaci = _storage.Load();
        }

        private int GenerateId() => _izdavaci.Count == 0 ? 1 : _izdavaci.Max(i => i.Id) + 1;

        public Izdavac Add(Izdavac izdavac)
        {
            izdavac.Id = GenerateId();
            _izdavaci.Add(izdavac);
            _storage.Save(_izdavaci);
            return izdavac;
        }

        public Izdavac? Update(Izdavac izdavac)
        {
            Izdavac? stari = GetById(izdavac.Id);
            if (stari == null) return null;

            stari.Sifra = izdavac.Sifra;
            stari.Naziv = izdavac.Naziv;
            stari.SefIzdavacaId = izdavac.SefIzdavacaId;

            _storage.Save(_izdavaci);
            return stari;
        }

        public Izdavac? Remove(int id)
        {
            Izdavac? izdavac = GetById(id);
            if (izdavac == null) return null;

            _izdavaci.Remove(izdavac);
            _storage.Save(_izdavaci);
            return izdavac;
        }

        public Izdavac? GetById(int id) => _izdavaci.FirstOrDefault(i => i.Id == id);
        public List<Izdavac> GetAll() => _izdavaci;
    }
}