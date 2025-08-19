using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Contratos.Persistencia
{
    public interface IunidadDeTrabajo
    {
        Task Persistir();
        Task Reversar();
    }
}
