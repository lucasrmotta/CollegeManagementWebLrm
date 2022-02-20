using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement
{
    [Table("COURSES")]
    public partial class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
            Subjects = new HashSet<Subject>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_COURSE")]
        public int IdCourse { get; set; }
        [Column("DS_COURSE")]
        [StringLength(255)]
        [Unicode(false)]
        public string DsCourse { get; set; } = null!;

        [InverseProperty(nameof(Student.IdCourseNavigation))]
        public virtual ICollection<Student> Students { get; set; }

        [InverseProperty(nameof(Subject.IdCourseNavigation))]
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
