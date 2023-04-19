using System;
using System.Collections.Generic;
using EnglishTrainer.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishTrainer.Resourses;

public partial class EnglishTrainerContext : DbContext
{
    public EnglishTrainerContext()
    {
    }

    public EnglishTrainerContext(DbContextOptions<EnglishTrainerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ResultHistory> ResultHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=EnglishTrainer;Username=postgres;Password=baza1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ResultHistory>(entity =>
        {
            entity.HasKey(e => e.ResultHistoryId).HasName("ResultHistory_pkey");

            entity.ToTable("ResultHistory");

            entity.Property(e => e.ResultHistoryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ResultHistoryID");
            entity.Property(e => e.Date).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.Time).HasDefaultValueSql("CURRENT_TIME");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ResultHistory_UserID_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "User_Login_key").IsUnique();

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("UserID");
            entity.Property(e => e.Login).HasMaxLength(15);
        });

        modelBuilder.Entity<Word>(entity =>
        {
            entity.HasKey(e => e.WordId).HasName("Word_pkey");

            entity.ToTable("Word");

            entity.Property(e => e.WordId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("WordID");
            entity.Property(e => e.EnglishVersion).HasColumnType("character varying");
            entity.Property(e => e.RussianVersion).HasColumnType("character varying");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Words)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Word_UserID_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
