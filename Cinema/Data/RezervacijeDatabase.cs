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
            try
            {
                int insertedReservationId = await Database.InsertAsync(rezervacija);
                return insertedReservationId;
            }
            catch (Exception ex)
            {
                // Handle exception
                return -1; // Return a default value or handle the exception according to your application logic
            }
        }


        public async Task<List<Rezervacije>> GetRezervaciju()
        {
            return await Database.Table<Rezervacije>().Where(r => r.Status == 0).ToListAsync();
        }

        public async Task<Rezervacije> GetRezervacijuPoId(int id)
        {
            return await Database.Table<Rezervacije>().Where(r => r.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteRezervaciju(int id)
        {
            try
            {
                // Find the reservation with the specified ID
                var reservation = await GetRezervacijuPoId(id);

                if (reservation != null)
                {
                    // Delete the reservation
                    int deletedRows = await Database.DeleteAsync(reservation);
                    return deletedRows > 0;
                }
                else
                {
                    // Reservation with the specified ID was not found
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }
        public async Task<List<Rezervacije>> GetRezervacijePoKorisnikId(int korisnikId)
        {
            try
            {
                // Find all reservations for the specified korisnikId that are active
                return await Database.Table<Rezervacije>()
                                       .Where(r => r.KorisnikId == korisnikId && r.Status == StatusRezervacije.aktivna)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }

    }
}
