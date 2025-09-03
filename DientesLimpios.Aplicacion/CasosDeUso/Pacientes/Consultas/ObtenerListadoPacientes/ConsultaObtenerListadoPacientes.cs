using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    //heredamos de : FiltroPacienteDTO, para tener acceso a pagina y registos por pagina
    public class ConsultaObtenerListadoPacientes : FiltroPacienteDTO, IRequest<PaginadoDTO<PacienteListadoDTO>>
    {

    }
}
