using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement
{
    public partial class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_TEACHER")]
        public int IdTeacher { get; set; }
        [Column("NAME")]
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("BIRTHDAY", TypeName = "date")]
        public DateTime Birthday { get; set; }
        [Column("SALARY", TypeName = "money")]
        public decimal Salary { get; set; }

        [InverseProperty(nameof(Subject.IdTeacherNavigation))]
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
