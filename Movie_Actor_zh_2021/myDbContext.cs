using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Actor_zh_2021.App
{
    public partial class myDbContext : DbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public myDbContext([System.Diagnostics.CodeAnalysis.NotNull] DbContextOptions options) : base(options)
        {
        }
        public myDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder), nameof(this.OnConfiguring) + " took null parameter!!!");
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\Movies.mdf; Integrated Security=True; MultipleActiveResultSets = true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder), nameof(this.OnModelCreating) + " took null parameter!!!");
            }
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasOne(actor => actor.Movie).WithMany(movie => movie.Actors).HasForeignKey(actor => actor.MovieId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
