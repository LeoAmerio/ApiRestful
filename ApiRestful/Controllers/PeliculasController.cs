using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Repository.IRepository;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeliculasController : ControllerBase
{
    private readonly IPeliculaRepositorio _pelRepo;
    private readonly IMapper _mapper;

    public PeliculasController(IPeliculaRepositorio pelRepo, IMapper mapper)
    {
        _pelRepo = pelRepo;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetPeliculas()
    {
        var listPeliculas = _pelRepo.GetPeliculas();

        var listPeliculasDto = new List<PeliculaDto>();

        foreach (var lista in listPeliculas)
        {
            listPeliculasDto.Add(_mapper.Map<PeliculaDto>(lista));
        }

        return Ok(listPeliculasDto);
    }
    
    [HttpGet("{peliculaId:int}", Name = "GetPelicula")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetPelicula(int peliculaId)
    {
        var itemPelicula = _pelRepo.GetPelicula(peliculaId);

        if (itemPelicula == null)
        {
            return NotFound();
        }

        var itemPeliculaDto = _mapper.Map<PeliculaDto>(itemPelicula);

        return Ok(itemPeliculaDto);
    }
    
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(PeliculaDto))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CrearPelicula([FromBody] PeliculaDto peliculaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (peliculaDto == null)
        {
            return BadRequest(ModelState);
        }

        if (_pelRepo.ExistePelicula(peliculaDto.Name))
        {
            ModelState.AddModelError("", "La pelicula ya existe");
            return StatusCode(404, ModelState);
        }

        var pelicula = _mapper.Map<Pelicula>(peliculaDto);

        if (!_pelRepo.CrearPelicula(pelicula))
        {
            ModelState.AddModelError("", $"Algo salio mal guardando el registro {pelicula.Name}");
            return StatusCode(404, ModelState);
        }

        return CreatedAtRoute("GetPelicula", new { peliculaId = pelicula.Id }, pelicula);
    }
    
    [HttpPatch("{PeliculaId:int}", Name = "ActualizarPatchPelicula")]
    [ProducesResponseType(204)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ActualizarPatchPelicula(int peliculaId, [FromBody] PeliculaDto peliculaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (peliculaDto == null || peliculaId != peliculaDto.Id)
        {
            return BadRequest(ModelState);
        }

        var pelicula = _mapper.Map<Pelicula>(peliculaDto);

        if (!_pelRepo.ActualizarPelicula(pelicula))
        {
            ModelState.AddModelError("", $"Algo salio mal actualizando el registro {pelicula.Nombre}");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
}