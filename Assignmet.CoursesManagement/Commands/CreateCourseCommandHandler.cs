using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Interfaces;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Commands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseCommand>
    {
        private readonly ICourseProfileService courseProfileService;
        private readonly ICourseRepository courseRepository;
        private readonly IUnitOfWork unitOfWork;
        public ILogger<CreateCourseCommand> Logger { get; }
        public CreateCourseCommandHandler(ICourseProfileService courseProfileService, 
            ICourseRepository courseRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<CreateCourseCommand> logger)
        {
            this.courseProfileService = courseProfileService ?? throw new ArgumentNullException(nameof(courseProfileService));
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Logger = logger;
        }

        public async Task<CreateCourseCommand> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Creating new course...");

            if (string.IsNullOrWhiteSpace(request.Name)) throw new ArgumentNullException(nameof(request.Name));

            var course = request.ToDomainModel();
            var courseToAdd = await courseProfileService.AddOrUpdateStudents(course, request.Students);

            courseRepository.AddCourse(courseToAdd);
            await unitOfWork.CompleteAsync();

            Logger.LogInformation($"Course  {@courseToAdd} is created successfully", courseToAdd);
            return request;
        }
    }
}
