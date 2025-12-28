using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Dentistas
{
    public class CrearDentistaDTO
    {
        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; }
        [Required]
        [StringLength(254)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
