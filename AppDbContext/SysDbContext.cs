using AppDbContext.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppDbContext
{
    public class SysDbContext : DbContext
    {
        public SysDbContext(DbContextOptions<SysDbContext> options) : base(options)
        {

        }

        public DbSet<WorkMate> WorkMate { get; set; }
    }
}
