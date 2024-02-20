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
                    await instance.InitializeAdminUser();
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
        public async Task InitializeAdminUser()
        {
            // Check if there are any admin users in the database
            var adminUser = await Database.Table<Korisnik>().FirstOrDefaultAsync(x => x.Uloga == Uloga.Admin);

            // If there is no admin user, insert one
            if (adminUser == null)
            {
                var defaultAdmin = new Korisnik
                {
                    KorisnickoIme = "admin",
                    Lozinka = "adminpass",
                    Uloga = Uloga.Admin
                };
                await Database.InsertAsync(defaultAdmin);
            }
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
