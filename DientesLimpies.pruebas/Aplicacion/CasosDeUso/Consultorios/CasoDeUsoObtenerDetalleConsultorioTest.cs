using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DientesLimpies.pruebas;

[TestClass]
public class CasoDeUsoObtenerDetalleConsultorioTest
{
    private IrepositorioConsultorios repositorio; 
    private CasoDeUsoObtenerDetalleConsultorio CasoDeUsoObtenerDetalleConsultorio;

    [TestInitialize]
    public void setup() 
    {
        repositorio = Substitute.For<IrepositorioConsultorios>();
        CasoDeUsoObtenerDetalleConsultorio = new CasoDeUsoObtenerDetalleConsultorio(repositorio);
    }

    [TestMethod]
    public async Task Consultorio_Existe_RetornaDTO()
    {
        //Preparacion
        var consultorio = new Consultorio("Consultorio A");
        var id = consultorio.Id;
        var consulta = new ConsultaObtenerDetalleConsultorio() { Id=id };

        repositorio.ObtenerPorId(id).Returns(consultorio);

        //Prueba 
        var resultado = await CasoDeUsoObtenerDetalleConsultorio.Handle(consulta);

        //verificacion 
        Assert.IsNotNull(resultado);
        Assert.AreEqual(id, resultado.Id);
        Assert.AreEqual("Consultorio A", resultado.Nombre);
    }

    [TestMethod]
    [ExpectedException(typeof(ExcepcionNoEncontrado))]
    public async Task Handle_ConsultorioNoExiste_LanzaExcepcionNoEncontrado() 
    {
        //preparacion 
        var id = Guid.NewGuid();
        var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

        repositorio.ObtenerPorId(id).ReturnsNull();

        //Prueba 
        await CasoDeUsoObtenerDetalleConsultorio.Handle(consulta);
    }
}
