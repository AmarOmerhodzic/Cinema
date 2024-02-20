using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Models;
using SQLite;

namespace Cinema.Data
{
    public class RezervacijeDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<RezervacijeDatabase> Instance =
            new AsyncLazy<RezervacijeDatabase>(async () =>
            {
                var instance = new RezervacijeDatabase();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<Rezervacije>();
                }
                catch (Exception ex)
                {

                    throw;
                }
                return instance;
            });


        public RezervacijeDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
        public async Task<int> CreateRezervaciju(Rezervacije rezervacija)
        {
            return await Database.InsertAsync(rezervacija);
        }

        public async Task<List<Rezervacije>> GetRezervaciju()
        {
            return await Database.Table<Rezervacije>().Where(r => r.Status == 0).ToListAsync();
        }

        public async Task<Rezervacije> GetRezervacijuPoId(int id)
        {
            return await Database.Table<Rezervacije>().Where(r => r.Id == id).FirstOrDefaultAsync();
        }
    }
}
