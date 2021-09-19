using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Interfaces;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using Assignmet.CoursesManagement.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Commands
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseViewModel>
    {
        private readonly ICourseProfileService courseProfileService;
        private readonly ICourseQueries courseQueries;
        private readonly ICourseRepository courseRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateCourseCommandHandler(ICourseProfileService courseProfileService, ICourseQueries courseQueries, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            this.courseProfileService = courseProfileService ?? throw new ArgumentNullException(nameof(courseProfileService));
            this.courseQueries = courseQueries ?? throw new ArgumentNullException(nameof(courseQueries)); 
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<CourseViewModel> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseViewModel = request.CourseViewModel;
            var course = await courseQueries.GetCourseById(courseViewModel.CourseId);
            var courseToUpdate = await courseProfileService.AddOrUpdateKeepExistingStudentsAsync(course, courseViewModel.Students);

            courseToUpdate.Name = courseViewModel.Name;
            courseToUpdate.Price = courseViewModel.Price;
            courseToUpdate.Description = courseViewModel.Description;
            courseToUpdate.CourseType = courseViewModel.CourseType;

            courseRepository.UpdateCourse(courseToUpdate);
            await unitOfWork.CompleteAsync();

            return courseViewModel;
        }
    }
}
