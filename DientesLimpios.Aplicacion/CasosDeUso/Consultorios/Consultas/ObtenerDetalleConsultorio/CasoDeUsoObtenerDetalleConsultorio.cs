using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    public class CasoDeUsoObtenerDetalleConsultorio : IRequestHandler<ConsultaObtenerDetalleConsultorio,
                                        ConsultorioDetalleDTO>
    {
        public IrepositorioConsultorios Repositorio;
        public CasoDeUsoObtenerDetalleConsultorio(IrepositorioConsultorios repositorio)
        {
            Repositorio = repositorio;
        }

        public async Task<ConsultorioDetalleDTO> Handle(ConsultaObtenerDetalleConsultorio request)
        {
            var consultorio = await this.Repositorio.ObtenerPorId(request.Id);

            if(consultorio is null) 
            {
                throw new ExcepcionNoEncontrado();
            }

            //var dto = new ConsultorioDetalleDTO { Id = consultorio.Id, Nombre = consultorio.Nombre };
            //return dto;  mapeareamos el DTO con MapeadoExtensions
            return consultorio.ADto();
        }
    }
}
