using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacientesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PacientesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteListadoDTO>>> Get(
                                [FromQuery] ConsultaObtenerListadoPacientes consulta) 
        {
            var resultado = await mediator.Send(consulta);
            HttpContext.InsertarInformacionEnCabecera(resultado.Total); //metodo extension para agregar info al httpcontext
            return resultado.Elementos;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearPacienteDTO pacienteDTO) 
        {
            var comando = new ComandoCrearPaciente { Nombre = pacienteDTO.Nombre, Email = pacienteDTO.Email };
            await mediator.Send(comando);
            return Ok();
        }
    }
}
