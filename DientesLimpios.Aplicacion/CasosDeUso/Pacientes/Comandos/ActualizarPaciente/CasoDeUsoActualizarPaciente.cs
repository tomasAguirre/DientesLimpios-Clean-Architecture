using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente
{
    public class CasoDeUsoActualizarPaciente : IRequestHandler<ComandoActualizarPaciente>
    {
        private readonly IRepositorioPacientes repositorio;
        private readonly IunidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoActualizarPaciente(IRepositorioPacientes repositorio, IunidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task Handle(ComandoActualizarPaciente request)
        {
            var paciente = await this.repositorio.ObtenerPorId(request.Id);
            if (paciente is null) 
            {
                throw new ExcepcionNoEncontrado();
            }

            paciente.actualizarNombre(request.Nombre);
            var email = new Email(request.Email);
            paciente.actualizarEmail(email);
            try
            {
                await this.repositorio.actualizar(paciente);
                await this.unidadDeTrabajo.Persistir();
            }
            catch (Exception ex) 
            {
                await this.unidadDeTrabajo.Reversar();
                throw;
            }

        }
    }
}
