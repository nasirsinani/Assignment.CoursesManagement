using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Commands;
using Assignmet.CoursesManagement.Application.Filters;
using Assignmet.CoursesManagement.Application.Interfaces;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using Assignmet.CoursesManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class CoursesController : Controller
    {
        private readonly IMediator mediator;
        private readonly IStudentQueries studentQueries;
        private readonly ICourseQueries courseQueries;

        public CoursesController(IMediator mediator, IStudentQueries studentQueries, ICourseQueries courseQueries)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.studentQueries = studentQueries ?? throw new ArgumentNullException(nameof(studentQueries));
            this.courseQueries = courseQueries ?? throw new ArgumentNullException(nameof(courseQueries));
        }

        public ActionResult Index()
        {

            var courses = courseQueries.GetAll();

            var coursesViewModel = courses.Select(course => course.ToViewModel());

            return View(coursesViewModel);
        }

        public ActionResult Create()
        {

            var studentData = studentQueries.GetSelectedStudents();

            var courseViewModel = new CreateCourseCommand { Students = studentData };

            return View(courseViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateCourseCommand courseViewModel)
        {
            if (ModelState.IsValid)
            {
                await mediator.Send(courseViewModel);
                return RedirectToAction("Index");
            }

            return View(courseViewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var course = await courseQueries.GetCourseById(id);
            var students = studentQueries.GetAll();
            var courseViewModel = course.ToViewModel(students);

            return View(courseViewModel);
        }

        [HttpPost]
        public async Task<ActionResult>Edit(CourseViewModel courseViewModel)
        {
            if(ModelState.IsValid)
            {
                var command = new UpdateCourseCommand { CourseViewModel = courseViewModel };
                await mediator.Send(command);
                
                return RedirectToAction("Index");
            }

            return View(courseViewModel);
        }

        public async Task<ActionResult> DetailsAsync(int id)
        {
            var allStudents = studentQueries.GetAll();

            var course = await courseQueries.GetCourseById(id).ConfigureAwait(false);

            var courseViewModel = course.ToViewModel(allStudents);

            return View(courseViewModel);
        }
    }
}
