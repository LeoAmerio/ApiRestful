using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Repository.IRepository;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoriaRepositorio _ctRepo;
    private readonly IMapper _mapper;

    public CategoryController(ICategoriaRepositorio ctRepo, IMapper mapper)
    {
        _ctRepo = ctRepo;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetCategorias()
    {
        var listCategory = _ctRepo.GetCategorias();

        var listCategoryDto = new List<CategoriaDto>();

        foreach (var lista in listCategoryDto)
        {
            listCategoryDto.Add(_mapper.Map<CategoriaDto>(lista));
        }

        return Ok(listCategoryDto);
    }

    [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetCategoria(int categoriaId)
    {
        var itemCategory = _ctRepo.GetCategoria(categoriaId);

        if (itemCategory == null)
        {
            return NotFound();
        }

        var itemCategoryDto = _mapper.Map<CategoriaDto>(itemCategory);

        return Ok(itemCategoryDto);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(CategoriaDto))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CrearCategoria([FromBody] CrearCategoriaDto crearCategoriaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (crearCategoriaDto == null)
        {
            return BadRequest(ModelState);
        }

        if (_ctRepo.ExisteCategoria(crearCategoriaDto.Nombre))
        {
            ModelState.AddModelError("", "La categoria ya existe");
            return StatusCode(404, ModelState);
        }

        var categoria = _mapper.Map<Category>(crearCategoriaDto);

        if (!_ctRepo.CrearCategoria(categoria))
        {
            ModelState.AddModelError("", $"Algo salio mal guardando el registro {categoria.Nombre}");
            return StatusCode(404, ModelState);
        }

        return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);
    }

    [HttpPatch("{CategoriaId:int}", Name = "ActualizarPatchCategoria")]
    [ProducesResponseType(201, Type = typeof(CategoriaDto))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ActualizarPatchCategory(int categoriaId, [FromBody] CategoriaDto categoriaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (categoriaDto == null || categoriaId != categoriaDto.Id)
        {
            return BadRequest(ModelState);
        }

        var categoria = _mapper.Map<Category>(categoriaDto);

        if (!_ctRepo.ActualizarCategoria(categoria))
        {
            ModelState.AddModelError("", $"Algo salio mal actualizando el registro {categoria.Nombre}");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    [HttpDelete("{CategoriaId:int}", Name = "BorrarCategory")]
    [ProducesResponseType(201, Type = typeof(CategoriaDto))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult BorrarCategory(int categoriaId)
    {
        if (!_ctRepo.ExisteCategoria(categoriaId))
        {
            return NotFound();
        }
        
        var categoria = _ctRepo.GetCategoria(categoriaId);
        if (!_ctRepo.EliminarCategoria(categoria))
        {
            ModelState.AddModelError("", $"Algo salio mal borrando el registro {categoria.Nombre}");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}