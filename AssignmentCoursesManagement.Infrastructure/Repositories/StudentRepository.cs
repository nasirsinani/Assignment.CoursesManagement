using Assignment.CoursesManagement.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCoursesManagement.Infrastructure
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CoursesDbContext context;
        public ILogger<IStudentRepository> Logger { get; }

        public StudentRepository(CoursesDbContext context, ILogger<IStudentRepository> logger)
        {
            this.context = context;
            Logger = logger;
        }

        public async Task<Student> Find(int id)
        {
            Logger.LogInformation($"Getting student with id: {id}");
            return await context.Students.FindAsync(id);
        }

        public ICollection<Student> GetAll()
        {
            Logger.LogInformation("Getting students from the database...");
            return context.Students.ToList();
        }
    }
}
