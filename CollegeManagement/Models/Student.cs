using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement
{
    [Table("STUDENTS")]
    public partial class Student
    {
        [Key]
        [Column("ID_STUDENT_REGISTRATION_NUMBER")]
        public int IdStudentRegistrationNumber { get; set; }
        [Column("NAME")]
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("BIRTHDAY", TypeName = "date")]
        public DateTime Birthday { get; set; }
        [Column("ID_COURSE")]
        public int IdCourse { get; set; }

        [ForeignKey(nameof(IdCourse))]
        [InverseProperty(nameof(Course.Students))]
        public virtual Course IdCourseNavigation { get; set; } = null!;
    }
}
