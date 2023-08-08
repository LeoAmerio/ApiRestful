using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Usuario
{
    [Key] public int Id { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}