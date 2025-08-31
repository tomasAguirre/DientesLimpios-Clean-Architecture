using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace DientesLimpies.pruebas;

[TestClass]
public class CasoDeUsoActualizarContultorioTest
{
    private IrepositorioConsultorios repositorio;
    private IunidadDeTrabajo unidadDeTrabajo;
    private CasoDeUsoActualizarConsultorio casoDeUso;

    [TestInitialize]
    public void Setup() 
    {
        repositorio = Substitute.For<IrepositorioConsultorios>();
        unidadDeTrabajo = Substitute.For<IunidadDeTrabajo>();
        casoDeUso = new CasoDeUsoActualizarConsultorio(repositorio, unidadDeTrabajo);
    }

    [TestMethod]
    public async Task Handle_CuandoElConsultorioExiste_ActulizarNombreYPersiste() 
    {
        var consultorio = new Consultorio("Consultorio A");
        var id = consultorio.Id;
        var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Nuevo Nombre" };

        repositorio.ObtenerPorId(id).Returns(consultorio);
        await casoDeUso.Handle(comando);

        await repositorio.Received(1).actualizar(consultorio);
        await unidadDeTrabajo.Received(1).Persistir();
    }

    [TestMethod]
    [ExpectedException(typeof(ExcepcionNoEncontrado))]
    public async Task Handle_CuandoElConsultorioNoExiste_LanzaExcepcionNoEncontrado()
    {
        var comando = new ComandoActualizarConsultorio { Id = Guid.NewGuid(), Nombre = "Nuevo Nombre" };

        repositorio.ObtenerPorId(comando.Id).ReturnsNull();
        await casoDeUso.Handle(comando);
    }

    [TestMethod]
    public async Task Handle_CuandoOcurreErrorAlActualizar_LlamaReversarYLanzaExcepcion()
    {
        var consultorio = new Consultorio("Consultorio A");
        var id = consultorio.Id;
        var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Consultorio B" };
        repositorio.ObtenerPorId(id).Returns(consultorio);
        repositorio.actualizar(consultorio).Throws(new InvalidOperationException("Error al actualizar"));

        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));
        await unidadDeTrabajo.Received(1).Reversar();
    }
}
