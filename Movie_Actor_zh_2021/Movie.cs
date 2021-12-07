using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Actor_zh_2021.App
{
    [Table("Movies")]
    public class Movie
    {
        public Movie() //CTOR
        {
            this.Actors = new HashSet<Actor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public  string Title { get; set; }
        [StringRange("Action", "Comedy", "Drama", "Fantasy", "Horror", "Mystery", "Romance", "Thriller")] //ATTRIBUTES (2 POINTS)
        public string Genre { get; set; }
        [StringRange("G", "PG", "PG-13", "R", "NC-17")] //ATTRIBUTES (2 POINTS)
        public string Rating { get; set; }
        public int YearOfRelease { get; set; }
        [NotMapped]
        public virtual ICollection<Actor> Actors { get; set; }
        public override string ToString()
        {
            return $"Id: {Id} - Title: {Title} - Genre: {Genre} - Rating: {Rating} - Year of Release: {YearOfRelease}";
        }
    }
}
