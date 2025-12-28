using DientesLimpios.API.DTOs.Dentistas;
using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.ActualizarDentistas;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.BorrarDentistas;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerDetalleDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/dentistas")]
    public class DentistasController : ControllerBase
    {
        private readonly IMediator mediator;

        public DentistasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DentistaListadoDTO>>> Get(
                                [FromQuery] ConsultaObtenerListadoDentista consulta)
        {
            var resultado = await mediator.Send(consulta);
            HttpContext.InsertarInformacionEnCabecera(resultado.Total); //metodo extension para agregar info al httpcontext
            return resultado.Elementos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DentistaDetalleDTO>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetalleDentista() { Id = id };
            var resultado = await mediator.Send(consulta);
            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearDentistaDTO dentistaDTO)
        {
            var comando = new ComandoCrearDentista { Nombre = dentistaDTO.Nombre, Email = dentistaDTO.Email };
            await mediator.Send(comando);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarDentistaDTO actualizarDentistaDTO)
        {
            var comando = new ComandoActualizarDentista
            {
                Id = id,
                Nombre = actualizarDentistaDTO.Nombre,
                Email = actualizarDentistaDTO.Email
            };
            await mediator.Send(comando);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var comando = new ComandoBorrarDentista { Id = id };
            await mediator.Send(comando);
            return Ok();
        }
    }
}
