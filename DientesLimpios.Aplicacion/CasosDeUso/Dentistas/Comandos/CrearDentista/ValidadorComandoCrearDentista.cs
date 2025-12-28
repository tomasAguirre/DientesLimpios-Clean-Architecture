using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista
{
    public class ValidadorComandoCrearDentista : AbstractValidator<ComandoCrearDentista>
    {
        public ValidadorComandoCrearDentista()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del dentista es obligatorio.")
                .MaximumLength(250).WithMessage("El nombre del dentista no puede exceder los 100 caracteres.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email del dentista es obligatorio.")
                .MaximumLength(250).WithMessage("El campo debe de tener 250 caracteres")
                .EmailAddress().WithMessage("El email del dentista no es válido.");
        }
    }
}
