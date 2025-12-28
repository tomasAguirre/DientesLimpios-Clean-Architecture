using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas
{
    public class FilstroDentistasDTO
    {
        public int Pagina { get; set; } = 1; //setear esto como 1 ya que sino el skip dara error
        public int RegistrosPorPagina { get; set; } = 10;

        //Para filtrar opcional mente (no obligatorio), nombre y email
        public String? Nombre { get; set; }
        public String? Email { get; set; }
    }
}
