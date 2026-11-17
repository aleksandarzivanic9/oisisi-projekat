using Core.DAO;
using Core.Models;

namespace Core.Services
{
    public class SajamService
    {
        public AdresaDAO Adrese { get; }
        public PosetilacDAO Posetioci { get; }
        public AutorDAO Autori { get; }
        public KnjigaDAO Knjige { get; }
        public KupovinaDAO Kupovine { get; }
        public IzdavacDAO Izdavaci { get; }

        public SajamService()
        {
            Adrese = new AdresaDAO();
            Posetioci = new PosetilacDAO();
            Autori = new AutorDAO();
            Knjige = new KnjigaDAO();
            Kupovine = new KupovinaDAO();
            Izdavaci = new IzdavacDAO();
        }
    }
}