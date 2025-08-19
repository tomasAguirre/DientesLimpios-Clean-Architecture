using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Consultorio
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null;

        public Consultorio(string Nombre)
        {
            if(string.IsNullOrWhiteSpace(Nombre)) 
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(Nombre)} es obligatorio");
            }
            this.Nombre = Nombre;
            Id = Guid.CreateVersion7();
        }

    }
}
