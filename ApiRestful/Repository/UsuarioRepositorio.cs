using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Repository.IRepository;

namespace WebApplication1.Repository;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly ApplicationDbContext _db;
    private string claveSecreta;
    public UsuarioRepositorio(ApplicationDbContext db, IConfiguration config)
    {
        _db = db;
        claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
    }
    public Usuario GetUsuario(int usuarioId)
    {
        return _db.Usuario.FirstOrDefault(u => u.Id == usuarioId);
    }
    
    public ICollection<Usuario> GetUsuarios()
    {
        return _db.Usuario.OrderBy(u => u.UserName).ToList();
    }


    public bool IsUniqueUser(string user)
    {
        var userDb = _db.Usuario.FirstOrDefault(u => u.UserName == user);
        if (userDb == null)
        {
            return true;
        }

        return false;
    }

    public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
    {
        var encryptedPassword = obtenermd5(usuarioRegistroDto.Password);

        Usuario user = new Usuario()
        {
            UserName = usuarioRegistroDto.UserName,
            Password = encryptedPassword,
            Name = usuarioRegistroDto.Name,
            Role = usuarioRegistroDto.Role
        };

        _db.Usuario.Add(user);
        await _db.SaveChangesAsync();
        user.Password = encryptedPassword;
        return user;
    }

    public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
    {
        var encryptedPassword = obtenermd5(usuarioLoginDto.Password);

        var user = _db.Usuario.FirstOrDefault(
            u => u.UserName.ToLower() == usuarioLoginDto.UserName.ToLower()
            && u.Password == encryptedPassword
        );
        
        // Validamos si eel user no existe en la combinacion usuario y contraseña correcta
        if (user == null)
        {
            return new UsuarioLoginRespuestaDto()
            {
                Token = "",
                Usuario = null
            };
        }
        
        //El usuario existe, sigue el login
        var manejadorToken = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(claveSecreta);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }
            ),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = manejadorToken.CreateToken(tokenDescriptor);

        UsuarioLoginRespuestaDto usuarioLoignResuestaDto = new UsuarioLoginRespuestaDto()
        {
            Token = manejadorToken.WriteToken(token),
            Usuario = user
        };

        return usuarioLoignResuestaDto;
    }
    
    //Metodo para encryptar contraseña con MD5 
    public static string obtenermd5(string valor)
    {
        MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
        data = x.ComputeHash(data);
        string resp = "";
        for (int i = 0; i < data.Length; i++)
            resp += data[i].ToString("x2").ToLower();
        return resp;
    }
}