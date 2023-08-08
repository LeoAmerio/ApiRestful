using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Repository.IRepository;

public interface IUsuarioRepositorio
{
    ICollection<Usuario> GetUsuarios();
    Usuario GetUsuario(int usuarioId);
    bool IsUniqueUser(string user);
    Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
    Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto);

}