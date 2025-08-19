using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpies.pruebas.Dominio.Entidades
{
    [TestClass]
    public class ConsultorioTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void constructor_nombreNulo_lanzaExcepcion() 
        {
            new Consultorio(null!);
        }
    }
}
