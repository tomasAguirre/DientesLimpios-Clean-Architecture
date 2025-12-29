using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CrearCita
{
    public class CasoDeUsoCrearCita : IRequestHandler<ComandoCrearCita, Guid>
    {
        private readonly IunidadDeTrabajo iunidadDeTrabajo;
        private readonly IRepositorioCitas repositorioCitas;

        public CasoDeUsoCrearCita(IRepositorioCitas repositorioCitas, IunidadDeTrabajo iunidadDeTrabajo)
        {
           this.repositorioCitas = repositorioCitas;
            this.iunidadDeTrabajo = iunidadDeTrabajo;
        }

        public IRepositorioCitas RepositorioCitas { get; }

        public async Task<Guid> Handle(ComandoCrearCita request)
        {
            var citaSolapada = this.repositorioCitas.ExisteCitaSolapada(request.DentistaId, request.FechaInicio, request.FechaFin);
            if (await citaSolapada)
            {
                throw new ExcepcionDeValidacion("El dentista ya tiene una cita programada en ese horario");
            }
            var intervaloDeTiempo = new IntervaloDeTiempo(request.FechaInicio, request.FechaFin);
            var cita = new Cita(request.PacienteId, request.DentistaId, request.ConsultorioId, intervaloDeTiempo);
            try
            {
                var respuesta = await this.repositorioCitas.agregar(cita);
                await this.iunidadDeTrabajo.Persistir();
                return respuesta.id;
            }
            catch (Exception)
            {
                await this.iunidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
