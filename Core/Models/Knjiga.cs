using System.Globalization;
using Core.Serialization;

namespace Core.Models
{
    public class Knjiga : ISerializable
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Naziv { get; set; } = string.Empty;
        public string Zanr { get; set; } = string.Empty;
        public int GodinaIzdanja { get; set; }
        public double Cena { get; set; }
        public int BrojStrana { get; set; }
        public int IzdavacId { get; set; }
        public Izdavac? Izdavac { get; set; }

        public List<Autor> Autori { get; set; } = new List<Autor>();
        public List<int> AutoriIds { get; set; } = new List<int>();
        public List<Posetilac> KupiliPosetioci { get; set; } = new List<Posetilac>();
        public List<Posetilac> ZeljePosetioci { get; set; } = new List<Posetilac>();

        public Knjiga() { }

        public string[] ToCSV()
        {
            string autoriStr = string.Join(",", AutoriIds);
            return new string[]
            {
                Id.ToString(),
                ISBN,
                Naziv,
                Zanr,
                GodinaIzdanja.ToString(),
                Cena.ToString(CultureInfo.InvariantCulture),
                BrojStrana.ToString(),
                IzdavacId.ToString(),
                autoriStr
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ISBN = values[1];
            Naziv = values[2];
            Zanr = values[3];
            GodinaIzdanja = int.Parse(values[4]);
            Cena = double.Parse(values[5], CultureInfo.InvariantCulture);
            BrojStrana = int.Parse(values[6]);
            IzdavacId = int.Parse(values[7]);

            AutoriIds = new List<int>();
            if (values.Length > 8 && !string.IsNullOrWhiteSpace(values[8]))
            {
                foreach (var idStr in values[8].Split(','))
                {
                    if (int.TryParse(idStr, out int aId))
                        AutoriIds.Add(aId);
                }
            }
        }

        public override string ToString()
        {
            return $"[ID: {Id}] ISBN: {ISBN} | \"{Naziv}\" | Zanr: {Zanr} | God: {GodinaIzdanja} | Cena: {Cena:F2} RSD | Strana: {BrojStrana} | IzdavacID: {IzdavacId}";
        }
    }
}