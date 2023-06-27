using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class LescogidoProgramacionNcapasGjContext : DbContext
{
    public LescogidoProgramacionNcapasGjContext()
    {
    }

    public LescogidoProgramacionNcapasGjContext(DbContextOptions<LescogidoProgramacionNcapasGjContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    public virtual DbSet<Plantel> Plantels { get; set; }

    public virtual DbSet<Semestre> Semestres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= LEscogidoProgramacionNCapasGJ; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo).HasName("PK__Grupo__303F6FD97B269AF0");

            entity.ToTable("Grupo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPlantelNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.IdPlantel)
                .HasConstraintName("FK__Grupo__IdPlantel__29572725");
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PK__Materia__EC1746701E14FF71");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSemestreNavigation).WithMany(p => p.Materia)
                .HasForeignKey(d => d.IdSemestre)
                .HasConstraintName("FK__Materia__IdSemes__15502E78");
        });

        modelBuilder.Entity<Plantel>(entity =>
        {
            entity.HasKey(e => e.IdPlantel).HasName("PK__Plantel__485FDCFEF3BAE9CE");

            entity.ToTable("Plantel");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Semestre>(entity =>
        {
            entity.HasKey(e => e.IdSemestre).HasName("PK__Semestre__BD1FD7F87097BA10");

            entity.ToTable("Semestre");

            entity.Property(e => e.IdSemestre).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
