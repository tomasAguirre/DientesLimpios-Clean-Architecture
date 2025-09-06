using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
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
    public class RepositorioPacientes : Repositorio<Paciente>, IRepositorioPacientes
    {
        private readonly DientesLimpiosDBContext dBContext;

        public RepositorioPacientes(DientesLimpiosDBContext dBContext) : base(dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IEnumerable<Paciente>> ObtenerFiltrado(FiltroPacienteDTO filtro)
        {
            var queryable = dBContext.Pacientes.AsQueryable();

            if (!string.IsNullOrEmpty(filtro.Nombre))
            {
                queryable = queryable.Where(x => x.Nombre.Contains(filtro.Nombre));
            }

            if (!string.IsNullOrEmpty(filtro.Email)) 
            {
                queryable = queryable.Where(x => x.Nombre.Contains(filtro.Email));
            }

            return await queryable.OrderBy(x => x.Nombre).
                                            Paginar(filtro.Pagina, filtro.RegistrosPorPagina).ToListAsync();
        }
    }
}
