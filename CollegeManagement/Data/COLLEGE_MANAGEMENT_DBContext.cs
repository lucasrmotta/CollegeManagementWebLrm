using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CollegeManagement.Models;

namespace CollegeManagement.Data
{
    public partial class COLLEGE_MANAGEMENT_DBContext : DbContext
    {
        public COLLEGE_MANAGEMENT_DBContext()
        {
        }

        public COLLEGE_MANAGEMENT_DBContext(DbContextOptions<COLLEGE_MANAGEMENT_DBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentGrade> StudentGrades { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
