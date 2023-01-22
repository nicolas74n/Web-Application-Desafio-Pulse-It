using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web_Application_Desafio_Pulse_It.Models;

public partial class DesafioContext : DbContext
{
    public DesafioContext()
    {
    }

    public DesafioContext(DbContextOptions<DesafioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<EstadoInvitacion> EstadoInvitacions { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Invitacion> Invitacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    
        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
             => optionsBuilder.UseSqlServer("server=localhost; database=desafio; integrated security=true; TrustServerCertificate=True;");

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asistenc__3214EC0709ED6519");

            entity.Property(e => e.AsistenciaUsuarioId).HasColumnName("AsistenciaUsuarioID");

            entity.HasOne(d => d.AsistenciaUsuario).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.AsistenciaUsuarioId)
                .HasConstraintName("FK_AsitenciaUsuarios");
        });

        modelBuilder.Entity<EstadoInvitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadoIn__3214EC07B891F60E");

            entity.ToTable("EstadoInvitacion");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Evento__3214EC07ABF25A9D");

            entity.ToTable("Evento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaEvento).HasColumnType("date");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_UsuarioEvento");
        });

        modelBuilder.Entity<Invitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invitaci__3214EC072C925782");

            entity.ToTable("Invitacion");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoInvitacion).WithMany(p => p.Invitacions)
                .HasForeignKey(d => d.EstadoInvitacionId)
                .HasConstraintName("FK_EstadoInvitacion");

            entity.HasOne(d => d.InvitacionEvento).WithMany(p => p.Invitacions)
                .HasForeignKey(d => d.InvitacionEventoId)
                .HasConstraintName("FK_InvitacionEvento");

            entity.HasOne(d => d.UsuarioInvitado).WithMany(p => p.Invitacions)
                .HasForeignKey(d => d.UsuarioInvitadoId)
                .HasConstraintName("FK_InvitacionUsuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07908BED8E");

            entity.ToTable("Usuario");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
