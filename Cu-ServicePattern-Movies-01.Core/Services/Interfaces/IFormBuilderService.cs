using Microsoft.AspNetCore.Mvc.Rendering;
using Cu_ServicePattern_Movies_01.Core.Models;
using Cu_ServicePattern_Movies_01.Models;

namespace Cu_ServicePattern_Movies_01.Services.Interfaces
{
    public interface IFormBuilderService
    {
        Task<IEnumerable<SelectListItem>> GetCompaniesDropDown();
        Task<IEnumerable<SelectListItem>> GetActorsDropDown();
        Task<List<CheckboxModel>> GetDirectorsCheckboxes();
    }
}
