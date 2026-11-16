using System.Globalization;
using Core.Serialization;

namespace Core.Models
{
    public class Kupovina : ISerializable
    {
        public int Id { get; set; }
        public int PosetilacId { get; set; }
        public Posetilac? Posetilac { get; set; }
        public int KnjigaId { get; set; }
        public Knjiga? Knjiga { get; set; }
        public DateTime DatumKupovine { get; set; }
        public int Ocena { get; set; } // 1-5
        public string Komentar { get; set; } = string.Empty;

        public Kupovina() { }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                PosetilacId.ToString(),
                KnjigaId.ToString(),
                DatumKupovine.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Ocena.ToString(),
                Komentar
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            PosetilacId = int.Parse(values[1]);
            KnjigaId = int.Parse(values[2]);
            DatumKupovine = DateTime.ParseExact(values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Ocena = int.Parse(values[4]);
            Komentar = values.Length > 5 ? values[5] : string.Empty;
        }

        public override string ToString()
        {
            return $"[ID: {Id}] PosetilacID: {PosetilacId} | KnjigaID: {KnjigaId} | Datum: {DatumKupovine:dd.MM.yyyy} | Ocena: {Ocena}/5 | Komentar: \"{Komentar}\"";
        }
    }
}