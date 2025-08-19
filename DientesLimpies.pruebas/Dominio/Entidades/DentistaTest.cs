using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;

namespace DientesLimpies.pruebas;

[TestClass]
public class DentistaTest
{
    [TestMethod]
    [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
    public void constructor_nombreNulo_LanzaExepcion()
    {
        var email = new Email("test@gmial.com");
        new Dentista(null!, email);
    }

    [TestMethod]
    [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
    public void constructor_emailNulo_LanzaExepcion()
    {
        Email email = null!;
        new Dentista("test", email);
    }
}
