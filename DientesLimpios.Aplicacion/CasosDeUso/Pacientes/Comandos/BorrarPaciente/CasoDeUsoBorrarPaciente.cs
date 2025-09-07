using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente
{
    public class CasoDeUsoBorrarPaciente : IRequestHandler<ComandoBorrarPaciente>
    {
        private readonly IRepositorioPacientes repositorio;
        private readonly IunidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoBorrarPaciente(IRepositorioPacientes repositorio, IunidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task Handle(ComandoBorrarPaciente request)
        {
            var paciente = await this.repositorio.ObtenerPorId(request.Id);

            if (paciente is null) 
            {
                throw new ExcepcionNoEncontrado();
            }
            try
            {
                await this.repositorio.borrar(paciente);
                await this.unidadDeTrabajo.Persistir();
            }
            catch (Exception ex) 
            {
                this.unidadDeTrabajo.Reversar();
                throw ex;
            }
        }
    }
}
