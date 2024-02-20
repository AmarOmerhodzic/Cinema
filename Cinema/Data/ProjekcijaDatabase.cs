using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Models;
using SQLite;

namespace Cinema.Data
{
    public class ProjekcijaDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<ProjekcijaDatabase> Instance =
            new AsyncLazy<ProjekcijaDatabase>(async () =>
            {
                var instance = new ProjekcijaDatabase();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<Projekcija>();
                }
                catch (Exception ex)
                {

                    throw;
                }
                return instance;
            });


        public ProjekcijaDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
        // Dohvati sve projekcije koje još nisu završile (čije su datume u budućnosti)
        public async Task<List<Projekcija>> GetSveProjekcijeKojeTraju()
        {
            try
            {
                DateTime danas = DateTime.Now;
                return await Database.Table<Projekcija>().Where(p => p.DatumVrijeme > danas).ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }

        // Dohvati sve projekcije
        public async Task<List<Projekcija>> GetSveProjekcije()
        {
            try
            {
                return await Database.Table<Projekcija>().ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }

        // Kreiraj novu projekciju
        public async Task<bool> CreateProjekciju(Projekcija projekcija)
        {
            try
            {
                int insertedRows = await Database.InsertAsync(projekcija);
                return insertedRows > 0;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Ažuriraj projekciju
        public async Task<bool> UpdateProjekciju(Projekcija projekcija)
        {
            try
            {
                int updatedRows = await Database.UpdateAsync(projekcija);
                return updatedRows > 0;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Obriši projekciju
        public async Task<bool> DeleteProjekciju(int id)
        {
            try
            {
                int deletedRows = await Database.DeleteAsync<Projekcija>(id);
                return deletedRows > 0;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

    }
}
