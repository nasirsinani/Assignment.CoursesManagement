using Assignment.CoursesManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Models.ViewModels
{
    public static class ViewModelHelpers
    {
        public static CourseViewModel ToViewModel(this Course course)
        {
            var courseViewModel = new CourseViewModel
            {
                CourseId = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                CourseType = course.CourseType
            };

            foreach (var student in course.Students)
            {
                var std = new SelectedStudentData
                {
                    StudentId = student.StudentId,
                    Name = student.Student.Name,
                    StudentNo = student.Student.StudentNo,
                    IsSelected = true
                };

                courseViewModel.Students.Add(std);
            }

            return courseViewModel;
        }

        public static CourseViewModel ToViewModel(this Course course, ICollection<Student> allDbStudents)
        {
            var courseViewModel = new CourseViewModel
            {
                CourseId = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                CourseType = course.CourseType
            };

            List<SelectedStudentData> allStudents = new List<SelectedStudentData>();

            foreach (var student in allDbStudents)
            {
                var assignedStudent = new SelectedStudentData
                {
                    StudentId = student.Id,
                    Name = student.Name,
                    StudentNo = student.StudentNo,
                    IsSelected = course.Students.FirstOrDefault(x => x.StudentId == student.Id) != null
                };

                allStudents.Add(assignedStudent);
            }

            courseViewModel.Students = allStudents;

            return courseViewModel;
        }
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
