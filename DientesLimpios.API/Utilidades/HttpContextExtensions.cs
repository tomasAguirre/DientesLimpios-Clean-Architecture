namespace DientesLimpios.API.Utilidades
{
    public static class HttpContextExtensions
    {
        public static void InsertarInformacionEnCabecera(this HttpContext httpContext, int cantidadDeRegistros) 
        {
            httpContext.Response.Headers.Append("Cantidad-total-registros", cantidadDeRegistros.ToString());
        }
    }
}
