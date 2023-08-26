using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyPhillReminder_APIIssak_melany.Models
{
    public partial class MyPhillReminderBDContext : DbContext
    {
        public MyPhillReminderBDContext()
        {
        }

        public MyPhillReminderBDContext(DbContextOptions<MyPhillReminderBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PillReminder> PillReminders { get; set; } = null!;
        public virtual DbSet<ReminderCategory> ReminderCategories { get; set; } = null!;
        public virtual DbSet<ReminderStep> ReminderSteps { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //optionsBuilder.UseSqlServer("SERVER=.; DATABASE=MyPhillReminderBD; INTEGRATED SECURITY=FALSE; USER ID=MyPhillReminder; PASSWORD=1234567");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PillReminder>(entity =>
            {
                entity.HasKey(e => e.ReminderId)
                    .HasName("PK__PillRemi__01A830A775FBDBAD");

                entity.ToTable("PillReminder");

                entity.Property(e => e.ReminderId).HasColumnName("ReminderID");

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ReminderCategoryNavigation)
                    .WithMany(p => p.PillReminders)
                    .HasForeignKey(d => d.ReminderCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPillRemind646282");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PillReminders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPillRemind503448");

                entity.HasMany(d => d.ReminderStepReminderSteps)
                    .WithMany(p => p.PillReminderReminders)
                    .UsingEntity<Dictionary<string, object>>(
                        "ReminderDetail",
                        l => l.HasOne<ReminderStep>().WithMany().HasForeignKey("ReminderStepReminderStepId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKReminderDe354116"),
                        r => r.HasOne<PillReminder>().WithMany().HasForeignKey("PillReminderReminderId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKReminderDe220075"),
                        j =>
                        {
                            j.HasKey("PillReminderReminderId", "ReminderStepReminderStepId").HasName("PK__Reminder__3F95E8EAD9458BF7");

                            j.ToTable("ReminderDetails");

                            j.IndexerProperty<int>("PillReminderReminderId").HasColumnName("PillReminderReminderID");

                            j.IndexerProperty<int>("ReminderStepReminderStepId").HasColumnName("ReminderStepReminderStepID");
                        });
            });

            modelBuilder.Entity<ReminderCategory>(entity =>
            {
                entity.HasKey(e => e.ReminderCategory1)
                    .HasName("PK__Reminder__36C0394349AFF788");

                entity.ToTable("ReminderCategory");

                entity.Property(e => e.ReminderCategory1).HasColumnName("ReminderCategory");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReminderCategories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReminderCa325048");
            });

            modelBuilder.Entity<ReminderStep>(entity =>
            {
                entity.ToTable("ReminderStep");

                entity.Property(e => e.ReminderStepId).HasColumnName("ReminderStepID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.Step)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReminderSteps)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReminderSt457491");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.Property(e => e.Address)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.BackUpEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IDRoleUser).HasColumnName("IDRoleUser");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserRoleId)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser935809");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId)
                    .HasName("PK__UserRole__94A4711B48967B9B");

                entity.ToTable("UserRole");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleId");

                entity.Property(e => e.Description)
                    .HasMaxLength(1500)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
