using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    //es normal que cada caso de uso tenga su propio DTO
    //Seccion 6 video : Obtener Lista de consultorios 
    public class ConsultorioListadoDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
    }
}
