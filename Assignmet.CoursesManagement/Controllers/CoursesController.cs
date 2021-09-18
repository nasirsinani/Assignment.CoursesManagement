using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IUnitOfWork unitOfWork;

        public CoursesController(IStudentRepository studentRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.unitOfWork = unitOfWork;
        }
        public ActionResult Create()
        {
            var courseViewModel = new CourseViewModel { Students = PopulateStudentData() };

            return View(courseViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CourseViewModel courseViewModel)
        {
            if(ModelState.IsValid)
            {
                var course = courseViewModel.ToDomainModel();
                var td = await AddOrUpdateStudents(course, courseViewModel.Students);

                courseRepository.AddCourse(td);
                await unitOfWork.CompleteAsync();


                return RedirectToAction("Index");
            }

            return View(courseViewModel);
        }

        private List<StudentData> PopulateStudentData()
        {
            var students = studentRepository.GetAll();
            var studentData = new List<StudentData>();

            foreach (var student in students)
            {
                studentData.Add(new StudentData
                {
                    StudentId = student.Id,
                    Name = student.Name,
                    StudentNo = student.StudentNo
                });
            }

            return studentData;
        }
        private async Task<Course> AddOrUpdateStudents(Course courseProfile, IEnumerable<StudentData> assignedStudents)
        {
            if (assignedStudents == null) return courseProfile;

            if(courseProfile.Id != 0)
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
