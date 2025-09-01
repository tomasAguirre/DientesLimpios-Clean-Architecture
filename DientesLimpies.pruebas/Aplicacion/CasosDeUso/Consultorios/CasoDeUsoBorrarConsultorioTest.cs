using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DientesLimpies.pruebas;

[TestClass]
public class CasoDeUsoBorrarConsultorioTest
{
    private IrepositorioConsultorios repositorio;
    private IunidadDeTrabajo unidadDeTrabajo;
    private CasoDeUsoBorrarConsultorio casoDeUso;


    [TestInitialize]
    public void setup()
    {
        repositorio = Substitute.For<IrepositorioConsultorios>();
        unidadDeTrabajo = Substitute.For<IunidadDeTrabajo>();
        casoDeUso = new CasoDeUsoBorrarConsultorio(repositorio, unidadDeTrabajo);
    }

    [TestMethod]
    public async Task Hadle_CuandoConsultorioExiste_BorrarConsultorioYPersiste()
    {
        var id = Guid.NewGuid();
        var comando = new ComandoBorrarConsultorio { Id = id };
        var consultorio = new Consultorio("Consultorio A");

        repositorio.ObtenerPorId(id).Returns(consultorio);
        await casoDeUso.Handle(comando);

        await repositorio.Received(1).borrar(consultorio);
        await unidadDeTrabajo.Received(1).Persistir();
    }

    //[TestMethod]
    //[ExpectedException(typeof(ExcepcionNoEncontrado))]
    //public async Task Hadle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
    //{
    //    var id = Guid.NewGuid();
    //    var comando = new ComandoBorrarConsultorio { Id = id };

    //    repositorio.ObtenerPorId(id).ReturnsNull();
    //    await casoDeUso.Handle(comando);

    //}
}
