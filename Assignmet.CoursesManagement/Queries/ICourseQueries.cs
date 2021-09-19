using Assignment.CoursesManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Queries
{
    public interface ICourseQueries
    {
        ICollection<Course> GetAll();
        Task<Course> GetCourseById(int id);
    }
}
