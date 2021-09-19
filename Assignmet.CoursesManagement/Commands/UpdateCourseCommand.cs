using Assignmet.CoursesManagement.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Commands
{
    public class UpdateCourseCommand : IRequest<CourseViewModel>
    {
        public CourseViewModel CourseViewModel { get; set; }
    }
}
