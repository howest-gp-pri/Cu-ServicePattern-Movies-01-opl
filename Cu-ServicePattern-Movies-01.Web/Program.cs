using Cu_ServicePattern_Movies_01;
using Cu_ServicePattern_Movies_01.Core.Data;
using Cu_ServicePattern_Movies_01.Core.Interfaces;
using Cu_ServicePattern_Movies_01.Core.Services;
using Cu_ServicePattern_Movies_01.Core.Services.Interfaces;
using Cu_ServicePattern_Movies_01.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core_MovieShop_opl_Afst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Add entity framework database
            builder.Services.AddDbContext<MovieDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesDb")));
            //register session
            builder.Services.AddSession();
            //register own services
            builder.Services.AddScoped<IFormBuilderService, FormBuilderService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IMovieService,MovieService>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}