﻿using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerDetallePaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion
{
    public static class RegistroDeServiciosDeAplicacion
    {
        public static IServiceCollection AgregarServicioDeAplicacion(
                                            this IServiceCollection services) 
        {
            services.AddTransient<IMediator, MediadorSimple>();
            services.AddScoped<IRequestHandler<ComandoCrearConsultorio, Guid>,
                                                CasoDeUsoCrearConsultorio>();

            services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>,
                                                CasoDeUsoObtenerDetalleConsultorio>();

            services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios, 
                List<ConsultorioListadoDTO>>,
                                    CasoDeUsoObtenerListadoConsultorios>();

            services.AddScoped<IRequestHandler<ComandoActualizarConsultorio>, 
                                                CasoDeUsoActualizarConsultorio>();


            services.AddScoped<IRequestHandler<ComandoBorrarConsultorio>, CasoDeUsoBorrarConsultorio>();

            services.AddScoped<IRequestHandler<ComandoCrearPaciente, Guid>, CasoDeUsoCrearPaciente>();

            services.AddScoped<IRequestHandler<ConsultaObtenerListadoPacientes, PaginadoDTO<PacienteListadoDTO>>, 
                                                            CasoDeUsoObtenerListadoPacientes>();

            services.AddScoped < IRequestHandler<ConsultaObtenerDetallePaciente, PacienteDetalleDTO>,
                                                CasoDeUsoObtenerDetallePaciente>();

            services.AddScoped<IRequestHandler<ComandoActualizarPaciente>, CasoDeUsoActualizarPaciente>();

            services.AddScoped<IRequestHandler<ComandoBorrarPaciente>, CasoDeUsoBorrarPaciente>();

            return services;
        }
    }
}
