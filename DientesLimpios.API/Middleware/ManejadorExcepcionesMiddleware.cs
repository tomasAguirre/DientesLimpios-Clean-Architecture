using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Excepciones;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace DientesLimpios.API.Middleware
{
    public class ManejadorExcepcionesMiddleware
    {
        private readonly RequestDelegate _next;

        public ManejadorExcepcionesMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task Invoke(HttpContext content)
        {
            try
            {
                await _next(content);
            }
            catch (Exception ex) 
            {
                ManejarExcepcion(content, ex);
            }
        }

        //cada ves que ocurra un error cae dentro de este metodo 
        private Task ManejarExcepcion(HttpContext content, Exception exception) 
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            content.Response.ContentType = "application/json";
            var resultado = string.Empty;

            switch (exception) 
            {
                case ExcepcionNoEncontrado:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ExcepcionDeValidacion excepcionDeValidacion:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    resultado = JsonSerializer.Serialize(excepcionDeValidacion.ErroresDeValidacion);
                    break;
                case ExcepcionDeReglaDeNegocio excepcionDeReglaDeNegocio:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    resultado = JsonSerializer.Serialize(new { mensaje = exception.Message });
                    break;

            }
            content.Response.StatusCode = (int)httpStatusCode;
            return content.Response.WriteAsync(resultado);
        }
    }

    public static class ManejadorExcepcionesMiddlewareExtensions
    {
        public static IApplicationBuilder UseManejadorExcepciones(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ManejadorExcepcionesMiddleware>();
        }
    }
}
