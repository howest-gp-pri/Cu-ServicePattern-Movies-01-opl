using Cu_ServicePattern_Movies_01.Core.Data;
using Cu_ServicePattern_Movies_01.Core.Interfaces;
using Cu_ServicePattern_Movies_01.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cu_ServicePattern_Movies_01.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _movieDbContext;
        private readonly IFileService _fileService;

        public MovieService(MovieDbContext movieDbContext, IFileService fileService)
        {
            _movieDbContext = movieDbContext;
            _fileService = fileService;
        }

        public async Task<bool> CreateAsync(string title,DateTime releaseDate, 
            decimal price, int companyId, IFormFile image, IEnumerable<int> actorIds, 
            IEnumerable<int> directorIds)
        {
            //create the movie
            var movie = new Movie();
            movie.Title = title;
            movie.Price = price;
            movie.ReleaseDate = releaseDate;
            movie.CompanyId = companyId;
            //actors
            movie.Actors = await _movieDbContext
                .Actors
                .Where(m => actorIds.Contains(m.Id)).ToListAsync();
            //Directors
            movie.Directors = await _movieDbContext
                .Directors
                .Where(d => directorIds.Contains(d.Id)).ToListAsync();
            //image
            if (image != null)
            {
                movie.Image = await _fileService.Store(image);
            }
            //add to context
            _movieDbContext.Movies.Add(movie);
            //savechanges
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await GetbyIdAsync(id);
            if(movie != null)
            {
                //delete the movie image
                if (!String.IsNullOrEmpty(movie.Image))
                {
                    _fileService.Delete(movie.Image);
                }
                _movieDbContext.Movies.Remove(movie);
                //savechanges
                return await SaveChangesAsync();
            }
            return false;
        }

        public IQueryable<Movie> GetAll()
        {
            return _movieDbContext.Movies.AsQueryable();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _movieDbContext.Movies
                .Include(m => m.Company)
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .ToListAsync();
        }

        public async Task<Movie> GetbyIdAsync(int id)
        {
            return await _movieDbContext.Movies
               .Include(m => m.Company)
               .Include(m => m.Actors)
               .Include(m => m.Directors)
               .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _movieDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                Console.WriteLine(dbUpdateException.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id,DateTime releasedate, string title, decimal price, int companyId, IFormFile image, IEnumerable<int> actorIds, IEnumerable<int> directorIds)
        {
            //update
            var movie = await GetbyIdAsync(id);
            if (movie == null)
            {
                return false;
            }
            //edit the properties
            movie.Title = title;
            movie.ReleaseDate = releasedate;
            movie.CompanyId = companyId;
            movie.Price = price;
            //actors
            movie.Actors.Clear();
            movie.Actors = await _movieDbContext
                .Actors
                .Where(m => actorIds.Contains(m.Id)).ToListAsync();
            //Directors
            movie.Directors.Clear();
            //get the list of the selected directors
            movie.Directors = await _movieDbContext
                .Directors
                .Where(d => directorIds.Contains(d.Id)).ToListAsync();
            //image
            if (image != null)
            {
                if (movie.Image != null)
                {
                    movie.Image = await _fileService.Update(image, movie.Image);
                }
                else
                {
                    movie.Image = await _fileService.Store(image);
                }

            }
            //savechanges
            return await SaveChangesAsync();
        }
    }
}
