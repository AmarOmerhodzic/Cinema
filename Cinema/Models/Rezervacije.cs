using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Cinema.Models
{
    public class Rezervacije
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Korisnik))]
        public int KorisnikId { get; set; }
        [ForeignKey(typeof(Projekcija))]
        public int ProjekcijaId { get; set; }
        public StatusRezervacije Status { get; set; }
        public string Sjediste { get; set; }


    }
}
