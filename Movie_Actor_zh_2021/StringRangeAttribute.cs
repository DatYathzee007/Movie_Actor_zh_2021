using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Actor_zh_2021.App
{

    //ATTRIBUTES (2 POINTS)
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StringRangeAttribute : Attribute
    {
        //a)	Create an attribute called StringRangeAttribute. (0.5 points)
        //b)	Create the attribute so that it can be applied to properties alone. (0.5 points)
        //c)	The attribute should have a public List<string> property where the valid values can be stored. (0.5 points)
        //d)	Place the attribute on the Rating and Genre properties of the Movie class. (0.5 points)
        //i.Valid Values for Rating: G, PG, PG-13, R, NC-17
        //ii.Valid Values for Genre: Action, Comedy, Drama, Fantasy, Horror, Mystery, Romance, Thriller

        // Returns a new instance of the <see cref="StringRangeAttribute"/> class.
        public StringRangeAttribute(params string[] allowedStrings)
        {
            this.WhiteList = new List<string>(allowedStrings);
        }

 
        // Gets or Sets the list of allowed strings.
        public IList<string> WhiteList { get; set; }

    }
}
