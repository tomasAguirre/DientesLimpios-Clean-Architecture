using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.ActualizarDentistas
{
    public class CasoDeUsoActualizarDentista : IRequestHandler<ComandoActualizarDentista>
    {
        private readonly IrepositorioDentista repositorio;
        private readonly IunidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoActualizarDentista(IrepositorioDentista repositorio, IunidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task Handle(ComandoActualizarDentista request)
        {
            var dentista = await this.repositorio.ObtenerPorId(request.Id);
            if (dentista is null)
            {
                throw new Exception("Dentista no encontrado");
            }
            dentista.actualizarNombre(request.Nombre);
            Email email = new Email(request.Email);
            dentista.actualizarEmail(email);
            try
            {
                await this.repositorio.actualizar(dentista);
                await this.unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                await this.unidadDeTrabajo.Persistir();
                throw;
            }
        }
    }
}
