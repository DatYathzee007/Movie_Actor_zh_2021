using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Actor_zh_2021.App
{
    [Table("Actors")]
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public override string ToString()
        {
            return $"Id: {Id} - Name: {Name} - Sex: {Sex}";
        }


    }
}
