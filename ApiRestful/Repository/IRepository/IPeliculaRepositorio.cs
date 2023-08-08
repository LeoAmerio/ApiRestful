using WebApplication1.Models;

namespace WebApplication1.Repository.IRepository;

public interface IPeliculaRepositorio
{
    ICollection<Pelicula> GetPeliculas();
    Pelicula GetPelicula(int peliculaId);
    bool ExistePelicula(string name);
    bool ExistePelicula(int id);
    bool CrearPelicula(Pelicula pelicula);
    bool ActualizarPelicula(Pelicula pelicula);
    bool EliminarPelicula(Pelicula pelicula);
    
    // Metodos para buscar peliculas en categoria y buscar pelicula por nombre
    ICollection<Pelicula> GetPeliculaByCategory(int id);
    ICollection<Pelicula> GetPeliculaByName(string nombre);
    
    bool Guardar();

}