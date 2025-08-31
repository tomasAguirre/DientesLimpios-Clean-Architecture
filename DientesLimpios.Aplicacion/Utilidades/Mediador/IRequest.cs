using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public interface IRequest<TResponse>
    {
    }


    /// <summary>
    /// Class <c>IRequest</c> caso de uso que no retorna nada 
    /// </summary>
    public interface IRequest
    {
    }
}
