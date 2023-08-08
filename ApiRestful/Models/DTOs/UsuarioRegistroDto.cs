﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;

public class UsuarioRegistroDto
{
    [Required(ErrorMessage = "El usuario es obligatorio")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "El usuario es obligatorio")]
    public string Name { get; set; }
    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; }
    public string Role { get; set; }
}