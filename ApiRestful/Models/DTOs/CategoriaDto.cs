using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;

public class CategoriaDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(60, ErrorMessage = "El numero maximo de caracteres es de 60!")]
    public string Nombre { get; set; } = null!;

}