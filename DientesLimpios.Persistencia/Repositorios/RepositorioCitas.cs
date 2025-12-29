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
