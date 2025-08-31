using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio
{
    public class CasoDeUsoBorrarConsultorio : IRequestHandler<ComandoBorrarConsultorio>
    {
        public IrepositorioConsultorios Repositorio { get; }
        public IunidadDeTrabajo IunidadDeTrabajo { get; }
        public CasoDeUsoBorrarConsultorio(IrepositorioConsultorios repositorio, IunidadDeTrabajo iunidadDeTrabajo)
        {
            Repositorio = repositorio;
            IunidadDeTrabajo = iunidadDeTrabajo;
        }

        public async Task Handle(ComandoBorrarConsultorio request)
        {
            var consultorio = await this.Repositorio.ObtenerPorId(request.Id);
            if (consultorio is null) 
            {
                throw new ExcepcionNoEncontrado();
            }

            try
            {
                await this.Repositorio.borrar(consultorio);
                await this.IunidadDeTrabajo.Persistir();
            }
            catch (Exception) 
            {
                await this.IunidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
