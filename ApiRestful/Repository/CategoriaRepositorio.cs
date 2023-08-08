using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository.IRepository;

namespace WebApplication1.Repository;

public class CategoriaRepositorio : ICategoriaRepositorio
{
    private readonly ApplicationDbContext _db;

    public CategoriaRepositorio(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public ICollection<Category> GetCategorias()
    {
        return _db.Categorias.OrderBy(c => c.Nombre).ToList();
    }

    public Category GetCategoria(int categoriaId)
    {
        return _db.Categorias.FirstOrDefault(c => c.Id == categoriaId);
    }

    public bool ExisteCategoria(string name)
    {
        bool value = _db.Categorias.Any(c => c.Nombre.ToLower().Trim() == name.ToLower().Trim());
        return value;
    }

    public bool ExisteCategoria(int id)
    {
        return _db.Categorias.Any(c => c.Id == id);
    }

    public bool CrearCategoria(Category categoria)
    {
        categoria.FechaCreacion = DateTime.Now;
        _db.Categorias.Add(categoria);
        return Guardar();
    }

    public bool ActualizarCategoria(Category categoria)
    {
        categoria.FechaCreacion = DateTime.Now;
        _db.Categorias.Update(categoria);
        return Guardar();
    }

    public bool EliminarCategoria(Category categoria)
    {
        _db.Categorias.Remove(categoria);
        return Guardar();
    }

    public bool Guardar()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }
}