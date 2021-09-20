using Assignment.CoursesManagement.Core;
using AssignmentCoursesManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagement.Infrastructure.Tests
{
    public abstract class BaseEfRepoTestFixture
    {
        protected CoursesDbContext _dbContext;
        protected DbContextOptions<CoursesDbContext> options1;

        protected static DbContextOptions<CoursesDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<CoursesDbContext>();
            builder.UseInMemoryDatabase("CoursesManagementDb")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected CourseRepository GetCourseRepository()
        {
            options1 = CreateNewContextOptions();
            var mockLogger = new Mock<ILogger<ICourseRepository>>();

            _dbContext = new CoursesDbContext(options1);
            return new CourseRepository(_dbContext, mockLogger.Object);
        }

        // must use the same _dbContext with the same options
        protected UnitOfWork GetUnitWork()
        {
            return new UnitOfWork(_dbContext);
        }
    }
}
