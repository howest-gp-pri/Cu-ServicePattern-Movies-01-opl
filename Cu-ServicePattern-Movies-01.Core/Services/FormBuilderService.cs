using Cu_ServicePattern_Movies_01.Core.Data;
using Cu_ServicePattern_Movies_01.Models;
using Cu_ServicePattern_Movies_01.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cu_ServicePattern_Movies_01.Core.Services
{
    public class FormBuilderService : IFormBuilderService
    {
        private readonly MovieDbContext _movieDbContext;

        public FormBuilderService(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public async Task<IEnumerable<SelectListItem>> GetActorsDropDown()
        {
            return await _movieDbContext.Actors
                        .OrderBy(a => a.Lastname)
                        .Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = $"{a.Firstname}{a.Lastname}"
                        }).ToListAsync();

        }

        public async Task<IEnumerable<SelectListItem>> GetCompaniesDropDown()
        {
            return await _movieDbContext.Companies
                    .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }).ToListAsync();
        }

        public async Task<List<CheckboxModel>> GetDirectorsCheckboxes()
        {
            return await _movieDbContext.Directors
                          .OrderBy(a => a.Lastname)
                          .Select(d => new CheckboxModel
                          {
                              Value = d.Id,
                              Text = $"{d.Firstname}{d.Lastname}",
                          }).ToListAsync();
        }

    }
}
