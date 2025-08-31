using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    public class CasoDeUsoActualizarConsultorio : IRequestHandler<ComandoActualizarConsultorio>
    {
        private IrepositorioConsultorios Repositorio { get; }
        private IunidadDeTrabajo IunidadDeTrabajo { get; }
        public CasoDeUsoActualizarConsultorio(IrepositorioConsultorios repositorio
            , IunidadDeTrabajo iunidadDeTrabajo)
        {
            Repositorio = repositorio;
            IunidadDeTrabajo = iunidadDeTrabajo;
        }

        public async Task Handle(ComandoActualizarConsultorio request)
        {
            var consultorio = await this.Repositorio.ObtenerPorId(request.Id);

            if (consultorio is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            consultorio.ActualizarNombre(request.Nombre);
            try
            {
                await this.Repositorio.actualizar(consultorio);
                await IunidadDeTrabajo.Persistir();
            }
            catch (Exception) 
            {
                await this.IunidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
