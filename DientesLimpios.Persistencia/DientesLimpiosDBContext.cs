using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//add-migration mygrationName
//Update-Database

namespace DientesLimpios.Persistencia
{
    public class DientesLimpiosDBContext : DbContext
    {
        public DientesLimpiosDBContext(DbContextOptions<DientesLimpiosDBContext> options) : base(options)
        {
        }

        protected DientesLimpiosDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DientesLimpiosDBContext).Assembly);
            //Eso permite que tome las configuraciones que nosotros creamos en mi solucion de aplicacion Persistencia 
        }

        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Dentista> Dentistas { get; set; }
        public DbSet<Cita> Citas { get; set; }
    }
}
