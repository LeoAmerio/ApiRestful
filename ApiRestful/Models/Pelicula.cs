using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Pelicula
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string RutaImg { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }

    public enum TipoClasificaicon
    {
        Siete,
        Trece,
        Dieciseis,
        Dieciocho
    }

    public TipoClasificaicon Clasificacion { get; set; }
    public DateTime FechaCreacion { get; set; }
    [ForeignKey("categoriaId")]
    public int categoriaId { get; set; }

    public Category Categoria { get; set; }
}