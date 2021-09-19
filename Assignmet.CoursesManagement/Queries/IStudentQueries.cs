using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Queries
{
    public interface IStudentQueries
    {
        List<Student> GetAll();
        List<SelectedStudentData> GetSelectedStudents();
    }
}
