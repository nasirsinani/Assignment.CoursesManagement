using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Commands;
using Assignmet.CoursesManagement.Application.Interfaces;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using Castle.Core.Logging;
using CourseManagement.Application.Tests.DataSources;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CourseManagement.Application.Tests.Commands
{

    public class CreateCourseCommandHandlerTests
    {
        private readonly Mock<ICourseProfileService> mockCourseProfileService;
        private readonly Mock<ICourseRepository> mockCourseRepository;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<ILogger<CreateCourseCommand>> mockLogger;

        private readonly CancellationToken cancellationToken = CancellationToken.None;

        private CreateCourseCommandHandler commandHandler;
        public CreateCourseCommandHandlerTests()
        {
            mockCourseProfileService = new Mock<ICourseProfileService>();
            mockCourseRepository = new Mock<ICourseRepository>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockLogger = new Mock<ILogger<CreateCourseCommand>>();

            commandHandler = new CreateCourseCommandHandler(mockCourseProfileService.Object, 
                mockCourseRepository.Object, 
                mockUnitOfWork.Object, 
                mockLogger.Object);
        }

        [Fact]
        public void Constructor_NullCourseProfileService_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CreateCourseCommandHandler(null,
                    mockCourseRepository.Object,
                    mockUnitOfWork.Object,
                    mockLogger.Object
                    );
            });
        }

        [Fact]
        public void Constructor_NullCourseRepository_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CreateCourseCommandHandler(mockCourseProfileService.Object,
                    null,
                    mockUnitOfWork.Object,
                    mockLogger.Object
                    );
            });
        }

        [Fact]
        public void Constructor_NullUnitOfWork_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CreateCourseCommandHandler(mockCourseProfileService.Object,
                    mockCourseRepository.Object,
                    null,
                    mockLogger.Object
                    );
            });
        }

        [Fact]
        public void Constructor_ValidParameters_ReturnsOk()
        {
            var command = new CreateCourseCommandHandler(mockCourseProfileService.Object,
                    mockCourseRepository.Object,
                    mockUnitOfWork.Object,
                    mockLogger.Object
                    );

            Assert.NotNull(command);
        }

        [Fact]
        public void CommandHandler_NullName_ReturnsArgumentNullException()
        {
            var command = new CreateCourseCommand { Name = null };

            Assert.ThrowsAsync<ArgumentNullException>(async () => { await commandHandler.Handle(command, cancellationToken); });

        }

        [Fact]
        public async Task CommandHandler_ValidCourse_ReturnsOkAsync()
        {
            var command = TestDataSource.GetCourseCommand();
            var course = command.ToDomainModel();

            mockCourseProfileService.Setup(p => p.AddOrUpdateStudents(It.IsAny<Course>(), It.IsAny<List<SelectedStudentData>>())).ReturnsAsync(course);
            mockCourseRepository.Setup(b => b.AddCourse(It.IsAny<Course>()));
            mockUnitOfWork.Setup(t => t.CompleteAsync());

            var response = await commandHandler.Handle(command, cancellationToken);

            Assert.Equal(command.CourseId, response.CourseId);
            Assert.Equal(command.Name, response.Name);
            Assert.Equal(command.Price, response.Price);
            Assert.Equal(command.CourseType, response.CourseType);
            Assert.Equal(command.Description, command.Description);
            Assert.Equal(command.Students, command.Students);

            mockCourseProfileService.Verify(p => p.AddOrUpdateStudents(It.IsAny<Course>(), It.IsAny<List<SelectedStudentData>>()), Times.Once);
            mockCourseProfileService.VerifyNoOtherCalls();
            mockCourseRepository.Verify(t => t.AddCourse(It.IsAny<Course>()));
            mockCourseRepository.VerifyNoOtherCalls();
            mockUnitOfWork.Verify(t => t.CompleteAsync(), Times.Once);
        }
    }
}
