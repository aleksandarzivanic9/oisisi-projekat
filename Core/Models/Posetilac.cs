using System.Globalization;
using Core.Serialization;

namespace Core.Models
{
    public class Posetilac : ISerializable
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public DateTime DatumRodjenja { get; set; }
        public int AdresaId { get; set; }
        public Adresa? Adresa { get; set; }
        public string KontaktTelefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BrojClanskeKarte { get; set; } = string.Empty; // npr. SK-1-2023
        public int TrenutnaGodinaClanstva { get; set; }
        public StatusPosetioca Status { get; set; }
        public double ProsecnaOcenaRecenzija { get; set; }

        public List<Kupovina> KupljeneKnjige { get; set; } = new List<Kupovina>();
        public List<Knjiga> ListaZelja { get; set; } = new List<Knjiga>();
        public List<int> ListaZeljaIds { get; set; } = new List<int>();

        public Posetilac() { }

        public string[] ToCSV()
        {
            string zeljeStr = string.Join(",", ListaZeljaIds);
            return new string[]
            {
                Id.ToString(),
                Ime,
                Prezime,
                DatumRodjenja.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                AdresaId.ToString(),
                KontaktTelefon,
                Email,
                BrojClanskeKarte,
                TrenutnaGodinaClanstva.ToString(),
                Status.ToString(),
                ProsecnaOcenaRecenzija.ToString(CultureInfo.InvariantCulture),
                zeljeStr
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            DatumRodjenja = DateTime.ParseExact(values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            AdresaId = int.Parse(values[4]);
            KontaktTelefon = values[5];
            Email = values[6];
            BrojClanskeKarte = values[7];
            TrenutnaGodinaClanstva = int.Parse(values[8]);
            Status = Enum.Parse<StatusPosetioca>(values[9]);
            ProsecnaOcenaRecenzija = double.Parse(values[10], CultureInfo.InvariantCulture);

            ListaZeljaIds = new List<int>();
            if (values.Length > 11 && !string.IsNullOrWhiteSpace(values[11]))
            {
                foreach (var idStr in values[11].Split(','))
                {
                    if (int.TryParse(idStr, out int kId))
                        ListaZeljaIds.Add(kId);
                }
            }
        }

        public override string ToString()
        {
            string statusOznaka = Status == StatusPosetioca.VIP ? "V (VIP)" : "R (Redovan)";
            return $"[ID: {Id}] {BrojClanskeKarte} | {Ime} {Prezime} | Rodj: {DatumRodjenja:dd.MM.yyyy} | AdresaID: {AdresaId} | Tel: {KontaktTelefon} | Email: {Email} | God. clanstva: {TrenutnaGodinaClanstva} | Status: {statusOznaka} | Ocena: {ProsecnaOcenaRecenzija:F2}";
        }
    }
}