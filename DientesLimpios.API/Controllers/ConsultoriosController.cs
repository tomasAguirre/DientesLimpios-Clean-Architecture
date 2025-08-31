﻿using DientesLimpios.API.DTOs.Consultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/consultorios")]
    public class ConsultoriosController : ControllerBase
    {
        private readonly IMediator mediator;

        public ConsultoriosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultorioDetalleDTO>> Get(Guid id) 
        {
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };
            var resultado = await mediator.Send(consulta);
            return resultado;
        }

        [HttpGet]
        public async Task<ActionResult<List<ConsultorioListadoDTO>>> Get() 
        {
            var consulta = new ConsultaObtenerListadoConsultorios();
            var resultado = await this.mediator.Send(consulta);
            return resultado;
        }


        [HttpPost]
        public async Task<IActionResult> Post(CrearConsultorioDTO crearConsultorioDTO)
        {
            var comando = new ComandoCrearConsultorio { Nombre = crearConsultorioDTO.Nombre};
            await mediator.Send(comando);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarConsultorioDTO actualizarConsultorioDTO) 
        {
            var comando = new ComandoActualizarConsultorio { Id=id, Nombre = actualizarConsultorioDTO.nombre };
            await mediator.Send(comando); 
            return Ok();
        }
    }
}
