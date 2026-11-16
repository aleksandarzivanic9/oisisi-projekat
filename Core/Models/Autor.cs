using System.Globalization;
using Core.Serialization;

namespace Core.Models
{
    public class Autor : ISerializable
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public DateTime DatumRodjenja { get; set; }
        public int AdresaId { get; set; }
        public Adresa? Adresa { get; set; }
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BrojLicneKarte { get; set; } = string.Empty;
        public int GodineIskustva { get; set; }

        public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();
        public List<int> KnjigeIds { get; set; } = new List<int>();

        public Autor() { }

        public string[] ToCSV()
        {
            string knjigeStr = string.Join(",", KnjigeIds);
            return new string[]
            {
                Id.ToString(),
                Ime,
                Prezime,
                DatumRodjenja.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                AdresaId.ToString(),
                Telefon,
                Email,
                BrojLicneKarte,
                GodineIskustva.ToString(),
                knjigeStr
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateTime.ParseExact(values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            AdresaId = int.Parse(values[4]);
            Telefon = values[5];
            Email = values[6];
            BrojLicneKarte = values[7];
            GodineIskustva = int.Parse(values[8]);

            KnjigeIds = new List<int>();
            if (values.Length > 9 && !string.IsNullOrWhiteSpace(values[9]))
            {
                foreach (var idStr in values[9].Split(','))
                {
                    if (int.TryParse(idStr, out int kId))
                        KnjigeIds.Add(kId);
                }
            }
        }

        public override string ToString()
        {
            return $"[ID: {Id}] {Ime} {Prezime} | LK: {BrojLicneKarte} | Rodj: {DatumRodjenja:dd.MM.yyyy} | AdresaID: {AdresaId} | Tel: {Telefon} | Email: {Email} | Iskustvo: {GodineIskustva} god.";
        }
    }
}