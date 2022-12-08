using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IQBInterview.Models
{
    public class ExamResult : EnBaseEntity
    {

        [Required]
        [Index("Exam",IsUnique = true, Order = 1)]
        public int StudentId { get; set; }
        public Student Students { get; set; }
        [Required]
        [Index("Exam", IsUnique = true, Order = 2)]
        public int CourseId { get; set; }
        public Course Courses { get; set; }
        public int Score { get; set; }
    }
}