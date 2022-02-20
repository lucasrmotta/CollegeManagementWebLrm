using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace CollegeManagement
{
    [Keyless]
    [Table("StudentsInSubjects")]
    public partial class StudentsInSubjects
    {

        [Column("ID_SUBJECT")]
        public int IdSubject { get; set; }
     
        [Column("ID_STUDENT_REGISTRATION_NUMBER")]
        public int IdStudentRegistrationNumber { get; set; }

        [ForeignKey(nameof(IdSubject))]
        public virtual Subject IdSubjectNavigation { get; set; } = null!;

        [ForeignKey(nameof(IdStudentRegistrationNumber))]
        public virtual Student IdStudentRegistrationNumberNavigation { get; set; } = null!;

    }
}
