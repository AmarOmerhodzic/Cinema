using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Models;
using SQLite;

namespace Cinema.Data
{
    public class KorisnikDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<KorisnikDatabase> Instance =
            new AsyncLazy<KorisnikDatabase>(async () =>
            {
                var instance = new KorisnikDatabase();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<Korisnik>();
                }
                catch (Exception ex)
                {

                    throw;
                }
                return instance;
            });


        public KorisnikDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
        public async Task<bool> RegistrirajKorisnika(Korisnik korisnik)
        {
            try
            {
                int insertedRows = await Database.InsertAsync(korisnik);
                return insertedRows > 0;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Prijava korisnika
        public async Task<Korisnik> PrijaviKorisnika(string korisnickoIme, string lozinka)
        {
            try
            {
                // Pronađi korisnika s odgovarajućim korisničkim imenom i lozinkom
                var korisnik = await Database.Table<Korisnik>().Where(x => x.KorisnickoIme == korisnickoIme && x.Lozinka == lozinka).FirstOrDefaultAsync();
                return korisnik;
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }
    }
}
