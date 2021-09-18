using Assignment.CoursesManagement.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCoursesManagement.Infrastructure
{
    public class CoursesDbContext : DbContext 
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>().HasKey(cs => new { cs.CourseId, cs.StudentId });
        }
    }
}
