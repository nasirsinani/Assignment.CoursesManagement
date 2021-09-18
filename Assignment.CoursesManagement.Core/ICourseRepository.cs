using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.CoursesManagement.Core
{
    public interface ICourseRepository
    {
        Task<Course> GetCourse(int id);
        void AddCourse(Course course);
        void RemoveCourse(Course course);
    }
}
