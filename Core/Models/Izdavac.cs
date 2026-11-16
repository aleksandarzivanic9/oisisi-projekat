using Core.Serialization;

namespace Core.Models
{
    public class Izdavac : ISerializable
    {
        public int Id { get; set; }
        public string Sifra { get; set; } = string.Empty;
        public string Naziv { get; set; } = string.Empty;
        public int SefIzdavacaId { get; set; } // Autor sa >= 5 god. iskustva
        public Autor? SefIzdavaca { get; set; }

        public List<Autor> Autori { get; set; } = new List<Autor>();
        public List<int> AutoriIds { get; set; } = new List<int>();
        public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();
        public List<int> KnjigeIds { get; set; } = new List<int>();

        public Izdavac() { }

        public string[] ToCSV()
        {
            string autoriStr = string.Join(",", AutoriIds);
            string knjigeStr = string.Join(",", KnjigeIds);
            return new string[]
            {
                Id.ToString(),
                Sifra,
                Naziv,
                SefIzdavacaId.ToString(),
                autoriStr,
                knjigeStr
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Sifra = values[1];
            Naziv = values[2];
            SefIzdavacaId = int.Parse(values[3]);

            AutoriIds = new List<int>();
            if (values.Length > 4 && !string.IsNullOrWhiteSpace(values[4]))
            {
                foreach (var idStr in values[4].Split(','))
                {
                    if (int.TryParse(idStr, out int aId))
                        AutoriIds.Add(aId);
                }
            }

            KnjigeIds = new List<int>();
            if (values.Length > 5 && !string.IsNullOrWhiteSpace(values[5]))
            {
                foreach (var idStr in values[5].Split(','))
                {
                    if (int.TryParse(idStr, out int kId))
                        KnjigeIds.Add(kId);
                }
            }
        }

        public override string ToString()
        {
            return $"[ID: {Id}] Sifra: {Sifra} | Naziv: {Naziv} | SefIzdavacaID: {SefIzdavacaId}";
        }
    }
}