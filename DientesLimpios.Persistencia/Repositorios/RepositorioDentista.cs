using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Persistencia.Utilidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioDentista : Repositorio<Dentista>, IrepositorioDentista
    {
        private readonly DientesLimpiosDBContext dBContext;
        public RepositorioDentista(DientesLimpiosDBContext dBContext) : base(dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IEnumerable<Dentista>> ObtenerFiltrado(FilstroDentistasDTO filtro)
        {
            var queryable = dBContext.Dentistas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nombre)) 
            {
                queryable = queryable.Where(d => d.Nombre.Contains(filtro.Nombre));
            }

            if(!string.IsNullOrEmpty(filtro.Email))
            {
                queryable = queryable.Where(d => d.Email.Valor.Contains(filtro.Email));
            }

            return await queryable.OrderBy(x => x.Nombre).Paginar(filtro.Pagina, filtro.RegistrosPorPagina).ToListAsync();
        }
    }
}
