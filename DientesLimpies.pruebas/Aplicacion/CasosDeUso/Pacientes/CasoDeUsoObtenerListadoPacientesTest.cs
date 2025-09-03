using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;

namespace DientesLimpies.pruebas;

[TestClass]
public class CasoDeUsoObtenerListadoPacientesTest
{
    private IRepositorioPacientes repositorio;
    private CasoDeUsoObtenerListadoPacientes casoDeUso;

    [TestInitialize]
    public void setup() 
    {
        repositorio = Substitute.For<IRepositorioPacientes>();
        casoDeUso = new CasoDeUsoObtenerListadoPacientes(repositorio);
    }

    [TestMethod]
    public async Task Handle_RetornaPacientesPaginadosCorrectamente()
    {
        var pagina = 1;
        var registrosPorPagina = 2;

        var filtroPaciente = new FiltroPacienteDTO { Pagina = pagina, RegistrosPorPagina = registrosPorPagina };

        var paciente1 = new Paciente("Felipe", new DientesLimpios.Dominio.ObjetosDeValor.Email("Felipe@gmail.com"));
        var paciente2 = new Paciente("Claudia", new DientesLimpios.Dominio.ObjetosDeValor.Email("Claudia@gmail.com"));

        IEnumerable<Paciente> pacientes = new List<Paciente> { paciente1, paciente2 };
        repositorio.ObtenerFiltrado(Arg.Any<FiltroPacienteDTO>()).Returns(Task.FromResult(pacientes));

        var request = new ConsultaObtenerListadoPacientes
        {
            Pagina = pagina,
            RegistrosPorPagina = registrosPorPagina,
        };
        var resultado = await casoDeUso.Handle(request);

      //  Assert.AreEqual(10, resultado.Total);
        Assert.AreEqual(2, resultado.Elementos.Count);
        Assert.AreEqual("Felipe", resultado.Elementos[0].Nombre);
        Assert.AreEqual("Claudia", resultado.Elementos[1].Nombre);

    }


}
