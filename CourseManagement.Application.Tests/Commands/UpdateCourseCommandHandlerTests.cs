using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Commands;
using Assignmet.CoursesManagement.Application.Interfaces;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using Assignmet.CoursesManagement.Application.Queries;
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
    public class UpdateCourseCommandHandlerTests
    {
        private readonly Mock<ICourseProfileService> mockCourseProfileService;
        private readonly Mock<ICourseQueries> mockCourseQueries;
        private readonly Mock<ICourseRepository> mockCourseRepository;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<ILogger<UpdateCourseCommand>> mockLogger;

        private readonly CancellationToken cancellationToken = CancellationToken.None;

        private UpdateCourseCommandHandler commandHandler;

        public UpdateCourseCommandHandlerTests()
        {
            mockCourseProfileService = new Mock<ICourseProfileService>();
            mockCourseQueries = new Mock<ICourseQueries>();
            mockCourseRepository = new Mock<ICourseRepository>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockLogger = new Mock<ILogger<UpdateCourseCommand>>();

            commandHandler = new UpdateCourseCommandHandler(mockCourseProfileService.Object,
                mockCourseQueries.Object,
                mockCourseRepository.Object,
                mockUnitOfWork.Object,
                mockLogger.Object);
        }
        [Fact]
        public void Constructor_NullCourseProfileService_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UpdateCourseCommandHandler(null,
                    mockCourseQueries.Object,
                    mockCourseRepository.Object,
                    mockUnitOfWork.Object,
                    mockLogger.Object
                    );
            });
        }

        [Fact]
        public void Constructor_NullCourseQueries_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UpdateCourseCommandHandler(mockCourseProfileService.Object,
                    null,
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
                new UpdateCourseCommandHandler(mockCourseProfileService.Object,
                    mockCourseQueries.Object,
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
                new UpdateCourseCommandHandler(mockCourseProfileService.Object,
                    mockCourseQueries.Object,
                    mockCourseRepository.Object,
                    null,
                    mockLogger.Object
                    );
            });
        }

        [Fact]
        public void Constructor_ValidParameters_ReturnsOk()
        {
            var command = new UpdateCourseCommandHandler(mockCourseProfileService.Object,
                    mockCourseQueries.Object,
                    mockCourseRepository.Object,
                    mockUnitOfWork.Object,
                    mockLogger.Object
                    );

            Assert.NotNull(command);
        }
    }
}
