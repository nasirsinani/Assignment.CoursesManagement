using Assignment.CoursesManagement.Core;
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

        public StudentRepository(CoursesDbContext context)
        {
            this.context = context;
        }

        public async Task<Student> Find(int id)
        {
            return await context.Students.FindAsync(id).ConfigureAwait(false);
        }

        public ICollection<Student> GetAll()
        {
            return context.Students.ToList();
        }
    }
}
