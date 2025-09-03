using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Utilidades
{
    //un objeto generico y estatico para poderlo reutilizar con otras entidades 
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, int pagina, int registrosPorPagina) 
        {
            return queryable
                .Skip((pagina - 1) * registrosPorPagina) //skip permite saltar cantidad de registros
                .Take(registrosPorPagina);
        }
    }
}
