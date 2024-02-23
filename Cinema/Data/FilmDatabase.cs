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
                // Check if the film object is null
                if (film == null)
                {
                    throw new ArgumentNullException(nameof(film), "Film object is null.");
                }

                // Insert the film object into the database
                int insertedRows = await Database.InsertAsync(film);

                // Check if any rows were inserted
                if (insertedRows > 0)
                {
                    return true; // Data saved successfully
                }
                else
                {
                    // No rows inserted, log a warning
                    Console.WriteLine("Warning: No rows inserted when saving film data.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in CreateFilm method: {ex.Message}");
                return false; // Data failed to save
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
        public async Task<int> UnosTest()
        {
            return await Database.ExecuteAsync("INSERT INTO Film (Naziv, Opis, Zanr, Trajanje, Trailer, Ocjena, Slika) VALUES (?, ?, ?, ?, ?, ?, ?);",
                "Black Panther",
                "Black Panther is a 2018 American superhero film based on the Marvel Comics character of the same name. Produced by Marvel Studios and distributed by Walt ...",
                "Action",
                120,
                "https://www.youtube.com/embed/xjDjIWPwcPU?si=7DwhtSoQY6nu5qR_",
                8.5,
                "https://www.washingtonpost.com/graphics/2019/entertainment/oscar-nominees-movie-poster-design/img/black-panther-web.jpg");
        }
        // Dohvati filmove po žanru
        public async Task<List<Film>> GetFilmoviPoZanru(string zanr)
        {
            try
            {
                return await Database.Table<Film>().Where(f => f.Zanr == zanr).ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }


    }
}
