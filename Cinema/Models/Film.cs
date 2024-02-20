using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Cinema.Models
{
    public class Film
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string Zanr { get; set; }
        public int Trajanje { get; set; }
        public string Trailer { get; set; }
        public double Ocjena { get; set; }
    }
}
