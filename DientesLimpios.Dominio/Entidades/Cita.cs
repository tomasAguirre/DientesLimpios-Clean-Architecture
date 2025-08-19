using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Cita
    {
        public Guid id { get; private set; }
        public Guid PacienteId { get; private set; }
        public Guid DentistaId { get; private set; }
        public Guid ConsultorioId { get; private set; }
        public EstadoCita EstadoCita { get; private set; }
        public IntervaloDeTiempo intervaloDeTiempo { get; private set; }

        public Paciente? Paciente { get; private set; }
        public Dentista? Dentista { get; private set; }
        public Consultorio? Consultorio { get; private set; }

        public Cita(Guid pacienteId, Guid dentistaId, Guid consultorioId, IntervaloDeTiempo intervaloDeTiempo)
        {

            this.PacienteId = pacienteId;
            this.DentistaId = dentistaId;
            this.ConsultorioId = consultorioId;
            this.intervaloDeTiempo = intervaloDeTiempo;
            this.EstadoCita = EstadoCita.Programada;
            this.id = Guid.CreateVersion7();
        }

        public void Cancelar() 
        {
            if (this.EstadoCita != EstadoCita.Programada) 
            {
                throw new ExcepcionDeReglaDeNegocio($"Solo se pueden cancelar citas programadas.");
            }
            this.EstadoCita = EstadoCita.Cancelada;
        }

        public void Completada() 
        {
            if (this.EstadoCita != EstadoCita.Programada)
            {
                throw new ExcepcionDeReglaDeNegocio($"Solo se pueden completar citas programadas.");
            }
            this.EstadoCita = EstadoCita.Completada;
        }
    }
}
