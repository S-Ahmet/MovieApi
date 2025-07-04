using Microsoft.EntityFrameworkCore;
using MoviApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Persistence.Context
{
    public class MovieContext : DbContext 
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-GMFVKU1D\\SQLEXPRESS;initial Catalog=ApiMovieDb;integrated Security =true;TrustServerCErtificate=true");
            }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Cast> Casts { get; set; }


    }
}
