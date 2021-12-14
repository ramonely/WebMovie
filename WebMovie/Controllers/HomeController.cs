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
        public IActionResult Edit()
        {

            using (var connect = new MySqlConnection(Private.connection))
            {

                string sql = $"SELECT * FROM Movies";
              
                    connect.Open();
                    connect.Execute(sql);
                    List<Movie> allMovies = connect.Query<Movie>(sql).ToList();
                    connect.Close();
                    return View(allMovies);
  

            }
        }
        public IActionResult Delete(Movie m)
        {

            using (var connect = new MySqlConnection(Private.connection))
            {

                string sql = $"delete FROM Movies where id='{m.ID}'";

                connect.Open();
                connect.Execute(sql);
                connect.Close();
                return RedirectToAction("Edit","Home");


            }
        }



        public IActionResult Index()
        {

                return View();

        }
        public IActionResult NewInfo()
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

                string sqlI = $"SELECT * FROM Movies where id = '{m.ID}'";
                string sqlT = $"SELECT * FROM Movies where title = '{m.Title}'";
                string sqlG = $"SELECT * FROM Movies where genre = '{m.Genre}'";
                string sqlA = $"SELECT * FROM Movies where genre = '{m.Genre}' and title = '{m.Title}'";

                if (m.Title == null && m.Genre != null)
                {
                    connect.Open();
                    connect.Execute(sqlG);
                    List<Movie> allMoviesG = connect.Query<Movie>(sqlG).ToList();
                    connect.Close();
                    return View(allMoviesG);
                }
                else if (m.Genre == null && m.Title != null)
                {
                    connect.Open();
                    connect.Execute(sqlT);
                    List<Movie> allMoviesT = connect.Query<Movie>(sqlT).ToList();
                    connect.Close();
                    return View(allMoviesT);
                }

                else if (m.Genre != null && m.Title != null)
                {
                    connect.Open();
                    connect.Execute(sqlA);
                    List<Movie> allMoviesT = connect.Query<Movie>(sqlA).ToList();
                    connect.Close();
                    return View(allMoviesT);
                }
                else if (m.Genre == null && m.Title == null)
                {
                    connect.Open();
                    connect.Execute(sqlI);
                    List<Movie> allMoviesI = connect.Query<Movie>(sqlI).ToList();
                    connect.Close();
                    return View(allMoviesI);
                }
                else

                    return RedirectToAction("Search");
                

            }


        }
        public IActionResult Confirmation(Movie m)

        {
            using (var connect = new MySqlConnection(Private.connection))
            {

                string sql = $"insert into movies values(0,'{m.Title}', '{m.Genre}','{m.Years}','{m.Runtime}')";
                connect.Open();
                connect.Execute(sql);
                string allM = "select * from movies";
                List<Movie> allMovies = connect.Query<Movie>(allM).ToList();
                connect.Close();
                return View(allMovies);

            }
        }
        public IActionResult Changing(Movie m)

        {
            using (var connect = new MySqlConnection(Private.connection))
            {

                string sql = $"update movies set title='{m.Title}', genre='{m.Genre}', years={m.Years}, runtime={m.Runtime} where id={m.ID};"; 
                connect.Open();
                connect.Execute(sql);
                connect.Close();
                return RedirectToAction("Edit");

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
