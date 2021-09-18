using Assignment.CoursesManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Models.ViewModels
{
    public static class ViewModelHelpers
    {
        public static Course ToDomainModel(this CourseViewModel courseViewModel)
        {
            var course = new Course
            {
                Id = courseViewModel.CourseId,
                Name = courseViewModel.Name,
                Description = courseViewModel.Description,
                Price = courseViewModel.Price,
                CourseType = courseViewModel.CourseType
            };

            return course;
        }
    }
}
