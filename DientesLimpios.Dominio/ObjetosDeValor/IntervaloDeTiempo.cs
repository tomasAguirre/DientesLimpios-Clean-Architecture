using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    public record IntervaloDeTiempo
    {
        public DateTime Inicio { get; }
        public DateTime Fin { get; }

        //contructor sin parametros para que el entitiy framework pueda crear la tabla sin problemas 
        private IntervaloDeTiempo()
        {
            
        }

        public IntervaloDeTiempo(DateTime Inicio, DateTime Fin)
        {
            if (Inicio > Fin)
            {
                throw new ExcepcionDeReglaDeNegocio($"la fecha de inicio no puede ser posterior a la fecha fin");
            }

            if (Inicio < DateTime.UtcNow)
            {
                throw new ExcepcionDeReglaDeNegocio($"La fecha de inicio no puede ser anterior a la fecha actual");
            }

            this.Inicio = Inicio;
            this.Fin = Fin;
        }
    }
}
