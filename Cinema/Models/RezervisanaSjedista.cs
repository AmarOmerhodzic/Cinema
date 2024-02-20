using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class RezervisanaSjedista
    {
        public int Id { get; set; }
        public int RezervacijaId { get; set; }
        public int ProjekcijaId { get; set; }

    }
}
