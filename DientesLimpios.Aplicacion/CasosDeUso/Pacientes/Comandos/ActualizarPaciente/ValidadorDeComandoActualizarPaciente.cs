﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente
{
    public class ValidadorDeComandoActualizarPaciente : AbstractValidator<ComandoActualizarPaciente>
    {
        public ValidadorDeComandoActualizarPaciente()
        {
            RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .MaximumLength(250).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLenth}");

            RuleFor(p => p.Email)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .MaximumLength(254).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLenth}")
            .EmailAddress().WithMessage("El formato del email no es valido");
        }
    }
}
