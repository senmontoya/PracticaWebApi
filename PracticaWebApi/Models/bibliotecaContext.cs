﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace PracticaWebApi.Models
{
    public class bibliotecaContext : DbContext
    {
        public bibliotecaContext(DbContextOptions<bibliotecaContext> options) : base(options)
        {

        }

        public DbSet<libro> libro { get; set; }
    }
}
