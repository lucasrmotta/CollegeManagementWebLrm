using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace CollegeManagement
{
    [Table("SUBJECTS")]
    public partial class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_SUBJECT")]
        public int IdSubject { get; set; }

        [Column("DS_SUBJECT")]
        [StringLength(255)]
        [Unicode(false)]
        public string DsSubject { get; set; } = null!;

        [Column("ID_TEACHER")]
        public int IdTeacher { get; set; }

        [Column("ID_COURSE")]
        public int IdCourse { get; set; }

        [ForeignKey(nameof(IdCourse))]
        [InverseProperty(nameof(Course.Subjects))]
        public virtual Course IdCourseNavigation { get; set; } = null!;

        [ForeignKey(nameof(IdTeacher))]
        [InverseProperty(nameof(Teacher.Subjects))]
        public virtual Teacher IdTeacherNavigation { get; set; } = null!;
    }
}
