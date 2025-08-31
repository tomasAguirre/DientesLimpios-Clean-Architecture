using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;

namespace DientesLimpies.pruebas;

[TestClass]
public class CasoDeUsoObtenerListadoConsultoriosTest
{
    private IrepositorioConsultorios irepositorio;
    private CasoDeUsoObtenerListadoConsultorios casoDeUso;


    [TestInitialize]
    public void setup() 
    {
        irepositorio = Substitute.For<IrepositorioConsultorios>();
        casoDeUso = new CasoDeUsoObtenerListadoConsultorios(irepositorio);
    }

    [TestMethod]
    public async Task Handle_CuandoHayConsultorios_RetornaListaDeConsultorioListadoDTO()
    {
        var consultorio = new List<Consultorio>
        {
            new Consultorio("Consultorio A"),
            new Consultorio("Consultorio B"),
        };

        irepositorio.obtenerTodos().Returns(consultorio);
        var esperado = consultorio.Select(c => new ConsultorioListadoDTO
        {
            Id = c.Id, Nombre = c.Nombre,
        }).ToList();

        var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());
        Assert.AreEqual(esperado.Count, resultado.Count);
    }

    [TestMethod]
    public async Task Handle_CuandoNoHayConsultorios_RetornaListaVacia()
    {
        irepositorio.obtenerTodos().Returns(new List<Consultorio>());
        var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

        Assert.IsNotNull(resultado);
        Assert.AreEqual(0, resultado.Count);
    }
}
