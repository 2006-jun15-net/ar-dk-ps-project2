﻿using Microsoft.EntityFrameworkCore;

namespace ClassRegistration.DataAccess.Entity
{
    public partial class Course_registration_dbContext : DbContext
    {
        public Course_registration_dbContext ()
        {
        }

        public Course_registration_dbContext (DbContextOptions<Course_registration_dbContext> options)
            : base (options)
        {
        }

        public virtual DbSet<College> College { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Instructor> Instructor { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Semester> Semester { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentType> StudentType { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<College> (entity =>
             {
                 entity.Property (e => e.CollegeId).HasColumnName ("CollegeID");

                 entity.Property (e => e.Address)
                     .IsRequired ()
                     .HasColumnName ("address")
                     .HasMaxLength (20);

                 entity.Property (e => e.CollegeName)
                     .IsRequired ()
                     .HasColumnName ("College_name")
                     .HasMaxLength (20);
             });

            modelBuilder.Entity<Course> (entity =>
             {
                 entity.Property (e => e.CourseId).HasColumnName ("CourseID");

                 entity.Property (e => e.CourseName)
                     .IsRequired ()
                     .HasMaxLength (20);

                 entity.Property (e => e.Credits).HasDefaultValueSql ("((4))");

                 entity.Property (e => e.DeptId).HasColumnName ("DeptID");

                 entity.Property (e => e.Fees).HasColumnType ("decimal(6, 2)");

                 entity.HasOne (d => d.Dept)
                     .WithMany (p => p.Course)
                     .HasForeignKey (d => d.DeptId)
                     .HasConstraintName ("FK__Course__Fees__5629CD9C");
             });

            modelBuilder.Entity<Department> (entity =>
             {
                 entity.HasKey (e => e.DeptId)
                     .HasName ("PK__Departme__0148818ED6125360");

                 entity.Property (e => e.DeptId).HasColumnName ("DeptID");

                 entity.Property (e => e.CollegeId).HasColumnName ("CollegeID");

                 entity.Property (e => e.DeptName)
                     .IsRequired ()
                     .HasColumnName ("Dept_name")
                     .HasMaxLength (15);

                 entity.HasOne (d => d.College)
                     .WithMany (p => p.Department)
                     .HasForeignKey (d => d.CollegeId)
                     .HasConstraintName ("FK__Departmen__Colle__4D94879B");
             });

            modelBuilder.Entity<Enrollment> (entity =>
             {
                 entity.Property (e => e.EnrollmentId).HasColumnName ("EnrollmentID");

                 entity.Property (e => e.SectId).HasColumnName ("SectID");

                 entity.Property (e => e.StudentId).HasColumnName ("StudentID");

                 entity.HasOne (d => d.Sect)
                     .WithMany (p => p.Enrollment)
                     .HasForeignKey (d => d.SectId)
                     .HasConstraintName ("FK__Enrollmen__SectI__6A30C649");

                 entity.HasOne (d => d.Student)
                     .WithMany (p => p.Enrollment)
                     .HasForeignKey (d => d.StudentId)
                     .HasConstraintName ("FK__Enrollmen__Stude__693CA210");
             });

            modelBuilder.Entity<Instructor> (entity =>
             {
                 entity.Property (e => e.InstructorId).HasColumnName ("InstructorID");

                 entity.Property (e => e.DeptId).HasColumnName ("DeptID");

                 entity.Property (e => e.FirstName)
                     .IsRequired ()
                     .HasColumnName ("firstName")
                     .HasMaxLength (20);

                 entity.Property (e => e.LastName)
                     .IsRequired ()
                     .HasColumnName ("lastName")
                     .HasMaxLength (20);

                 entity.HasOne (d => d.Dept)
                     .WithMany (p => p.Instructor)
                     .HasForeignKey (d => d.DeptId)
                     .HasConstraintName ("FK__Instructo__DeptI__5070F446");
             });

            modelBuilder.Entity<Reviews> (entity =>
             {
                 entity.HasKey (e => e.ReviewId);

                 entity.Property (e => e.CourseId).HasColumnName ("CourseID");

                 entity.Property (e => e.Date).HasColumnType ("date");

                 entity.Property (e => e.StudentId).HasColumnName ("StudentID");

                 entity.Property (e => e.Text)
                     .IsRequired ()
                     .HasMaxLength (2000);

                 entity.HasOne (d => d.Course)
                     .WithMany (p => p.Reviews)
                     .HasForeignKey (d => d.CourseId);

                 entity.HasOne (d => d.Student)
                     .WithMany (p => p.Reviews)
                     .HasForeignKey (d => d.StudentId);
             });

            modelBuilder.Entity<Section> (entity =>
             {
                 entity.HasKey (e => e.SectId)
                     .HasName ("PK__Section__8D0832FF54591CC6");

                 entity.Property (e => e.SectId).HasColumnName ("SectID");

                 entity.Property (e => e.CourseId).HasColumnName ("CourseID");

                 entity.Property (e => e.EndTime).HasColumnName ("end_time");

                 entity.Property (e => e.InstructorId).HasColumnName ("InstructorID");

                 entity.Property (e => e.StartTime).HasColumnName ("start_time");

                 entity.Property (e => e.Term)
                     .IsRequired ()
                     .HasMaxLength (15);

                 entity.HasOne (d => d.Course)
                     .WithMany (p => p.Section)
                     .HasForeignKey (d => d.CourseId)
                     .HasConstraintName ("FK__Section__CourseI__6477ECF3");

                 entity.HasOne (d => d.Instructor)
                     .WithMany (p => p.Section)
                     .HasForeignKey (d => d.InstructorId)
                     .OnDelete (DeleteBehavior.ClientSetNull)
                     .HasConstraintName ("FK__Section__Instruc__656C112C");

                 entity.HasOne (d => d.TermNavigation)
                     .WithMany (p => p.Section)
                     .HasForeignKey (d => d.Term)
                     .HasConstraintName ("FK__Section__Term__66603565");
             });

            modelBuilder.Entity<Semester> (entity =>
             {
                 entity.HasKey (e => e.Term)
                     .HasName ("PK__Semester__8F79FD68ED6B5E93");

                 entity.Property (e => e.Term).HasMaxLength (15);

                 entity.Property (e => e.BeginDate)
                     .HasColumnName ("begin_date")
                     .HasColumnType ("date");

                 entity.Property (e => e.EndDate)
                     .HasColumnName ("end_date")
                     .HasColumnType ("date");
             });

            modelBuilder.Entity<Student> (entity =>
             {
                 entity.Property (e => e.StudentId).HasColumnName ("StudentID");

                 entity.Property (e => e.DateOfBirth)
                     .HasColumnName ("Date_of_birth")
                     .HasColumnType ("date");

                 entity.Property (e => e.DeptId).HasColumnName ("DeptID");

                 entity.Property (e => e.Email)
                     .IsRequired ()
                     .HasColumnName ("email")
                     .HasMaxLength (30);

                 entity.Property (e => e.FirstName)
                     .IsRequired ()
                     .HasMaxLength (20);

                 entity.Property (e => e.LastName)
                     .IsRequired ()
                     .HasMaxLength (20);

                 entity.Property (e => e.Phone).HasMaxLength (12);

                 entity.Property (e => e.ResidentId)
                     .HasColumnName ("Resident_id")
                     .HasMaxLength (50);

                 entity.HasOne (d => d.Dept)
                     .WithMany (p => p.Student)
                     .HasForeignKey (d => d.DeptId)
                     .OnDelete (DeleteBehavior.ClientSetNull)
                     .HasConstraintName ("Fk_Student_Department");

                 entity.HasOne (d => d.Resident)
                     .WithMany (p => p.Student)
                     .HasForeignKey (d => d.ResidentId)
                     .HasConstraintName ("FK__Student__Residen__7D439ABD");
             });

            modelBuilder.Entity<StudentType> (entity =>
             {
                 entity.HasKey (e => e.ResidentId)
                     .HasName ("PK__Student___F3015361E0F4B8D2");

                 entity.ToTable ("Student_type");

                 entity.Property (e => e.ResidentId)
                     .HasColumnName ("Resident_id")
                     .HasMaxLength (50);

                 entity.Property (e => e.Discount).HasColumnType ("decimal(6, 2)");
             });

            OnModelCreatingPartial (modelBuilder);
        }

        partial void OnModelCreatingPartial (ModelBuilder modelBuilder);
    }
}
