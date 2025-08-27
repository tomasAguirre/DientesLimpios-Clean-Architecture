using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    public class CasoDeUsoObtenerListadoConsultorios : IRequestHandler<ConsultaObtenerListadoConsultorios,
                                                            List<ConsultorioListadoDTO>>
    {
        private readonly IrepositorioConsultorios repositorio;

        public CasoDeUsoObtenerListadoConsultorios(IrepositorioConsultorios repositorio)
        {
            this.repositorio = repositorio;
        }
        public async Task<List<ConsultorioListadoDTO>> Handle(ConsultaObtenerListadoConsultorios request)
        {
            var consultorios = await this.repositorio.obtenerTodos();
            var consultoriosDTO = consultorios.Select(consultorios => consultorios.ADto()).ToList();
            return consultoriosDTO;
        }
    }
}
