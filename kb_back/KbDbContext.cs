using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace kb_back;

public partial class KbDbContext : DbContext
{
    private string connectionString;

    public KbDbContext(string _connectionString)
    {
        connectionString = _connectionString;
    }

    public KbDbContext(DbContextOptions<KbDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircraft { get; set; }

    public virtual DbSet<AircraftArmament> AircraftArmaments { get; set; }

    public virtual DbSet<Airframe> Airframes { get; set; }

    public virtual DbSet<Armament> Armaments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Engine> Engines { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK_Name");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Crew).HasDefaultValue((byte)1);
            entity.Property(e => e.Engine).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(200);

            entity.HasOne(d => d.EngineNavigation).WithMany(p => p.Aircraft)
                .HasForeignKey(d => d.Engine)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Engine_Aircraft");
        });

        modelBuilder.Entity<AircraftArmament>(entity =>
        {
            entity.HasKey(e => new { e.Aircraft, e.Armament });

            entity.ToTable("Aircraft_Armament");

            entity.Property(e => e.Aircraft).HasMaxLength(100);
            entity.Property(e => e.Armament).HasMaxLength(100);

            entity.HasOne(d => d.AircraftNavigation).WithMany(p => p.AircraftArmaments)
                .HasForeignKey(d => d.Aircraft)
                .HasConstraintName("FK_Aircraft_Armament_Aircraft");

            entity.HasOne(d => d.ArmamentNavigation).WithMany(p => p.AircraftArmaments)
                .HasForeignKey(d => d.Armament)
                .HasConstraintName("FK_Aircraft_Armament_Armament");
        });

        modelBuilder.Entity<Airframe>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK_Aircraft");

            entity.ToTable("Airframe");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.WingProfile)
                .HasMaxLength(100)
                .HasColumnName("Wing_profile");

            entity.HasOne(d => d.NameNavigation).WithOne(p => p.Airframe)
                .HasForeignKey<Airframe>(d => d.Name)
                .HasConstraintName("FK_Aircraft_Airframe");
        });

        modelBuilder.Entity<Armament>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK_ArmamentName");

            entity.ToTable("Armament");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.FiringRate).HasColumnName("Firing_rate");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK_DepartmentName");

            entity.ToTable("Department");

            entity.HasIndex(e => e.Adress, "UQ__Departme__08F62FE54897E6F5").IsUnique();

            entity.HasIndex(e => e.Director, "unique director").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Adress).HasMaxLength(200);

            entity.HasOne(d => d.DirectorNavigation).WithOne(p => p.DepartmentNavigation)
                .HasForeignKey<Department>(d => d.Director)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmployeeID");

            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_of_birth");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_name");
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("money");
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.YearsOfExperience).HasColumnName("Years_of_experience");

            entity.HasOne(d => d.Department1).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Department)
                .HasConstraintName("FK_Department_Employee");
        });

        modelBuilder.Entity<Engine>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("Engine");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(200);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectID");

            entity.ToTable("Project");

            entity.HasIndex(e => e.Name, "UQ__Project__737584F65A17535F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Aircraft).HasMaxLength(100);
            entity.Property(e => e.ChiefDesigner).HasColumnName("Chief_designer");
            entity.Property(e => e.DateBegan).HasColumnName("Date_began");
            entity.Property(e => e.DateFinished).HasColumnName("Date_finished");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("начат");

            entity.HasOne(d => d.AircraftNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Aircraft)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Aircraft_Project");

            entity.HasOne(d => d.ChiefDesignerNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ChiefDesigner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Project");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
