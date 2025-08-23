using DientesLimpios.Aplicacion.Contratos.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.UnidadesDeTrabajo
{
    public class UnidadDeTrabajoEFCore : IunidadDeTrabajo
    {
        private DientesLimpiosDBContext context;

        public UnidadDeTrabajoEFCore(DientesLimpiosDBContext context)
        {
            this.context = context;
        }
        public async Task Persistir()
        {
            await this.context.SaveChangesAsync();
        }

        public Task Reversar()
        {
            return Task.CompletedTask;
        }
    }
}
