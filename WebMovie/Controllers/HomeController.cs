using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebMovie.Models;
using MySqlConnector;


namespace WebMovie.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {

                return View();

        }

        public IActionResult MovieReg()
        {

                return View();

            
        }
        public IActionResult Search()
        {

            return View();


        }
        public IActionResult SearchResult(Movie m)
        {

            using (var connect = new MySqlConnection(Private.connection))
            {


                string sqlT = $"SELECT * FROM Movies WHERE (Title = {m.Title})";
                string sqlG = $"SELECT * FROM Movies WHERE (Genre = {m.Genre})";
                if (m.Title == null )
                {
                    connect.Open();
                    connect.Execute(sqlG);
                    List<Movie> allMoviesG = connect.Query<Movie>(sqlG).ToList();
                    connect.Close();
                    return View(allMoviesG);
                }
                else if (m.Genre == null)
                {
                    connect.Open();
                    connect.Execute(sqlT);
                    List<Movie> allMoviesT = connect.Query<Movie>(sqlT).ToList();
                    connect.Close();
                    return View(allMoviesT);
                }
                else
                
                    return RedirectToAction("Search","Home");
                

            }


        }
        public IActionResult Confirmation(Movie m)

        {
            using (var connect = new MySqlConnection(Private.connection))
            {


                string sql = $"insert into movies values(0,@Title,@Genre,@Years,@Runtime)";
                connect.Open();
                connect.Execute(sql, new {Title = m.Title,Genre = m.Genre, Years = m.Years, Runtime = m.Runtime });
                string allM = "select * from movies";
                List<Movie> allMovies = connect.Query<Movie>(allM).ToList();
                connect.Close();
                return View(allMovies);

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
