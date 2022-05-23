using Microsoft.EntityFrameworkCore;
using NationalPark.Models;
using System;

namespace NationalPark.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Park> Parks { get; set;}
        public DbSet<Trail> Trails { get; set;}
    }
}
