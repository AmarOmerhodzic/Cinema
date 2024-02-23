using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class ProfilRezervacijeModel
    {
        public int RezervacijaId { get; set; }
        public string Sjediste { get; set; }
        public DateTime DatumVrijeme { get; set; }
    }
}
