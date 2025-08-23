using DientesLimpios.Aplicacion.Contratos.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class Repositorio<T> : Irepositorio<T> where T : class
    {
        private DientesLimpiosDBContext context;

        public Repositorio(DientesLimpiosDBContext dBContext) 
        {
            this.context = dBContext;
        }

        public Task actualizar(T entidad)
        {
            this.context.Update(entidad);
            return Task.CompletedTask;
        }

        public Task<T> agregar(T entidad)
        {
            this.context.Add(entidad);
            return Task.FromResult(entidad);
        }

        public Task borrar(T entidad)
        {
            this.context.Remove(entidad);
            return Task.CompletedTask;
        }

        public async Task<T> ObtenerPorId(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> obtenerTodos()
        {
            return await context.Set<T>().ToListAsync();
        }
    }
}
