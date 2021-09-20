using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Commands;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Tests.DataSources
{
    internal static class TestDataSource
    {
        public static CreateCourseCommand GetCourseCommand()
        {
            return new CreateCourseCommand()
            {
                CourseId = 1,
                CourseType =
                CourseType.Free,
                Description = "some-test-description",
                Name = "some-test-name",
                Students = new List<SelectedStudentData> { new SelectedStudentData {
                    StudentId = 1,
                    Name = "some-test-student-name",
                    StudentNo = "some-test-student-no",
                    IsSelected = false} }
            };
        }
    }
}
