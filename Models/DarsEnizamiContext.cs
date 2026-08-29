using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Darsenizami.Models;

public partial class DarsEnizamiContext : DbContext
{
    public DarsEnizamiContext()
    {
    }

    public DarsEnizamiContext(DbContextOptions<DarsEnizamiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<AdmissionForms> AdmissionForms { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FacultySubject> FacultySubjects { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<YearLevel> YearLevels { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__activity__9E2397E002C1720C");

            entity.ToTable("activity_logs");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.Action)
                .HasMaxLength(500)
                .HasColumnName("action");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__activity___user___72C60C4A");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admins__43AA41415FDC092C");

            entity.ToTable("admins");

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .HasColumnName("contact_no");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("designation");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__admins__user_id__44FF419A");
        });

        modelBuilder.Entity<Admission>(entity =>
        {
            entity.HasKey(e => e.AdmissionId).HasName("PK__admissio__3D9F8C72621A555F");

            entity.ToTable("admissions");

            entity.Property(e => e.AdmissionId).HasColumnName("admission_id");
            entity.Property(e => e.AdmissionDate)
                .HasColumnType("datetime")
                .HasColumnName("admission_date");
            entity.Property(e => e.FormId).HasColumnName("form_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasColumnName("remarks");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Form).WithMany(p => p.Admissions)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__admission__form___59FA5E80");

            entity.HasOne(d => d.Student).WithMany(p => p.Admissions)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__admission__stude__59063A47");
        });

        modelBuilder.Entity<AdmissionForms>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__admissio__190E16C92D70396B");

            entity.ToTable("admission_forms");

            entity.Property(e => e.FormId).HasColumnName("form_id");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .HasColumnName("address");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .HasColumnName("contact");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Documents)
                .HasMaxLength(500)
                .HasColumnName("documents");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.PreviousInstitute)
                .HasMaxLength(200)
                .HasColumnName("previous_institute");
            entity.Property(e => e.SubmissionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("submission_date");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__attendan__20D6A96825941903");

            entity.ToTable("attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("attendance_id");
            entity.Property(e => e.AttendanceDate).HasColumnName("attendance_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__attendanc__stude__60A75C0F");

            entity.HasOne(d => d.Subject).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__attendanc__subje__619B8048");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__books__490D1AE12B5A2AD0");

            entity.ToTable("books");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Author)
                .HasMaxLength(150)
                .HasColumnName("author");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.PdfLink)
                .HasMaxLength(400)
                .HasColumnName("pdf_link");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("title");
            entity.Property(e => e.YearLevel).HasColumnName("year_level");

            entity.HasOne(d => d.Subject).WithMany(p => p.Books)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__books__subject_i__4222D4EF");

            entity.HasOne(d => d.YearLevelNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.YearLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__books__year_leve__412EB0B6");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("PK__faculty__7B00413CF6C384AF");

            entity.ToTable("faculty");

            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .HasColumnName("contact_no");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("designation");
            entity.Property(e => e.ExperienceYears).HasColumnName("experience_years");
            entity.Property(e => e.Qualification)
                .HasMaxLength(200)
                .HasColumnName("qualification");
            entity.Property(e => e.Specialization)
                .HasMaxLength(200)
                .HasColumnName("specialization");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__faculty__user_id__4D94879B");
        });

        modelBuilder.Entity<FacultySubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__faculty___3213E83FD5FF57E1");

            entity.ToTable("faculty_subjects");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassYear).HasColumnName("class_year");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.ClassYearNavigation).WithMany(p => p.FacultySubjects)
                .HasForeignKey(d => d.ClassYear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__faculty_s__class__52593CB8");

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultySubjects)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__faculty_s__facul__5070F446");

            entity.HasOne(d => d.Subject).WithMany(p => p.FacultySubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__faculty_s__subje__5165187F");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__messages__0BBF6EE6A808D8AE");

            entity.ToTable("messages");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .HasColumnName("content");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__receiv__6EF57B66");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__sender__6E01572D");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__notifica__E059842F77F02CF9");

            entity.ToTable("notifications");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.DateSent)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_sent");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .HasColumnName("message");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("unread")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__notificat__user___6A30C649");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__payments__ED1FC9EA53EFB02A");

            entity.ToTable("payments");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Student).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__payments__studen__5DCAEF64");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__results__AFB3C316AFC10FD2");

            entity.ToTable("results");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.Grade)
                .HasMaxLength(10)
                .HasColumnName("grade");
            entity.Property(e => e.MarksObtained).HasColumnName("marks_obtained");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.Term)
                .HasMaxLength(50)
                .HasColumnName("term");
            entity.Property(e => e.TotalMarks).HasColumnName("total_marks");

            entity.HasOne(d => d.Student).WithMany(p => p.Results)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__results__student__6477ECF3");

            entity.HasOne(d => d.Subject).WithMany(p => p.Results)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__results__subject__656C112C");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.SettingId).HasName("PK__settings__256E1E32482B5488");

            entity.ToTable("settings");

            entity.Property(e => e.SettingId).HasColumnName("setting_id");
            entity.Property(e => e.Key)
                .HasMaxLength(100)
                .HasColumnName("key");
            entity.Property(e => e.Value)
                .HasMaxLength(500)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__students__2A33069AC658609B");

            entity.ToTable("students");

            entity.HasIndex(e => e.RollNo, "UQ__students__9560EEE09F4C6577").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .HasColumnName("address");
            entity.Property(e => e.JoiningDate).HasColumnName("joining_date");
            entity.Property(e => e.RollNo)
                .HasMaxLength(50)
                .HasColumnName("roll_no");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValue("enrolled")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.YearLevel).HasColumnName("year_level");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__students__user_i__49C3F6B7");

            entity.HasOne(d => d.YearLevelNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.YearLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__students__year_l__4AB81AF0");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__subjects__5004F6601ED3E265");

            entity.ToTable("subjects");

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(150)
                .HasColumnName("subject_name");
            entity.Property(e => e.YearId).HasColumnName("year_id");

            entity.HasOne(d => d.Year).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.YearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__subjects__year_i__3E52440B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F0DAAC943");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616400E235E8").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(30)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValue("active")
                .HasColumnName("status");
        });

        modelBuilder.Entity<YearLevel>(entity =>
        {
            entity.HasKey(e => e.YearId).HasName("PK__year_lev__B2A06B629E96C541");

            entity.ToTable("year_levels");

            entity.Property(e => e.YearId).HasColumnName("year_id");
            entity.Property(e => e.YearName)
                .HasMaxLength(50)
                .HasColumnName("year_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
