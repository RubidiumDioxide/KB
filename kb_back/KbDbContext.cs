using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kb_back;

public partial class KbDbContext : DbContext
{
    public KbDbContext()
    {
    }

    public KbDbContext(DbContextOptions<KbDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircraft { get; set; }

    public virtual DbSet<Airframe> Airframes { get; set; }

    public virtual DbSet<Armament> Armaments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Engine> Engines { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WIN-4E7JKGBR3SV\\SQLEXPRESS;Database=kb_DB;TrustServerCertificate=True;Encrypt=False;user id=sa;password=char<3;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.AircraftName).HasName("PK_AircraftName");

            entity.Property(e => e.AircraftName).HasMaxLength(100);
            entity.Property(e => e.AircraftType).HasMaxLength(100);
            entity.Property(e => e.Engine).HasMaxLength(100);

            entity.HasOne(d => d.EngineNavigation).WithMany(p => p.Aircraft)
                .HasForeignKey(d => d.Engine)
                .HasConstraintName("FK_Engine_Aircraft");

            entity.HasMany(d => d.Armaments).WithMany(p => p.Aircraft)
                .UsingEntity<Dictionary<string, object>>(
                    "AircraftArmament",
                    r => r.HasOne<Armament>().WithMany()
                        .HasForeignKey("Armament")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Aircraft_Armament_Armament"),
                    l => l.HasOne<Aircraft>().WithMany()
                        .HasForeignKey("Aircraft")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Aircraft_Armament_Aircraft"),
                    j =>
                    {
                        j.HasKey("Aircraft", "Armament");
                        j.ToTable("Aircraft_Armament");
                        j.IndexerProperty<string>("Aircraft").HasMaxLength(100);
                        j.IndexerProperty<string>("Armament").HasMaxLength(100);
                    });
        });

        modelBuilder.Entity<Airframe>(entity =>
        {
            entity.HasKey(e => e.Aircraft).HasName("PK_Aircraft");

            entity.ToTable("Airframe");

            entity.Property(e => e.Aircraft).HasMaxLength(100);
            entity.Property(e => e.WingProfile).HasMaxLength(100);

            entity.HasOne(d => d.AircraftNavigation).WithOne(p => p.Airframe)
                .HasForeignKey<Airframe>(d => d.Aircraft)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Aircraft_Airframe");
        });

        modelBuilder.Entity<Armament>(entity =>
        {
            entity.HasKey(e => e.ArmamentName).HasName("PK_ArmamentName");

            entity.ToTable("Armament");

            entity.Property(e => e.ArmamentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentName).HasName("PK_DepartmentName");

            entity.ToTable("Department");

            entity.HasIndex(e => e.Adress, "Adress_unique").IsUnique();

            entity.Property(e => e.DepartmentName).HasMaxLength(200);
            entity.Property(e => e.Adress).HasMaxLength(200);

            entity.HasOne(d => d.DirectorNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.Director)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_EmployeeID");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Department).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("money");
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.CurrentProjectNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CurrentProject)
                .HasConstraintName("FK_Project_Employee");

            entity.HasOne(d => d.DepartmentNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Department)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Department_Employee");
        });

        modelBuilder.Entity<Engine>(entity =>
        {
            entity.HasKey(e => e.EngineName);

            entity.ToTable("Engine");

            entity.Property(e => e.EngineName).HasMaxLength(100);
            entity.Property(e => e.EngineType).HasMaxLength(100);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK_ProjectID");

            entity.ToTable("Project");

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.Aircraft).HasMaxLength(100);
            entity.Property(e => e.Department).HasMaxLength(200);
            entity.Property(e => e.ProjectName).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.AircraftNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Aircraft)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Aircraft_Project");

            entity.HasOne(d => d.ChiefDesignerNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ChiefDesigner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Project");

            entity.HasOne(d => d.DepartmentNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Department)
                .HasConstraintName("FK_Department_Project");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
