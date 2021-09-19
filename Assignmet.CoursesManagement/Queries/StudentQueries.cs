using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Queries
{
    public class StudentQueries : IStudentQueries
    {
        private readonly IStudentRepository studentRepository;

        public StudentQueries(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        }
        public List<Student> GetAll()
        {
            return studentRepository.GetAll().ToList();
        }

        public List<SelectedStudentData> GetSelectedStudents()
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
