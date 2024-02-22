using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Models;
using SQLite;

namespace Cinema.Data
{
    public class FilmDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<FilmDatabase> Instance =
            new AsyncLazy<FilmDatabase>(async () =>
            {
                var instance = new FilmDatabase();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<Film>();
                }
                catch (Exception ex)
                {

                    throw;
                }
                return instance;
            });


        public FilmDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            InitializeDatabase();
        }
        private async void InitializeDatabase()
        {
            // Enable foreign key constraints
            await Database.ExecuteAsync("PRAGMA foreign_keys = ON;");
        }

        // Dohvati sve filmove
        public async Task<List<Film>> SviFilmovi()
        {
            try
            {
                return await Database.Table<Film>().ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }

        // Dohvati film po ID-u
        public async Task<Film> GetFilmPoId(int id)
        {
            try
            {
                return await Database.Table<Film>().Where(f => f.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }

        // Kreiraj novi film
        public async Task<bool> CreateFilm(Film film)
        {
            try
            {
                int insertedRows = await Database.InsertAsync(film);
                return insertedRows > 0;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Ažuriraj film
        public async Task<bool> UpdateFilm(Film film)
        {
            try
            {
                int updatedRows = await Database.UpdateAsync(film);
                return updatedRows > 0;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Obriši film
        public async Task<bool> DeleteFilm(int id)
        {
            try
            {
                int deletedRows = await Database.DeleteAsync<Film>(id);
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
