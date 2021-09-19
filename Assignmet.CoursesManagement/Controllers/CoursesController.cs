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

        public ActionResult Index()
        {
            var courses = courseRepository.GetAll();

            var coursesViewModel = courses.Select(course => course.ToViewModel());

            return View(coursesViewModel);
        }

        public ActionResult Create()
        {
            var courseViewModel = new CourseViewModel { Students = PopulateStudentData() };

            return View(courseViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                var course = courseViewModel.ToDomainModel();
                var courseToAdd = await AddOrUpdateStudents(course, courseViewModel.Students);

                courseRepository.AddCourse(courseToAdd);
                await unitOfWork.CompleteAsync();


                return RedirectToAction("Index");
            }

            return View(courseViewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var course = await courseRepository.GetCourse(id).ConfigureAwait(false);
            var students = studentRepository.GetAll().ToList();
            var courseViewModel = course.ToViewModel(students);

            return View(courseViewModel);
        }

        [HttpPost]
        public async Task<ActionResult>Edit(CourseViewModel courseViewModel)
        {
            if(ModelState.IsValid)
            {
                var course = await courseRepository.GetCourse(courseViewModel.CourseId);

                var courseToUpdate = await AddOrUpdateKeepExistingStudentsAsync(course, courseViewModel.Students);

                courseToUpdate.Name = courseViewModel.Name;
                courseToUpdate.Price = courseViewModel.Price;
                courseToUpdate.Description = courseViewModel.Description;
                courseToUpdate.CourseType = courseViewModel.CourseType;
                
                courseRepository.UpdateCourse(courseToUpdate);
                await unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }

            return View(courseViewModel);
        }

        public async Task<ActionResult> DetailsAsync(int id)
        {
            var allStudents = studentRepository.GetAll().ToList();

            var course = await courseRepository.GetCourse(id).ConfigureAwait(false);

            var courseViewModel = course.ToViewModel(allStudents);

            return View(courseViewModel);
        }

        private List<SelectedStudentData> PopulateStudentData()
        {
            var students = studentRepository.GetAll();
            var studentData = new List<SelectedStudentData>();

            foreach (var student in students)
            {
                studentData.Add(new SelectedStudentData
                {
                    StudentId = student.Id,
                    Name = student.Name,
                    StudentNo = student.StudentNo
                });
            }

            return studentData;
        }
        private async Task<Course> AddOrUpdateStudents(Course courseProfile, IEnumerable<SelectedStudentData> assignedStudents)
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

        private async Task<Course> AddOrUpdateKeepExistingStudentsAsync(Course courseProfile, List<SelectedStudentData> assignedStudents)
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
                if(!studentIds.Contains(id))
                {
                    var cs = new CourseStudent { CourseId = courseProfile.Id, StudentId = id };
                    courseProfile.Students.Add(cs);
                }
            }

            return courseProfile;
        }
    }
}
