﻿using DientesLimpios.Aplicacion.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public class MediadorSimple : IMediator
    {
        public readonly IServiceProvider ServiceProvider;
        public MediadorSimple(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var tipoValidador = typeof(IValidator<>).MakeGenericType(request.GetType());
            var validador = ServiceProvider.GetService(tipoValidador);

            if (validador is not null) 
            {
                var metodoValidar = tipoValidador.GetMethod("ValidateAsync");
                var tareaValidar = (Task)metodoValidar!.Invoke(validador,
                                    new object[] { request, CancellationToken.None });

                await tareaValidar.ConfigureAwait(false);
                var resultado = tareaValidar.GetType().GetProperty("Result");
                var validationResult = (ValidationResult)resultado!.GetValue(tareaValidar)!;

                if (!validationResult.IsValid) 
                {
                    throw new ExcepcionDeValidacion(validationResult);
                }
            }

            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var casoDeUso = this.ServiceProvider.GetService(tipoCasoDeUso);

            if (casoDeUso is null) 
            {
                throw new ExcepcionDeMediador($"No se encuentra un handler para {request.GetType().Name}");
            }

            var metodo = tipoCasoDeUso.GetMethod("Handle");

            return await (Task<TResponse>)metodo.Invoke(casoDeUso, new object[] {request})!;
        }
    }
}
