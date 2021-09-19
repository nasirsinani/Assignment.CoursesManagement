using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Interfaces;
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
        private readonly ICourseProfileService courseProfileService;

        public CoursesController(IStudentRepository studentRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork, ICourseProfileService courseProfileService)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.unitOfWork = unitOfWork;
            this.courseProfileService = courseProfileService;
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
                var courseToAdd = await courseProfileService.AddOrUpdateStudents(course, courseViewModel.Students);

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

                var courseToUpdate = await courseProfileService.AddOrUpdateKeepExistingStudentsAsync(course, courseViewModel.Students);

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
    }
}
