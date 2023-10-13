using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cu_ServicePattern_Movies_01.Core.Services.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> GetbyIdAsync(int id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<bool> CreateAsync(string title, DateTime releaseDate, decimal price, int companyId, IFormFile image,
            IEnumerable<int> actorIds, IEnumerable<int> directorIds);
        Task<bool> UpdateAsync(int id,DateTime releaseDate ,string title, decimal price, int companyId, IFormFile image,
            IEnumerable<int> actorIds, IEnumerable<int> directorIds);
        Task<bool> DeleteAsync(int id);
        IQueryable<Movie> GetAll();
        Task<bool> SaveChangesAsync();
    }
}
