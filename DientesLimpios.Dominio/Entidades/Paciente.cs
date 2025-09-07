using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DientesLimpios.Dominio.Entidades
{
    public class Paciente
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public Email Email { get; private set; } = null!;

        private Paciente()
        {
            
        }

        public Paciente(string nombre, Email email)
        {
            this.AplicarReglasDeNegocioNombre(nombre);
            this.AplicarReglasDeNegocioEmail(email);

            this.Id = Guid.CreateVersion7();
            this.Nombre = nombre;
            this.Email = email;
        }

        public void actualizarNombre(string nombre) 
        {
            this.AplicarReglasDeNegocioNombre(nombre);
            this.Nombre = nombre;
        }

        public void actualizarEmail(Email email)
        {
            this.AplicarReglasDeNegocioEmail(email);
            this.Email = email;
        }

        private void AplicarReglasDeNegocioNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }
        }

        private void AplicarReglasDeNegocioEmail(Email email) 
        {
            if (email is null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }
        }
    }
}
