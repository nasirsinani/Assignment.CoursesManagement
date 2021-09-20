using Assignment.CoursesManagement.Core;
using Assignment.CoursesManagement.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCoursesManagement.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CoursesDbContext context;

        public CourseRepository(CoursesDbContext context)
        {
            this.context = context;
        }
        public ICollection<Course> GetAll()
        {
            return context.Courses.Include(st => st.Students).ThenInclude(s => s.Student).ToList();
        }

        public void AddCourse(Course course)
        {
            context.Courses.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            var entry = context.Courses.First(e => e.Id == course.Id);
            context.Entry(course).CurrentValues.SetValues(course);
        }

        public async Task<Course> GetCourse(int id)
        {
            var course = await context.Courses
                .Include(st => st.Students)
                .ThenInclude(s => s.Student)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (course == null) throw new CourseNotFoundException(id);

            return course;
        }
    }
}
