using WebApplication1.Models;

namespace WebApplication1.Repository.IRepository;

public interface ICategoriaRepositorio
{
    ICollection<Category> GetCategorias();
    Category GetCategoria(int categoriaId);
    bool ExisteCategoria(string name);
    bool ExisteCategoria(int id);
    bool CrearCategoria(Category categoria);
    bool ActualizarCategoria(Category categoria);
    bool EliminarCategoria(Category categoria);
    bool Guardar();

}