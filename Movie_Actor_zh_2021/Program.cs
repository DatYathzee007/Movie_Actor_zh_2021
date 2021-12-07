using System;
using System.Linq;
using System.Xml.Linq;

namespace Movie_Actor_zh_2021.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //DATABASE CREATION (7.5 POINTS)
            myDbContext ctx = new myDbContext();
            //ACCESSING XML FILE AND SAVING DATA (6 POINTS)
            var movies = XmlLoader.XmlGetDataMovie("Movies.xml");
            foreach (var movie in movies)
            {
                foreach (var actor in movie.Actors)
                {
                    ctx.Add(actor);
                }
                ctx.Add(movie);
            }
            ctx.SaveChanges();

            //PRINT RESULT
            foreach (var movie in ctx.Movies)
            {
                Console.WriteLine(movie);
                foreach (var actor in movie.Actors)
                {
                    Console.WriteLine("     " + actor);
                }
            }

            //DATA QUERYING (SHOW THEIR RESULTS ON THE CONSOLE) (14 POINTS)
            //a)	Display the number of actors in the database on the console. (1 point)
            var a = ctx.Actors.Count();
            Console.WriteLine($"Display the number of actors in the database on the console. \n{a}");

            //b)	Display the male actors on the console. (1 point)
            var b = ctx.Actors.Where(x => x.Sex == "Male").Count();
            Console.WriteLine($"Display the male actors on the console. \n{b}");

            //c)	Display the most recent movie(1 point)
            var c = ctx.Movies.OrderByDescending(x => x.YearOfRelease).First();
            Console.WriteLine($"Display the most recent movie. \n{c}");

            //d)	Display the oldest movie that has a femail actor in it(2.5 points)
            Console.WriteLine($"Display the oldest movie that has a femail actor in it:");

            var trial = ctx.Actors
                .Join(ctx.Movies, actor => actor.MovieId, movie => movie.Id, (actor, movie) => new { movie.Title, movie.YearOfRelease, actor.Name, actor.Sex })
                .Where(x => x.Sex == "Female")
                .Distinct().OrderBy(x => x.YearOfRelease).First();
            Console.WriteLine(trial);

            var d1 = from movie in ctx.Movies
                     join actor in ctx.Actors on movie.Id equals actor.MovieId
                     where actor.Sex == "female"
                     select new { Title = movie.Title, Year = movie.YearOfRelease }; // default increasing order.
            var d2 = d1.Distinct().OrderBy(x => x.Year).First(); // OrderBy not needed, default increasing order.
            Console.WriteLine(d2.Title + " - " + d2.Year);

            //e)	Display the movies that contain female actors in ascending order of their year of creation. (2.5 points)
            Console.WriteLine("Display the movies that contain female actors in ascending order of their year of creation:");
            var e = from movie in ctx.Movies
                    join actor in ctx.Actors on movie.Id equals actor.MovieId
                    where actor.Sex == "female"
                    select new { Title = movie.Title, Year = movie.YearOfRelease }; // default increasing order.
            var e2 = e.Distinct().OrderBy(x => x.Year);
            foreach (var movie in e2)
            {
                Console.WriteLine(movie);
            }

            //f)	For each movie, display a data structure, which contains the name of the movie and the female and male actors separately
            //      in ascending order of the actor’s name. (3 points)
            Console.WriteLine("For each movie, display a data structure, which contains the name of the movie and the female and male actors separately in ascending order of the actor’s name:");
            var f = from movie in ctx.Movies
                    join actor in ctx.Actors on movie.Id equals actor.MovieId
                    select new { Title = movie.Title, Actor = actor.Name, Sex = actor.Sex }; // default increasing order.
            foreach (var movie in f.OrderBy(x => x.Sex).ThenBy(x => x.Actor))
            {
                Console.WriteLine(movie);
            }

            //g)	For each movie genre, find the the movie that has the most male actors, then project it into a data structure that contains the Genre,
            //      the Name of the movie and the NumberOfMaleActors property. (3 points)
            Console.WriteLine("For each movie genre, find the the movie that has the most male actors, then project it into a data structure that contains the Genre, the Name of the movie and the NumberOfMaleActors property:");
            var g = from movie in ctx.Movies
                    join actor in ctx.Actors on movie.Id equals actor.MovieId
                    where actor.Sex == "Male"
                    group movie by movie.Title into asd2
                    select new { Title = asd2.Key, Cnt = asd2.Count() };

            var x = ctx.Actors
                .Join(ctx.Movies, actor => actor.MovieId, movie => movie.Id, (actor, movie) => new { movie.Title, movie.Genre, actor.Sex })
                .Where(x => x.Sex == "Male")
                .GroupBy(x=>x.Title)
                .Select(g => new {Title = g.Key , Count = g.Count()});
       
            foreach (var item in x)
            {
                Console.WriteLine(item);
            }



        }
    }
}
