using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System.Runtime.CompilerServices;

namespace DientesLimpies.pruebas;

[TestClass]
public class CitaTest
{
    private Guid _pacienteId = Guid.NewGuid();
    private Guid _dentistaId = Guid.NewGuid();
    private Guid _consultorioId = Guid.NewGuid();
    private IntervaloDeTiempo _intervalo = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(1), 
                                            DateTime.UtcNow.AddDays(2));

    [TestMethod]
    public void constructor_CitaValida_EstadoEsProgramada()
    {
        var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
        Assert.AreEqual(cita.PacienteId, _pacienteId);
        Assert.AreEqual(cita.DentistaId, _dentistaId);
        Assert.AreEqual(cita.ConsultorioId, _consultorioId);
        Assert.AreEqual(cita.intervaloDeTiempo, _intervalo);
        Assert.AreEqual(cita.EstadoCita, EstadoCita.Programada);
        Assert.AreNotEqual(cita.id, Guid.Empty);
        
    }

    [TestMethod]
    public void cancelar_citaProgramada_CambiaEstadoCancelada() 
    {
        var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
        cita.Cancelar();
        Assert.AreEqual(cita.EstadoCita, EstadoCita.Cancelada);
    }

    [TestMethod]
    [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
    public void cancelar_citaNoProgramada_LanzaExcepcion()
    {
        var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
        cita.Cancelar();
        cita.Cancelar();
    }

    [TestMethod]
    public void completar_citaProgramada_CambiaEstadoCompletar()
    {
        var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervalo);
        cita.Completada();
        Assert.AreEqual(cita.EstadoCita, EstadoCita.Completada);
    }
}
