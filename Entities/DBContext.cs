﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Entities;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProblemType> ProblemTypes { get; set; }

    public virtual DbSet<RequestList> RequestLists { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;User ID=user;Password=root;Database=exam_db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProblemType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("problem_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<RequestList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("request_list");

            entity.HasIndex(e => e.ProblemType, "request_list_problem_types_id_fk");

            entity.HasIndex(e => e.Status, "request_list_request_status_id_fk");

            entity.HasIndex(e => e.WorkerId, "request_list_users_id_fk");

            entity.HasIndex(e => e.ClientId, "request_list_users_id_fk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DeviceName)
                .HasColumnType("text")
                .HasColumnName("device_name");
            entity.Property(e => e.Number)
                .HasColumnType("text")
                .HasColumnName("number");
            entity.Property(e => e.ProblemType).HasColumnName("problem_type");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.WorkerId).HasColumnName("worker_id");

            entity.HasOne(d => d.Client).WithMany(p => p.RequestListClients)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("request_list_users_id_fk_2");

            entity.HasOne(d => d.ProblemTypeNavigation).WithMany(p => p.RequestLists)
                .HasForeignKey(d => d.ProblemType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("request_list_problem_types_id_fk");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.RequestLists)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("request_list_request_status_id_fk");

            entity.HasOne(d => d.Worker).WithMany(p => p.RequestListWorkers)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("request_list_users_id_fk");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("request_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Role, "users_roles_id_fk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .HasColumnType("text")
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_roles_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
