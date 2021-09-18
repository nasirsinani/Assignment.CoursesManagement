using Assignment.CoursesManagement.Core;
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
        public void AddCourse(Course course)
        {
            context.Courses.Add(course);
        }

        public Task<Course> GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveCourse(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
