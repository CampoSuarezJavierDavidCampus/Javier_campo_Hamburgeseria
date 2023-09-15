using API.Controllers;
using API.Dtos;
using AutoMapper;
using API.Helpers;
using Dominio.Interfaces;
using Dominio.Interfaces.Pager;
using Microsoft.AspNetCore.Mvc;
using Dominio.Entities;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.1")]
[ApiVersion("1.0")]
public class CategoriaController : BaseApiController{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public CategoriaController (IUnitOfWork unitOfWork,IMapper mapper){
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<CategoriaDto>> Get(){
       var records = await _UnitOfWork.Categorias.GetAllAsync();
       return _Mapper.Map<List<CategoriaDto>>(records);
    }

    [HttpGet("{id}")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> Get(int id){
       var record = await _UnitOfWork.Categorias.GetByIdAsync(id);
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<CategoriaDto>(record);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CategoriaDto>>> Get11([FromQuery] Params conf){
        var param = new Param(conf);
       var records = await _UnitOfWork.Categorias.GetAllAsync(null,param);
       var recordDtos = _Mapper.Map<List<CategoriaDto>>(records);
       IPager<CategoriaDto> pager = new Pager<CategoriaDto>(recordDtos,records.Count(),param) ;
        return CreatedAtAction("Categorias",pager);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> Post(CategoriaDto recordDto){
       var record = _Mapper.Map<Categoria>(recordDto);
       _UnitOfWork.Categorias.Add(record);
       await _UnitOfWork.SaveAsync();
       if (record == null){
           return BadRequest();
       }
       return CreatedAtAction(nameof(Post),new {id= record.Id, recordDto});
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> Put(int id, [FromBody]CategoriaDto recordDto){
       if(recordDto == null)
           return NotFound();
       var record = _Mapper.Map<Categoria>(recordDto);
       record.Id = id;
       _UnitOfWork.Categorias.Update(record);
       await _UnitOfWork.SaveAsync();
       return recordDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
       var record = await _UnitOfWork.Categorias.GetByIdAsync(id);
       if(record == null){
           return NotFound();
       }
       _UnitOfWork.Categorias.Remove(record);
       await _UnitOfWork.SaveAsync();
       return NoContent();
    }
}