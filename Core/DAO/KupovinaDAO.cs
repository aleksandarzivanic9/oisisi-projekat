using Core.Models;
using Core.Serialization;

namespace Core.DAO
{
    public class KupovinaDAO
    {
        private readonly List<Kupovina> _kupovine;
        private readonly Storage<Kupovina> _storage;

        public KupovinaDAO()
        {
            _storage = new Storage<Kupovina>("kupovine.csv");
            _kupovine = _storage.Load();
        }

        private int GenerateId() => _kupovine.Count == 0 ? 1 : _kupovine.Max(k => k.Id) + 1;

        public Kupovina Add(Kupovina kupovina)
        {
            kupovina.Id = GenerateId();
            _kupovine.Add(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        public Kupovina? Update(Kupovina kupovina)
        {
            Kupovina? stara = GetById(kupovina.Id);
            if (stara == null) return null;

            stara.PosetilacId = kupovina.PosetilacId;
            stara.KnjigaId = kupovina.KnjigaId;
            stara.DatumKupovine = kupovina.DatumKupovine;
            stara.Ocena = kupovina.Ocena;
            stara.Komentar = kupovina.Komentar;

            _storage.Save(_kupovine);
            return stara;
        }

        public Kupovina? Remove(int id)
        {
            Kupovina? kupovina = GetById(id);
            if (kupovina == null) return null;

            _kupovine.Remove(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        public Kupovina? GetById(int id) => _kupovine.FirstOrDefault(k => k.Id == id);
        public List<Kupovina> GetAll() => _kupovine;
    }
}