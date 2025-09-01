using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DientesLimpies.pruebas;

[TestClass]
public class CasoDeUsoCrearPacienteTest
{
    private IRepositorioPacientes repositorio;
    private IunidadDeTrabajo unidadDeTrabajo;
    private CasoDeUsoCrearPaciente casoDeUso;

    [TestInitialize]
    public void setup() 
    {
        repositorio = Substitute.For<IRepositorioPacientes>();
        unidadDeTrabajo = Substitute.For<IunidadDeTrabajo>();
        casoDeUso = new CasoDeUsoCrearPaciente(repositorio, unidadDeTrabajo);
    }
    [TestMethod]
    public async Task Handle_CuandoDatosValidos_CreaPacientePersisteYRetornaId()
    {
        var comando = new ComandoCrearPaciente { Nombre = "Test", Email = "Test@gmail.com" };
        var pacienteCreado = new Paciente(comando.Nombre, new Email(comando.Email));
        var id = pacienteCreado.Id;

        repositorio.agregar(Arg.Any<Paciente>()).Returns(pacienteCreado);
        var idResultado = await casoDeUso.Handle(comando);

        Assert.AreEqual(id, idResultado);
        await repositorio.Received(1).agregar(Arg.Any<Paciente>());
        await unidadDeTrabajo.Received(1).Persistir(); //recivio una llamada al metodo persistir
    }


    [TestMethod]
    public async Task Handle_CuandocurreExcepcion_ReversaryLanzaExcepcion()
    {
        var comando = new ComandoCrearPaciente { Nombre = "relipe", Email = "felipe@ejemplo.com" }; 
        repositorio.agregar(Arg.Any<Paciente>())
        .Throws(new InvalidOperationException("Error al insertar"));
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(()=>casoDeUso.Handle(comando));
        await unidadDeTrabajo.Received(1).Reversar();
        await unidadDeTrabajo.DidNotReceive().Persistir();
    }
}
