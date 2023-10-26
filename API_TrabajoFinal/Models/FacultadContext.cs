using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_TrabajoFinal.Models;

public partial class FacultadContext : DbContext
{
    public FacultadContext()
    {
    }

    public FacultadContext(DbContextOptions<FacultadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Examen> Examenes { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Planificacion> Planificacions { get; set; }

    public virtual DbSet<Profesor> Profesores { get; set; }

    public virtual DbSet<TipoDoc> TipoDocs { get; set; }

    public virtual DbSet<Titulo> Titulos { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.NroLegajoA).HasName("PK__Alumnos__D55113AE029154F4");

            entity.Property(e => e.NroLegajoA)
                .ValueGeneratedNever()
                .HasColumnName("nro_legajo_a");
            entity.Property(e => e.ApeNomb)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("ape_nomb");
            entity.Property(e => e.CodDoc)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_doc");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.EstCivil)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("est_civil");
            entity.Property(e => e.FecNac)
                .HasColumnType("datetime")
                .HasColumnName("fec_nac");
            entity.Property(e => e.NroDoc).HasColumnName("nro_doc");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("sexo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("telefono");

            entity.HasOne(d => d.CodDocNavigation).WithMany(p => p.Alumnos)
                .HasForeignKey(d => d.CodDoc)
                .HasConstraintName("FK_Alumnos_Tipo_Doc");
        });

        modelBuilder.Entity<Examen>(entity =>
        {
            entity.HasKey(e => new { e.NroLegajoA, e.CodMat, e.CodTurno, e.Año }).HasName("PK__Examenes__77ECCBFBCE0984CA");

            entity.Property(e => e.NroLegajoA).HasColumnName("nro_legajo_a");
            entity.Property(e => e.CodMat)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_mat");
            entity.Property(e => e.CodTurno)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_turno");
            entity.Property(e => e.Año)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("año");
            entity.Property(e => e.FechaInscripcion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inscripcion");
            entity.Property(e => e.Nota).HasColumnName("nota");

            entity.HasOne(d => d.CodMatNavigation).WithMany(p => p.Examenes)
                .HasForeignKey(d => d.CodMat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Examenes_Materias");

            entity.HasOne(d => d.CodTurnoNavigation).WithMany(p => p.Examenes)
                .HasForeignKey(d => d.CodTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Examenes_Turnos");

            entity.HasOne(d => d.NroLegajoANavigation).WithMany(p => p.Examenes)
                .HasForeignKey(d => d.NroLegajoA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Examenes_Alumnos");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.CodMateria).HasName("PK__Materias__A2BFED18D706109E");

            entity.Property(e => e.CodMateria)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_materia");
            entity.Property(e => e.DescCarrera)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("desc_carrera");
            entity.Property(e => e.DescMat)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("desc_mat");
            entity.Property(e => e.NroLegajoP).HasColumnName("nro_legajo_p");

            entity.HasOne(d => d.NroLegajoPNavigation).WithMany(p => p.Materia)
                .HasForeignKey(d => d.NroLegajoP)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Materias_Profesores");
        });

        modelBuilder.Entity<Planificacion>(entity =>
        {
            entity.HasKey(e => new { e.CodMat, e.CodTurno, e.Año }).HasName("PK__PLANIFIC__6D03A92A4931C766");

            entity.ToTable("PLANIFICACION");

            entity.Property(e => e.CodMat)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_mat");
            entity.Property(e => e.CodTurno)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_turno");
            entity.Property(e => e.Año)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("año");
            entity.Property(e => e.FechaExamen)
                .HasColumnType("datetime")
                .HasColumnName("fecha_examen");

            entity.HasOne(d => d.CodMatNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.CodMat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PLANIFICACION_Materias");

            entity.HasOne(d => d.CodTurnoNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.CodTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PLANIFICACION_Turnos");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.NroLegajoP).HasName("PK__Profesor__D551139F90C2E492");

            entity.Property(e => e.NroLegajoP)
                .ValueGeneratedNever()
                .HasColumnName("nro_legajo_p");
            entity.Property(e => e.ApeNomb)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("ape_nomb");
            entity.Property(e => e.CodDoc)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_doc");
            entity.Property(e => e.CodTitulo)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_titulo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.EstCivil)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("est_civil");
            entity.Property(e => e.FecNac)
                .HasColumnType("datetime")
                .HasColumnName("fec_nac");
            entity.Property(e => e.NroDoc).HasColumnName("nro_doc");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("sexo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("telefono");

            entity.HasOne(d => d.CodDocNavigation).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.CodDoc)
                .HasConstraintName("FK_Profesores_Tipo_Doc");

            entity.HasOne(d => d.CodTituloNavigation).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.CodTitulo)
                .HasConstraintName("FK_Profesores_TITULOS");
        });

        modelBuilder.Entity<TipoDoc>(entity =>
        {
            entity.HasKey(e => e.CodDoc).HasName("PK__Tipo_Doc__FE608ABEFF3EEB3E");

            entity.ToTable("Tipo_Doc");

            entity.Property(e => e.CodDoc)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_doc");
            entity.Property(e => e.DescDoc)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("desc_doc");
        });

        modelBuilder.Entity<Titulo>(entity =>
        {
            entity.HasKey(e => e.CodTitulos).HasName("PK__TITULOS__9B938BAC16E4AAE0");

            entity.ToTable("TITULOS");

            entity.Property(e => e.CodTitulos)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("COD_TITULOS");
            entity.Property(e => e.DescTitulo)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("DESC_TITULO");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.CodTurno).HasName("PK__Turnos__3FF70AAC3B0D26C2");

            entity.Property(e => e.CodTurno)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cod_turno");
            entity.Property(e => e.DescTurno)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("desc_turno");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
