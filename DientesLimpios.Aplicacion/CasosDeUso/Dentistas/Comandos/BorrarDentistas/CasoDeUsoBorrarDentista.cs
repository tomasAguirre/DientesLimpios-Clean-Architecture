using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.BorrarDentistas
{
    public class CasoDeUsoBorrarDentista : IRequestHandler<ComandoBorrarDentista>
    {
        private readonly IRepositorioPacientes repositorio;
        private readonly IunidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoBorrarDentista(IRepositorioPacientes repositorio, IunidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task Handle(ComandoBorrarDentista request)
        {
            var dentista = await this.repositorio.ObtenerPorId(request.Id);
            if (dentista is null)
            {
                throw new Exception("Dentista no encontrado");
            }
            try
            {
                await this.repositorio.borrar(dentista);
                await this.unidadDeTrabajo.Persistir();
            }
            catch (Exception ex)
            {
                await this.unidadDeTrabajo.Reversar();
                throw ex;
            }
        }
    }
}
