using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Cinema.Models
{
    public class Korisnik
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public double StanjeNaRacunu { get; set; }
        public Uloga Uloga { get; set; }
    }
}
