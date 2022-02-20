using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace CollegeManagement
{
    [Table("STUDENT_GRADES")]
    public partial class StudentGrade
    {
        [Key]
        [Column("ID_STUDENT_SUBJECT")]
        public int IdStudentSubject { get; set; }

        [Column("ID_STUDENT_REGISTRATION_NUMBER")]
        public int IdStudentRegistrationNumber { get; set; }

        [Column("ID_SUBJECT")]
        public int IdSubject { get; set; }

        [Column("GRADE", TypeName = "decimal(18, 0)")]
        public decimal Grade { get; set; }

        [ForeignKey(nameof(IdStudentRegistrationNumber))]
        public virtual Student IdStudentRegistrationNumberNavigation { get; set; } = null!;

        [ForeignKey(nameof(IdSubject))]
        public virtual Subject IdSubjectNavigation { get; set; } = null!;
    }
}
