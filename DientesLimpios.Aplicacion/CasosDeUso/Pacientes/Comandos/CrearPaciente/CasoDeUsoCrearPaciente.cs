using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente
{
    public class CasoDeUsoCrearPaciente : IRequestHandler<ComandoCrearPaciente, Guid>
    {
        private readonly IRepositorioPacientes repositorio;

        public CasoDeUsoCrearPaciente(IRepositorioPacientes repositorio, IunidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            UnidadDeTrabajo = unidadDeTrabajo;
        }

        public IunidadDeTrabajo UnidadDeTrabajo { get; }

        public async Task<Guid> Handle(ComandoCrearPaciente request)
        {
            var email = new Email(request.Email);
            var paciente = new Paciente(request.Nombre, email);

            try
            {
                var respuesta = await this.repositorio.agregar(paciente);
                await this.UnidadDeTrabajo.Persistir();
                return respuesta.Id;
            }
            catch (Exception) {
                await this.UnidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
