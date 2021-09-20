using Assignment.CoursesManagement.Core;
using Assignment.CoursesManagement.Core.Exceptions;
using AssignmentCoursesManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoursesManagement.Infrastructure.Tests.Repositories
{
    public class CourseRepositoryTests : BaseEfRepoTestFixture
    {
        private readonly CourseRepository courseRepository;
        private readonly UnitOfWork unitOfWork;
        public CourseRepositoryTests()
        {
            courseRepository = GetCourseRepository();
            unitOfWork = GetUnitWork();
        }
        [Fact]
        public void AddCourse_NullName_ThrowsArgumentNullException()
        {
           
            var course = new Course();

            Assert.Throws<ArgumentNullException>(() => courseRepository.AddCourse(course));
        }
        [Fact]
        public async Task AddCourse_ValidCourse_ReturnsOk()
        {
            var testCourseName = "test-course";
            
            var course = new Course { Name = testCourseName };

            courseRepository.AddCourse(course);
            await unitOfWork.CompleteAsync();

            var newCourse = courseRepository.GetAll().FirstOrDefault();

            Assert.Equal(testCourseName, newCourse.Name);
            Assert.True(newCourse?.Id > 0);
        }

        [Fact]
        public async Task UpdateCourse_ValidCourse_ReturnsOkAsync()
        {
            var testCourseName = "test-course";
            
            var course = new Course { Name = testCourseName };

            courseRepository.AddCourse(course);
            await unitOfWork.CompleteAsync();

            var addedCourse = courseRepository.GetAll().FirstOrDefault();

            var updateCourse = addedCourse;
            var testUpdateCourseName = "test-updated-course";
            updateCourse.Name = testUpdateCourseName;

            courseRepository.UpdateCourse(updateCourse);
            await unitOfWork.CompleteAsync();

            var updatedCourse = courseRepository.GetAll().FirstOrDefault();

            Assert.NotNull(updatedCourse);
            Assert.Equal(testUpdateCourseName, updatedCourse.Name);
            Assert.Equal(addedCourse.Id, updatedCourse.Id);
        }

        [Fact]
        public void GetCourse_CourseNotFound_ThrowsCourseNotFoundException()
        {
            
            var some_test_Id = 121232;

            Assert.ThrowsAsync<CourseNotFoundException>(async () => await courseRepository.GetCourse(some_test_Id));
        }

        [Fact]
        public async Task GetCourse_ValidCourse_ReturnsOkAsync()
        {
            var testCourseName = "test-course";
           
            var course = new Course { Name = testCourseName };

            courseRepository.AddCourse(course);
            await unitOfWork.CompleteAsync();

            var addedCourse = await courseRepository.GetCourse(course.Id);

            Assert.NotNull(addedCourse);
            Assert.Equal(course.Id, addedCourse.Id);
            Assert.Equal(course.Name, addedCourse.Name);
        }

    }
}
