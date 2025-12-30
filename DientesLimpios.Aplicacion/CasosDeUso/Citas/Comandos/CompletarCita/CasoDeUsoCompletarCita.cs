using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Citas.Comandos.CompletarCita
{
    public class CasoDeUsoCompletarCita : IRequestHandler<ComandoCompletarCita>
    {
        private readonly IRepositorioCitas repositorio;
        private readonly IunidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoCompletarCita(IRepositorioCitas repositorio, IunidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task Handle(ComandoCompletarCita request)
        {
            var cita = await this.repositorio.ObtenerPorId(request.Id);
            if (cita is null) 
            {
                throw new ExcepcionNoEncontrado();
            }
            cita.Completada();
            try
            {
                await this.repositorio.actualizar(cita);
                await this.unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                await this.unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
