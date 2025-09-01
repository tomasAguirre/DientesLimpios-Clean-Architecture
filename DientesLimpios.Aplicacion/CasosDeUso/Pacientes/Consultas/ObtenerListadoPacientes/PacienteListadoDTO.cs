﻿using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public class PacienteListadoDTO
    {
        public Guid Id { get;  set; }
        public required string Nombre { get;  set; }
        public required string Email { get; set; } 
    }
}
