using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Interfaces;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Services
{
    public class CourseProfileService : ICourseProfileService
    {
        private readonly IStudentRepository studentRepository;

        public CourseProfileService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public async Task<Course> AddOrUpdateKeepExistingStudentsAsync(Course courseProfile, List<SelectedStudentData> assignedStudents)
        {
            var selectedStudentsId = assignedStudents.Where(c => c.IsSelected).Select(st => st.StudentId);
            var dbStudentIds = courseProfile.Students.Select(dbStudent => dbStudent.StudentId);
            var studentIds = dbStudentIds as int[] ?? dbStudentIds.ToArray();
            var studentIdsToDelete = studentIds.Where(id => !selectedStudentsId.Contains(id)).ToList();

            foreach (var id in studentIdsToDelete)
            {
                var student = await studentRepository.Find(id).ConfigureAwait(false);
                // var cs = new CourseStudent { CourseId = course.Id, StudentId = id, Course = course, Student = student};
                var cs = courseProfile.Students.FirstOrDefault(c => (c.StudentId == id) && (c.CourseId == courseProfile.Id));
                courseProfile.Students.Remove(cs);
            }

            foreach (var id in selectedStudentsId)
            {
                if (!studentIds.Contains(id))
                {
                    var cs = new CourseStudent { CourseId = courseProfile.Id, StudentId = id };
                    courseProfile.Students.Add(cs);
                }
            }

            return courseProfile;
        }

        public async Task<Course> AddOrUpdateStudents(Course courseProfile, IEnumerable<SelectedStudentData> assignedStudents)
        {
            if (assignedStudents == null) return courseProfile;

            if (courseProfile.Id != 0)
            {
                foreach (var student in courseProfile.Students.ToList())
                {
                    courseProfile.Students.Remove(student);
                }

                foreach (var student in assignedStudents.Where(c => c.IsSelected))
                {
                    var s = await studentRepository.Find(student.StudentId).ConfigureAwait(false);
                    var cs = new CourseStudent { CourseId = courseProfile.Id, StudentId = s.Id };
                    courseProfile.Students.Add(cs);
                }
            }

            else
            {
                foreach (var assignedStudent in assignedStudents.Where(c => c.IsSelected))
                {
                    var student = new Student { Id = assignedStudent.StudentId };
                    var cs = new CourseStudent { CourseId = courseProfile.Id, StudentId = student.Id };
                    courseProfile.Students.Add(cs);
                }
            }

            return courseProfile;
        }
    }
}
