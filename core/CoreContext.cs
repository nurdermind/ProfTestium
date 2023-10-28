using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProfTestium_TestService;

public partial class CoreContext : DbContext
{
    public CoreContext()
    {
    }

    public CoreContext(DbContextOptions<CoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnswerVariant> AnswerVariants { get; set; }

    public virtual DbSet<BankOfQuestion> BankOfQuestions { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<OpenQuestionAnswer> OpenQuestionAnswers { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionType> QuestionTypes { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<UserAnswer> UserAnswers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=core;Username=postgres;Password=Doomz999%");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnswerVariant>(entity =>
        {
            entity.HasKey(e => e.VariantId).HasName("UniqueAnswer");

            entity.ToTable("AnswerVariant");

            entity.Property(e => e.VariantId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("VariantID");
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.VariantText)
                .HasMaxLength(250)
                .IsFixedLength();

            entity.HasOne(d => d.Question).WithMany(p => p.AnswerVariants)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("QuestionAnswer");
        });

        modelBuilder.Entity<BankOfQuestion>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("UniqueBank");

            entity.Property(e => e.PositionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("PositionID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.TestId).HasColumnName("TestID");

            entity.HasOne(d => d.Question).WithMany(p => p.BankOfQuestions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BankQuestion");

            entity.HasOne(d => d.Test).WithMany(p => p.BankOfQuestions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BankTest");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.MarkId).HasName("Mark_pkey");

            entity.ToTable("Mark");

            entity.HasIndex(e => new { e.MarkId, e.Mark1, e.MinimumLimit }, "UniqueMark").IsUnique();

            entity.Property(e => e.MarkId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("MarkID");
            entity.Property(e => e.Mark1)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("Mark");
        });

        modelBuilder.Entity<OpenQuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("UniqueFA");

            entity.ToTable("OpenQuestionAnswer");

            entity.Property(e => e.AnswerId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("AnswerID");
            entity.Property(e => e.CorrectAnswerText)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UserAnswerText)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Question).WithMany(p => p.OpenQuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("QuestionAndFullAnswer");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.PictureId).HasName("Picture_pkey");

            entity.ToTable("Picture");

            entity.HasIndex(e => new { e.PictureId, e.PicturePath }, "UniquePic").IsUnique();

            entity.Property(e => e.PictureId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("PictureID");
            entity.Property(e => e.PicturePath)
                .HasMaxLength(300)
                .IsFixedLength();
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("UniqueQestion");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("QuestionID");
            entity.Property(e => e.PictureId).HasColumnName("PictureID");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");

            entity.HasOne(d => d.Picture).WithMany(p => p.Questions)
                .HasForeignKey(d => d.PictureId)
                .HasConstraintName("PictureInQuestion");

            entity.HasOne(d => d.QuestionType).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuestionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TypeOfQuestion");
        });

        modelBuilder.Entity<QuestionType>(entity =>
        {
            entity.HasKey(e => e.QuestionTypeId).HasName("QuestionType_pkey");

            entity.ToTable("QuestionType");

            entity.HasIndex(e => new { e.QuestionTypeId, e.QuestionTypeName }, "UniqueType").IsUnique();

            entity.Property(e => e.QuestionTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("QuestionTypeID");
            entity.Property(e => e.QuestionTypeName)
                .HasMaxLength(250)
                .IsFixedLength();
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("UniqueSession");

            entity.ToTable("Session");

            entity.Property(e => e.SessionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("SessionID");
            entity.Property(e => e.Duration).HasPrecision(6);
            entity.Property(e => e.FailureReason)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.IsSuccessful).HasColumnName("isSuccessful");
            entity.Property(e => e.MarkId).HasColumnName("MarkID");
            entity.Property(e => e.SessionDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.UserUd).HasColumnName("UserUD");

            entity.HasOne(d => d.Mark).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.MarkId)
                .HasConstraintName("SessionMark");

            entity.HasOne(d => d.Test).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SessionTest");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("UniqueTest");

            entity.ToTable("Test");

            entity.Property(e => e.TestId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("TestID");
            entity.Property(e => e.Testname)
                .HasMaxLength(250)
                .IsFixedLength();
        });

        modelBuilder.Entity<UserAnswer>(entity =>
        {
            entity.HasKey(e => e.UserAnswerId).HasName("UniquePositionAnswer");

            entity.ToTable("UserAnswer");

            entity.Property(e => e.UserAnswerId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("UserAnswerID");
            entity.Property(e => e.AnswerVariantId).HasColumnName("AnswerVariantID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.AnswerVariant).WithMany(p => p.UserAnswers)
                .HasForeignKey(d => d.AnswerVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserVariant");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
