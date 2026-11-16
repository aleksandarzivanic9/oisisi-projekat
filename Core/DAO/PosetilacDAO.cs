using Core.Models;
using Core.Serialization;

namespace Core.DAO
{
    public class PosetilacDAO
    {
        private readonly List<Posetilac> _posetioci;
        private readonly Storage<Posetilac> _storage;

        public PosetilacDAO()
        {
            _storage = new Storage<Posetilac>("posetioci.csv");
            _posetioci = _storage.Load();
        }

        private int GenerateId() => _posetioci.Count == 0 ? 1 : _posetioci.Max(p => p.Id) + 1;

        public Posetilac Add(Posetilac posetilac)
        {
            posetilac.Id = GenerateId();
            _posetioci.Add(posetilac);
            _storage.Save(_posetioci);
            return posetilac;
        }

        public Posetilac? Update(Posetilac posetilac)
        {
            Posetilac? stari = GetById(posetilac.Id);
            if (stari == null) return null;

            stari.Ime = posetilac.Ime;
            stari.Prezime = posetilac.Prezime;
            stari.DatumRodjenja = posetilac.DatumRodjenja;
            stari.AdresaId = posetilac.AdresaId;
            stari.KontaktTelefon = posetilac.KontaktTelefon;
            stari.Email = posetilac.Email;
            stari.BrojClanskeKarte = posetilac.BrojClanskeKarte;
            stari.TrenutnaGodinaClanstva = posetilac.TrenutnaGodinaClanstva;
            stari.Status = posetilac.Status;
            stari.ProsecnaOcenaRecenzija = posetilac.ProsecnaOcenaRecenzija;

            _storage.Save(_posetioci);
            return stari;
        }

        public Posetilac? Remove(int id)
        {
            Posetilac? posetilac = GetById(id);
            if (posetilac == null) return null;

            _posetioci.Remove(posetilac);
            _storage.Save(_posetioci);
            return posetilac;
        }

        public Posetilac? GetById(int id) => _posetioci.FirstOrDefault(p => p.Id == id);
        public List<Posetilac> GetAll() => _posetioci;
    }
}