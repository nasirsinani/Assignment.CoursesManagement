using Assignment.CoursesManagement.Core;
using AssignmentCoursesManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Queries
{
    public class CourseQueries : ICourseQueries
    {
        private readonly ICourseRepository courseRepository;

        public CourseQueries(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public ICollection<Course> GetAll()
        {
            return courseRepository.GetAll();
        }

        public async Task<Course> GetCourseById(int id)
        { 
            return await courseRepository.GetCourse(id).ConfigureAwait(false);
        }
    }
}
