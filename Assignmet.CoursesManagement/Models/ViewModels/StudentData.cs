using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Models.ViewModels
{
    public class StudentData
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string StudentNo { get; set; }
        public bool IsSelected { get; set; }
    }
}
