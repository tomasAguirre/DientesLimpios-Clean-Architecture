using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Dentista
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public Email Email { get; private set; } = null!;

        //contructor sin parametros para que el entitiy framework pueda crear la tabla sin problemas 
        private Dentista()
        {
            
        }

        public Dentista(string nombre, Email email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }


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
