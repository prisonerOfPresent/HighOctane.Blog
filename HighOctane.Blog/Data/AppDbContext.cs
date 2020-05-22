using HighOctane.Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOctane.Blog.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public AppDbContext( DbContextOptions<AppDbContext> options ) 
            : base( options ) 
        {

        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Post>()
        //        .HasOne(p => p.Category)
        //        .WithOne();


        //    modelBuilder.Entity<Post>()
        //        .HasMany(b => b.Tags)
        //        .WithOne();
        //}
    }
}
