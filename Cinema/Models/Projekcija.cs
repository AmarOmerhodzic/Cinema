using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Cinema.Models
{
    public class Projekcija
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Film))]
        public int FilmId { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public int BrojKarata { get; set; }
        public int BrojSjedista { get; set; }
        public double Cijena { get; set; } 
    }
}
