using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Testing.Platform.Requests;
using NSubstitute;
using System.Security.Cryptography.X509Certificates;

namespace DientesLimpies.pruebas;

[TestClass]
public class MediadorSimpleTest
{
    public class RequestFalso : IRequest<string> { }
    public class HandleFalso : IRequestHandler<RequestFalso, string> 
    {
        public Task<string> Handle(RequestFalso request) 
        {
            return Task.FromResult("respuesta correcta");
        }
    };  
    [TestMethod]
    public async Task Llama_metodoHandle()
    {
        var request = new RequestFalso();
        var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();
        var ServiceProvider = Substitute.For<IServiceProvider>();
        ServiceProvider
            .GetService(typeof(IRequestHandler<RequestFalso, string>))
            .Returns(casoDeUsoMock);

        var mediador = new MediadorSimple(ServiceProvider);

        var resultado = await mediador.Send(request);

        await casoDeUsoMock.Received(1).Handle(request);
    }

    [TestMethod]
    [ExpectedException(typeof(ExcepcionDeMediador))]
    public async Task Send_SinHandlerRegistrado_LanzaExcepcion()
    {
        var request = new RequestFalso();
        var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();
        var ServiceProvider = Substitute.For<IServiceProvider>();
        //ServiceProvider
        //    .GetService(typeof(IRequestHandler<RequestFalso, string>))
        //    .Returns(casoDeUsoMock);

        var mediador = new MediadorSimple(ServiceProvider);

        var resultado = await mediador.Send(request);

        //await casoDeUsoMock.Received(1).Handle(request);
    }
}
