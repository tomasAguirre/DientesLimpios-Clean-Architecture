using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Consultorios
{
    public class ActualizarConsultorioDTO
    {
        [Required]
        [StringLength(150)]
        public required string nombre { get; set; }
    }
}
