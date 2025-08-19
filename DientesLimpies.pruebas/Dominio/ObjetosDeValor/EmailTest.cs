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
    public class EmailTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void contructor_EmailNulo_LanzaExcepcion() 
        {
            new Email(null!);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void contructor_EmailSinArroba_LanzaExcepcion()
        {
            new Email("Test.com");
        }

        [TestMethod]
        public void contructor_EmailValido()
        {
            new Email("Test@gmail.com");
        }
    }
}
