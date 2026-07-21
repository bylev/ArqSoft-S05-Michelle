using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace CitasApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        // Definir las entidades y sus relaciones
        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Medico> Medicos => Set<Medico>();
        public DbSet<Cita> Citas => Set<Cita>();

        // Configurar las relaciones y restricciones
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Paciente>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Telefono).IsRequired().HasMaxLength(20);
            });

            builder.Entity<Medico>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Especialidad).IsRequired().HasMaxLength(100);
                entity.Property(m => m.NumeroLicencia).IsRequired().HasMaxLength(50);
            });

            builder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Motivo).HasMaxLength(250).IsRequired();
                entity.Property(e => e.Estado).HasMaxLength(30).IsRequired();

                entity.HasOne<Paciente>()
                      .WithMany()
                      .HasForeignKey(e => e.PacienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Medico>()
                      .WithMany()
                      .HasForeignKey(e => e.MedicoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
