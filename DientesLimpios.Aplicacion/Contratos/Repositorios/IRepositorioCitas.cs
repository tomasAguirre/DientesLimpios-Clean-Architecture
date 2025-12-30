using DientesLimpios.Aplicacion.CasosDeUso.Citas.Consultas.ObtenerListadoCitas;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    public interface IRepositorioCitas : Irepositorio<Cita>
    {
        Task<bool> ExisteCitaSolapada(Guid Dentista, DateTime inicio, DateTime fin);
        new Task<Cita> ObtenerPorId(Guid id);
        Task<IEnumerable<Cita>> ObtenerFiltrado(FiltroCitasDTO filtroCitasDTO);
    }
}
