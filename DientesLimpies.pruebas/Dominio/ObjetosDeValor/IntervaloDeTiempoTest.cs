using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpies.pruebas.Dominio.ObjetosDeValor
{
    [TestClass]
    public class IntervaloDeTiempoTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void contructor_FechaInicioPosteriorFechaFin_LanzaError() 
        {
            new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1));
        }

        [TestMethod]
        public void contructor_ParametrosCorrectos()
        {
            new IntervaloDeTiempo(DateTime.UtcNow.AddMinutes(30), DateTime.UtcNow.AddMinutes(60));
        }

    }
}
