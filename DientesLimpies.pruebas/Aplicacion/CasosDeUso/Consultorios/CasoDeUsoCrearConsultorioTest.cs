using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.ComponentModel.DataAnnotations;

namespace DientesLimpies.pruebas;

[TestClass]
public class CasoDeUsoCrearConsultorioTest
{
    private  IrepositorioConsultorios repositorio;
    private  IunidadDeTrabajo unidadDeTrabajo;
    private  CasoDeUsoCrearConsultorio casoDeUso;

    [TestInitialize]
    public void setup() 
    {
        repositorio = Substitute.For<IrepositorioConsultorios>();
        unidadDeTrabajo = Substitute.For<IunidadDeTrabajo>();

        casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo);
    }

    [TestMethod]
    public async Task Handle_ComandoValido_crearConsultorio()
    {
        var comando = new ComandoCrearConsultorio {Nombre = "Consultorio A" };
        var consultorioCreado = new Consultorio("Consultorio A");
        repositorio.agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);
        var resultado = await casoDeUso.Handle(comando);

        await repositorio.Received(1).agregar(Arg.Any<Consultorio>());
        await unidadDeTrabajo.Received(1).Persistir();

        Assert.AreNotEqual(Guid.Empty, resultado);
    }

    [TestMethod]
    [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
    public async Task Handle_ComandoNoValido_LanzarExcepcion()
    {
        var comando = new ComandoCrearConsultorio { Nombre = "" };

        var resultadoInvalido = new FluentValidation.Results.ValidationResult(new[] {
            new ValidationFailure("Nombre", "El nombre es obligatorio")
        });

        await Assert.ThrowsExceptionAsync<ExcepcionDeValidacion>(async () =>
        {
             await casoDeUso.Handle(comando);
        });
        await repositorio.DidNotReceive().agregar(Arg.Any<Consultorio>());

    }

    [TestMethod]
    public async Task Handle_CuandoHayError_HacemosRollBack()
    {
        var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
        repositorio.agregar(Arg.Any<Consultorio>()).Throws<Exception>();

        await Assert.ThrowsExceptionAsync<Exception>(async () =>
        {
            var resultado = await casoDeUso.Handle(comando);
        });

        await unidadDeTrabajo.Received(1).Reversar();

    }

}
