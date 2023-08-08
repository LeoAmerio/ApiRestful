using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.DTOs;

public class PeliculaDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Name { get; set; }
    public string RutaImg { get; set; }
    [Required(ErrorMessage = "La descripcion es obligatorio")]
    public string Description { get; set; }
    [Required(ErrorMessage = "La duracion es obligatorio")]
    public int Duration { get; set; }

    public enum TipoClasificaicon
    {
        Siete,
        Trece,
        Dieciseis,
        Dieciocho
    }

    public Pelicula.TipoClasificaicon Clasificacion { get; set; }
    public DateTime FechaCreacion { get; set; }
    [ForeignKey("categoriaId")]
    public int categoriaId { get; set; }

    public Category Categoria { get; set; }
}