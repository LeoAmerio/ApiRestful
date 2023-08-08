using AutoMapper;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.PeliculasMapper;

public class PeliculasMapper : Profile
{
    public PeliculasMapper()
    {
        CreateMap<Category, CategoriaDto>().ReverseMap();
        CreateMap<Category, CrearCategoriaDto>().ReverseMap();
        CreateMap<Pelicula, PeliculaDto>().ReverseMap();
    }
}