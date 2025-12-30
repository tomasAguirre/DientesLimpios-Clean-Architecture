using DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerListadoCitas;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioCitas : Repositorio<Cita>, IRepositorioCitas
    {
        private readonly DientesLimpiosDBContext context;

        public RepositorioCitas(DientesLimpiosDBContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExisteCitaSolapada(Guid Dentista, DateTime inicio, DateTime fin)
        {
            return await context.Citas.Where(x => x.DentistaId == Dentista && x.EstadoCita == Dominio.Enums.EstadoCita.Programada && 
            inicio < x.intervaloDeTiempo.Fin && fin > x.intervaloDeTiempo.Inicio).AnyAsync();
        }

        public async Task<IEnumerable<Cita>> ObtenerFiltrado(FiltroCitasDTO filtroCitasDTO)
        {
            var queryable = context.Citas
                .Include(x => x.Paciente)
                .Include(x => x.Dentista)
                .Include(x => x.Consultorio)
                .AsQueryable();

            //filtros
            if (filtroCitasDTO.ConsultorioId is not null)
            {
                queryable = queryable.Where(x=> x.ConsultorioId == filtroCitasDTO.ConsultorioId);
            }
            if (filtroCitasDTO.DentistaId is not null)
            {
                queryable = queryable.Where(x => x.DentistaId == filtroCitasDTO.DentistaId);
            }
            if (filtroCitasDTO.PacienteId is not null)
            {
                queryable = queryable.Where(x => x.PacienteId == filtroCitasDTO.PacienteId);
            }
            return await queryable.Where(x => x.intervaloDeTiempo.Inicio >= filtroCitasDTO.FechaInicio
                   && x.intervaloDeTiempo.Fin < filtroCitasDTO.FechaFin)
                .OrderBy(x => x.intervaloDeTiempo.Inicio).ToListAsync();
        }

        new public async Task<Cita> ObtenerPorId(Guid id)
        {
            return await context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Dentista)
                .Include(c => c.Consultorio)
                .FirstOrDefaultAsync(c => c.id == id);
        }
    }
}
