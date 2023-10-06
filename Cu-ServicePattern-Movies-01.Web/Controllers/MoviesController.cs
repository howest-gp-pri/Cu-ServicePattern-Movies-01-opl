using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cu_ServicePattern_Movies_01.Core.Data;
using Cu_ServicePattern_Movies_01.ViewModels;
using Cu_ServicePattern_Movies_01.Services.Interfaces;
using Cu_ServicePattern_Movies_01.Core;
using Cu_ServicePattern_Movies_01.Core.Interfaces;

namespace Cu_ServicePattern_Movies_01.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieDbContext _movieDbContext;
        private readonly IFormBuilderService _formBuilderService;
        private readonly IFileService _fileService;

        public MoviesController(MovieDbContext movieDbContext, IFormBuilderService formBuilderService, IFileService fileService)
        {
            _movieDbContext = movieDbContext;
            _formBuilderService = formBuilderService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var moviesIndexViewModel = new MoviesIndexViewModel
            {
                Movies = await _movieDbContext.Movies.Select(m =>
                new MoviesInfoViewModel
                {
                    Id = m.Id,
                    Name = m.Title,
                    Price = m.Price,
                }).ToListAsync(),
            };
            moviesIndexViewModel.PageTitle = "Our movies";
            return View(moviesIndexViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            var movie = await _movieDbContext
                .Movies
                .Include(m => m.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if(movie == null)
            {
                return NotFound();
            }
            var moviesInfoViewModel = new MoviesInfoViewModel
            {
                Id = movie.Id,
                Name = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Company = new BaseViewModel 
                {
                    Id  = movie.Company.Id,
                    Name = movie.Company.Name,
                },
                Image = movie.Image,
            };
            moviesInfoViewModel.PageTitle = "Info";
            return View(moviesInfoViewModel);
        }
        //create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var moviesCreateViewModel = new MoviesCreateViewModel
            {
                ReleaseDate = DateTime.Now,
                Companies = await _formBuilderService.GetCompaniesDropDown(),
                Actors = await _formBuilderService.GetActorsDropDown(),
                Directors = await _formBuilderService.GetDirectorsCheckboxes(),
            };
            return View(moviesCreateViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MoviesCreateViewModel moviesCreateViewModel)
        {
            if(!ModelState.IsValid)
            {
                moviesCreateViewModel.Companies = await _formBuilderService.GetCompaniesDropDown();
                moviesCreateViewModel.Actors = await _formBuilderService.GetActorsDropDown();
               return View(moviesCreateViewModel);
            }
            //create the movie
            var movie = new Movie();
            movie.Title = moviesCreateViewModel.Title;
            movie.Price = moviesCreateViewModel.Price;
            movie.ReleaseDate = moviesCreateViewModel.ReleaseDate;
            movie.CompanyId = moviesCreateViewModel.CompanyId;
            //actors
            movie.Actors = await _movieDbContext
                .Actors
                .Where(m => moviesCreateViewModel.ActorIds.Contains(m.Id)).ToListAsync();
            //Directors
            //get the list of the selected directors
            var selectedDirectors = moviesCreateViewModel.Directors
                .Where(d => d.IsSelected == true)
                .Select(d => d.Value);
            movie.Directors = await _movieDbContext
                .Directors
                .Where(d => selectedDirectors.Contains(d.Id)).ToListAsync();
            //image
            if(moviesCreateViewModel.Image != null)
            {
                movie.Image = await _fileService.Store(moviesCreateViewModel.Image);
            }
            //add to context
            await _movieDbContext.Movies.AddAsync(movie);
            try 
            {
                await _movieDbContext.SaveChangesAsync();
            }
            catch(DbUpdateException dbUpdateException)
            {
                Console.WriteLine(dbUpdateException.Message);
            }
            return RedirectToAction("Index");
        }
        //update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var movie = await _movieDbContext
                .Movies
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            var moviesUpdateViewModel = new MoviesUpdateViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Price = movie.Price,
                ReleaseDate = movie.ReleaseDate,
                CompanyId = (int)movie.CompanyId,
                Companies = await _formBuilderService.GetCompaniesDropDown(),
                Actors = await _formBuilderService.GetActorsDropDown(),
                ActorIds = movie.Actors.Select(a => a.Id),
                Directors = await _formBuilderService.GetDirectorsCheckboxes(),
            };
            //check the directors
            var directorIds = movie.Directors.Select(d => d.Id);
            
            foreach(var checkbox in moviesUpdateViewModel.Directors)
            {
                if(directorIds.Contains(checkbox.Value))
                {
                    checkbox.IsSelected = true;
                }
            }
            return View(moviesUpdateViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MoviesUpdateViewModel moviesUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                moviesUpdateViewModel.Companies = await _formBuilderService.GetCompaniesDropDown();
                moviesUpdateViewModel.Actors = await _formBuilderService.GetActorsDropDown();
                return View(moviesUpdateViewModel);
            }
            //update
            var movie = await _movieDbContext
                .Movies
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .FirstOrDefaultAsync(m => m.Id == moviesUpdateViewModel.Id);
            if (movie == null) 
            {
                return NotFound();
            }
            //edit the properties
            movie.Title = moviesUpdateViewModel.Title;
            movie.ReleaseDate = moviesUpdateViewModel.ReleaseDate;
            movie.CompanyId = moviesUpdateViewModel.CompanyId;
            movie.Price = moviesUpdateViewModel.Price;
            //actors
            movie.Actors.Clear();
            movie.Actors = await _movieDbContext
                .Actors
                .Where(m => moviesUpdateViewModel.ActorIds.Contains(m.Id)).ToListAsync();
            //Directors
            movie.Directors.Clear();
            //get the list of the selected directors
            var selectedDirectors = moviesUpdateViewModel.Directors
                .Where(d => d.IsSelected == true)
                .Select(d => d.Value);
            movie.Directors = await _movieDbContext
                .Directors
                .Where(d => selectedDirectors.Contains(d.Id)).ToListAsync();
            //image
            if(moviesUpdateViewModel.Image != null)
            {
                if(movie.Image != null)
                {
                    movie.Image = await _fileService.Update(moviesUpdateViewModel.Image, movie.Image);
                }
                else
                {
                    movie.Image = await _fileService.Store(moviesUpdateViewModel.Image);
                }
                
            }
            //savechanges
            try
            {
                await _movieDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                Console.WriteLine(dbUpdateException.Message);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var movie = await _movieDbContext.Movies
                .FirstOrDefaultAsync(m =>  m.Id == id);
            if(movie == null)
            {
                return NotFound();
            }
            var moviesDeleteViewModel = new MoviesDeleteViewModel
            {
                Id = movie.Id,
                Name = movie.Title
            };
            return View(moviesDeleteViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MoviesDeleteViewModel moviesDeleteViewModel)
        {
            var movie = await _movieDbContext.Movies
                .FirstOrDefaultAsync(m => m.Id == moviesDeleteViewModel.Id);
            if (movie == null)
            {
                return NotFound();
            }
            //delete the movie image
            if(!String.IsNullOrEmpty(movie.Image))
            {
                _fileService.Delete(movie.Image);
            }
            //delete the movie
            _movieDbContext.Movies.Remove(movie);
            //save the changes to the database
            //savechanges
            try
            {
                await _movieDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                Console.WriteLine(dbUpdateException.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
