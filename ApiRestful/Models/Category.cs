using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required] public string Nombre { get; set; }
    public DateTime FechaCreacion { get; set; }
}