using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Commands
{
    public class CreateCourseCommand : IRequest<CreateCourseCommand>
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public CourseType CourseType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<SelectedStudentData> Students { get; set; }
        public CreateCourseCommand()
        {
            Students = new List<SelectedStudentData>();
        }
    }
}
