using Assignment.CoursesManagement.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Models.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public CourseType CourseType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<StudentData> Students { get; set; }
    }
}
