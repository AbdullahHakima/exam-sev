using examservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace examservice.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<ModuleQuestion>()
            .HasKey(mq => new { mq.ModuleId, mq.QuestionId });

        modelBuilder.Entity<ModuleQuestion>()
            .HasOne(mq => mq.Module)
            .WithMany(m => m.ModuleQuestions)
            .HasForeignKey(mq => mq.ModuleId);

        modelBuilder.Entity<ModuleQuestion>()
            .HasOne(mq => mq.Question)
            .WithMany(q => q.ModuleQuestions)
            .HasForeignKey(mq => mq.QuestionId);
        modelBuilder.Entity<StudentQuizzes>()
            .HasKey(e => new { e.StudentId, e.QuizId });

        modelBuilder.Entity<InstructorCourses>()
            .HasKey(ic => new { ic.InstructorId, ic.CourseId });
        modelBuilder.Entity<InstructorCourses>()
            .HasOne(ic => ic.instructor)
            .WithMany(i => i.InstructorCourses)
            .HasForeignKey(ic => ic.InstructorId);
        modelBuilder.Entity<InstructorCourses>()
            .HasOne(ic => ic.course)
            .WithMany(c => c.InstructorCourses)
            .HasForeignKey(ic => ic.CourseId);

        modelBuilder.Entity<StudentCourses>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });
        modelBuilder.Entity<StudentCourses>()
            .HasOne(sc => sc.student)
            .WithMany(s => s.StudentCourses)
            .HasForeignKey(sc => sc.StudentId);
        modelBuilder.Entity<StudentCourses>()
            .HasOne(sc => sc.course)
            .WithMany(c => c.studentCourses)
            .HasForeignKey(sc => sc.CourseId);


    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Quiz> Quizs { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Course> courses { get; set; }
    public DbSet<Submission> Submissions { get; set; }


}
