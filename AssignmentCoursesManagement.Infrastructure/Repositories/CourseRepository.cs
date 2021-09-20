using Assignment.CoursesManagement.Core;
using Assignment.CoursesManagement.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public ILogger<ICourseRepository> Logger { get; }

        public CourseRepository(CoursesDbContext context, ILogger<ICourseRepository> logger)
        {
            this.context = context;
            Logger = logger;
        }
        public ICollection<Course> GetAll()
        {
            Logger.LogInformation("Getting the courses from the database...");
            return context.Courses.Include(st => st.Students).ThenInclude(s => s.Student).ToList();
        }

        public void AddCourse(Course course)
        {
            Logger.LogInformation($"Adding course {course}");
            context.Courses.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            Logger.LogInformation($"Updating course {course}");
            var entry = context.Courses.First(e => e.Id == course.Id);
            context.Entry(course).CurrentValues.SetValues(course);
        }

        public async Task<Course> GetCourse(int id)
        {
            Logger.LogInformation($"Getting course with id {id}");
            var course = await context.Courses
                .Include(st => st.Students)
                .ThenInclude(s => s.Student)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                Logger.LogError($"No course found with id {id}");
                throw new CourseNotFoundException(id);
            }

            return course;
        }
    }
}
