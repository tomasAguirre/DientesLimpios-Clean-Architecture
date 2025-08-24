using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos
{
    public class CasoDeUsoCrearConsultorio : IRequestHandler<ComandoCrearConsultorio, Guid>
    {
        private readonly IrepositorioConsultorios repositorio;
        private readonly IunidadDeTrabajo unidadDeTrabajo;
       // private readonly IValidator<ComandoCrearConsultorio> validador;

        public CasoDeUsoCrearConsultorio(IrepositorioConsultorios repositorio, IunidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
            //this.validador = validador; el mediador realizara las validaciones
        }
        public async Task<Guid> Handle(ComandoCrearConsultorio comando) 
        {
            #region
            //Esta validacion se mueve a el constructor del consultorio
            //var resultadoValidacion = await this.validador.ValidateAsync(comando);
            //if(!resultadoValidacion.IsValid)
            //{
            //    throw new ExcepcionDeValidacion(resultadoValidacion);
            //}
            #endregion
            var consultorio = new Consultorio(comando.Nombre);
            try
            {
                var respuesta = await repositorio.agregar(consultorio);
                await this.unidadDeTrabajo.Persistir();
                return respuesta.Id;
            }
            catch (Exception)
            {
                await this.unidadDeTrabajo.Reversar();
                throw;
            }

        }
    }
}
