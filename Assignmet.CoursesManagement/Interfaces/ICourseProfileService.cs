using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Interfaces
{
    public interface ICourseProfileService
    {
        Task<Course> AddOrUpdateStudents(Course courseProfile, IEnumerable<SelectedStudentData> assignedStudents);
        Task<Course> AddOrUpdateKeepExistingStudentsAsync(Course courseProfile, List<SelectedStudentData> assignedStudents);
    }
}
