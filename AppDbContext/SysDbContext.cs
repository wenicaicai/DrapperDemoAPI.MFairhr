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

        public virtual DbSet<WorkMate> WorkMate { get; set; }

        public virtual DbSet<Course> Course { get; set; }

        public virtual DbSet<Student> Student { get; set; }

        public virtual DbSet<Teacher> Teacher { get; set; }

        public virtual DbSet<Score> Score { get; set; }

        public DbSet<Employee> Employee { get; set; }
    }
}
