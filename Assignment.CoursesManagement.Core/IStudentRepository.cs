using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.CoursesManagement.Core
{
    public interface IStudentRepository
    {
        ICollection<Student> GetAll();
        Task<Student> Find(int id);
    }
}
