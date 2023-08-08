using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository.IRepository;

namespace WebApplication1.Repository;

public class PeliculaRepositorio : IPeliculaRepositorio
{
    private readonly ApplicationDbContext _db;

    public PeliculaRepositorio(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public ICollection<Pelicula> GetPeliculas()
    {
        return _db.Pelicula.OrderBy(c => c.Name).ToList();
    }

    public Pelicula GetPelicula(int peliculaId)
    {
        return _db.Pelicula.FirstOrDefault(c => c.Id == peliculaId);
    }

    public bool ExistePelicula(string name)
    {
        bool value = _db.Pelicula.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        return value;
    }

    public bool ExistePelicula(int id)
    {
        return _db.Pelicula.Any(c => c.Id == id);
    }

    public bool CrearPelicula(Pelicula pelicula)
    {
        pelicula.FechaCreacion = DateTime.Now;
        _db.Pelicula.Add(pelicula);
        return Guardar();
    }

    public bool ActualizarPelicula(Pelicula pelicula)
    {
        pelicula.FechaCreacion = DateTime.Now;
        _db.Pelicula.Update(pelicula);
        return Guardar();
    }

    public bool EliminarPelicula(Pelicula pelicula)
    {
        _db.Pelicula.Remove(pelicula);
        return Guardar();
    }

    public ICollection<Pelicula> GetPeliculaByCategory(int id)
    {
        return _db.Pelicula.Include(ca => ca.Categoria).Where(ca => ca.categoriaId == id).ToList();
    }

    public ICollection<Pelicula> GetPeliculaByName(string nombre)
    {
        IQueryable<Pelicula> query = _db.Pelicula;

        if (!string.IsNullOrEmpty(nombre))
        {
            query = query.Where(e => e.Name.Contains(nombre) || e.Description.Contains(nombre));
        }

        return query.ToList();
    }

    public bool Guardar()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }
}