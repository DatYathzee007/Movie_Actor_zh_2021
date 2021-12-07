using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Movie_Actor_zh_2021.App
{
    public static class XmlLoader
    {
        /*
        public XmlLoader(string connString)
        {
            XmlGetData(connString);
        }
        */
        /*
         <Movies>                                   //ROOT
             <Movie>                                    //DESCENDANTS("movie")
                <Title>The Godfather</Title>
                <Genre>Drama</Genre>
                <Rating>R</Rating>
                <YearOfRelease>1972</YearOfRelease>
                <Actors>
                    <Actor>
                        <Name>Marlon Brando</Name>
                        <Sex>Male</Sex>
                    </Actor>
                    <Actor>
                        <Name>Al Pacino</Name>
                        <Sex>Male</Sex>
                    </Actor>
                    <Actor>
                        <Name>James Caan</Name>
                        <Sex>Male</Sex>
                    </Actor>
                    <Actor>
                        <Name>Diane Keaton</Name>
                        <Sex>Female</Sex>
                    </Actor>
                </Actors>
            </Movie>
        </Movies>
         */
        public static List<Movie> XmlGetDataMovie(string connString)
        {
            List<Movie> listMovies = new List<Movie>();
            var movies = XDocument.Load(connString).Descendants("Movie"); // inside Root (root: Movies)
            foreach (var movie in movies)
            {
                List<Actor> listActors = new List<Actor>();
                foreach (var actor in movie.Descendants("Actor"))
                {
                    Actor newActor = new Actor {
                        Name = actor.Element("Name").Value,
                        Sex = actor.Element("Sex").Value
                    };
                    listActors.Add(newActor);
                }
                

                Movie newMovie = new Movie {
                    Title = movie.Element("Title").Value,
                    Genre = movie.Element("Genre").Value,
                    Rating = movie.Element("Rating").Value,
                    YearOfRelease = int.Parse(movie.Element("YearOfRelease").Value),
                    Actors = listActors
                };
                listMovies.Add(newMovie);
                

            }
            return listMovies;
        }
    }
}
