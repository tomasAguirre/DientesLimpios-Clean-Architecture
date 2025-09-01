using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Configuraciones
{
    //Add-Migration TablaPacientes
    //Update-Database
    public class PacienteConfig : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.Property(prop => prop.Nombre)
                .HasMaxLength(250)
                .IsRequired();

            //El email es una propiedad de un value object una propiedad compleja 
            //ComplexProperty para hacer mapeao de datos especiales (objetos de valor)
            builder.ComplexProperty(prop => prop.Email, accion => {
                accion.Property(e => e.Valor).HasColumnName("Email").HasMaxLength(254);  //Valor es la propiedad que esta dentro del valuesobjet email
            });
        }
    }
}
