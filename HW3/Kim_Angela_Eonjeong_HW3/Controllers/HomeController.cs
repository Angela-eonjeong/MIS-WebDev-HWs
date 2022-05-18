//using statements to give access to various libraries and namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Kim_Angela_Eonjeong_HW3.DAL;
using Kim_Angela_Eonjeong_HW3.Models;

namespace Kim_Angela_Eonjeong_HW3.Controllers
{
    public enum DisplayMovies {Greater, Less}
    public class HomeController : Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index(string SearchString)
        {

            var query = from b in _context.Movies
                        select b;

            if (SearchString is not null)
            {
                query = query.Where(b => b.Title.Contains(SearchString) ||
                               b.Description.Contains(SearchString));

                
            }

            List<Movie> SelectedMovies = query.Include(b => b.Genre).ToList();

            ViewBag.AllMovies = _context.Movies.Count();
            ViewBag.SelectedMovies = SelectedMovies.Count;

            return View(SelectedMovies.OrderBy(b => b.Title));
        }

        public IActionResult Details(int? id)//id is the id of the movie you want to see
        {
            if (id == null) //MovieID not specified
            {
                //user did not specify a MovieID – take them to the error view
                return View("Error", new String[] { "MovieID not specified - which movie do you want to view?" });
            }
            //look up the movie in the database based on the id; be sure to include the genre

            Movie movie = _context.Movies.Include(b => b.Genre)
                                             .FirstOrDefault(b => b.MovieID == id);

            if (movie == null) //No movie with this id exists in the database
            {
                //there is not a movie with this id in the database – show an error view
                return View("Error", new String[] { "Movie not found in database" });
            }

            //if code gets this far, all is well – display the details
            return View(movie);
        }

        public IActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();

            return View();
        }



        private SelectList GetAllGenres()
        {
            List<Genre> Genres = _context.Genres.ToList();

            Genre SelectNone = new Genre() { GenreID = 0, GenreName = "All Genres" };
            Genres.Add(SelectNone);

            //convert list to select list
            SelectList AllGenres = new SelectList(Genres.OrderBy(g => g.GenreID), "GenreID", "GenreName");

            //return the select list
            return AllGenres;
        }


        public IActionResult SearchResults(
            string Title, string Description, Rating? SearchRating,
          int Genre, DisplayMovies SelectedOpt,
          Decimal SelectedUserRating,
          DateTime? SearchReleaseDate)
        {



            List<Movie> SelectedMovies = new List<Movie>();

            var query = from r in _context.Movies select r;


            //title
            if (!string.IsNullOrEmpty(Title))
            {
                query = query.Where(r => r.Title.Contains(Title));
            }

            //Description
            if (!string.IsNullOrEmpty(Description))
            {
                query = query.Where(r => r.Description.Contains(Description));
            }

            Console.WriteLine(SearchRating);

            //GPA Rating
            if ( SearchRating != null )
            {
                query = query.Where(r => r.Rating == SearchRating);
            }


            /*if (SearchRating != 0)
            {
                query = query.Where(r => r.Rating == SearchRating);
            }*/


            //genre
            if (Genre != 0) // 0 = they chose "all genres" from the drop-down
            {
                Genre GenreToDisplay = _context.Genres.Find(Genre);
                query = query.Where(r => r.Genre == GenreToDisplay);
            }


            //Published Date 
            if (SearchReleaseDate != null)
            {
                query = query.Where(r => r.ReleaseDate >= SearchReleaseDate);
            }


            //User Rating
           switch (SelectedOpt)
            {
                case DisplayMovies.Greater:
                    query = query.Where(r => r.VoteAverage >= SelectedUserRating);
                    break;
                case DisplayMovies.Less:
                    query = query.Where(r => r.VoteAverage <= SelectedUserRating);
                    break;
                default:
                    break;
            }




            SelectedMovies = query.ToList();

            ViewBag.AllMovies = _context.Movies.Count();
            ViewBag.SelectedMovies = SelectedMovies.Count;

            return View("Index", SelectedMovies.OrderBy(b => b.Title));
        }

    }
}

