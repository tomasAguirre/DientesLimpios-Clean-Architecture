using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista
{
    public class CasoDeUsoCrearDentista : IRequestHandler<ComandoCrearDentista, Guid>
    {
        private readonly IrepositorioDentista repositorioDentista;
        private readonly IunidadDeTrabajo unidadDeTrabajo;

        public CasoDeUsoCrearDentista(IrepositorioDentista repositorioDentista, IunidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorioDentista = repositorioDentista;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task<Guid> Handle(ComandoCrearDentista request)
        {
            var email = new Email(request.Email);
            var dentista = new Dentista(request.Nombre, email);

            try
            {
                var respuesta = await repositorioDentista.agregar(dentista);
                await unidadDeTrabajo.Persistir();
                return respuesta.Id;
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
