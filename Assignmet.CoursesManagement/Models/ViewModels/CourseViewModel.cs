﻿using Assignment.CoursesManagement.Core;
using Assignmet.CoursesManagement.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignmet.CoursesManagement.Application.Models.ViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            Students = new List<SelectedStudentData>();
        }
        public int CourseId { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Course Type")]
        public CourseType CourseType { get; set; }
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        
        [ValidPriceAttribute]
        public decimal Price { get; set; }
        public List<SelectedStudentData> Students { get; set; }
    }
}
