
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;

namespace DientesLimpios.Aplicacion.Excepciones
{
    public class ExcepcionDeValidacion : Exception
    {
        public List<string> ErroresDeValidacion { get; set; } = [];

        public ExcepcionDeValidacion(string mensajeError) 
        {
            this.ErroresDeValidacion.Add(mensajeError);
        }

        public ExcepcionDeValidacion(FluentValidation.Results.ValidationResult validationResult)
        {
            foreach (var errorDeValidacion in validationResult.Errors) 
            {
                this.ErroresDeValidacion.Add(errorDeValidacion.ErrorMessage);
            }
        }

    }
}
