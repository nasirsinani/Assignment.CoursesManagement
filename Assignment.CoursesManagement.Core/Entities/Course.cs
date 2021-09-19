using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.CoursesManagement.Core
{
    [Table("Courses")]
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public CourseType CourseType { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<CourseStudent> Students { get; set; }

        public Course()
        {
            Students = new Collection<CourseStudent>();
        }
    }
}
