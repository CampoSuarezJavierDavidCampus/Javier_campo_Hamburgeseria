using API.Controllers;
using API.Dtos;
using AutoMapper;
using API.Helpers;
using Dominio.Interfaces;
using Dominio.Interfaces.Pager;
using Microsoft.AspNetCore.Mvc;
using Dominio.Entities;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class IngredienteController : BaseApiController{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public IngredienteController (IUnitOfWork unitOfWork,IMapper mapper){
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<IngredienteDto>> Get(){
       var records = await _UnitOfWork.Ingredientes.GetAllAsync();
       return _Mapper.Map<List<IngredienteDto>>(records);
    }

    [HttpGet("precio2o5Mil")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<IngredienteDto>> Precio(){
       var records = await _UnitOfWork.Ingredientes.GetAllAsync(x=> x.Precio <= 5000 && x.Precio >= 2000);
       return _Mapper.Map<List<IngredienteDto>>(records);
    }

    [HttpGet("mascaro")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IngredienteDto> MasCaro(){
       var records = (await _UnitOfWork.Ingredientes.GetAllAsync()).MaxBy(x => x.Precio);
       return _Mapper.Map<IngredienteDto>(records);
    }

    [HttpGet("menorStock")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<IngredienteDto>> MenorStock(){
       var records = await _UnitOfWork.Ingredientes
       .GetAllAsync(x => x.Stock < 400);
       return _Mapper.Map<List<IngredienteDto>>(records);
    }

    [HttpGet("{id}")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredienteDto>> Get(int id){
       var record = await _UnitOfWork.Ingredientes.GetByIdAsync(id);
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<IngredienteDto>(record);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<IngredienteDto>>> Get11([FromQuery] Params conf){
        var param = new Param(conf);
       var records = await _UnitOfWork.Ingredientes.GetAllAsync(null,param);
       var recordDtos = _Mapper.Map<List<IngredienteDto>>(records);
       IPager<IngredienteDto> pager = new Pager<IngredienteDto>(recordDtos,records.Count(),param) ;
         return Ok(pager);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredienteDto>> Post(IngredienteDto recordDto){
       var record = _Mapper.Map<Ingrediente>(recordDto);
       _UnitOfWork.Ingredientes.Add(record);
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
    public async Task<ActionResult<IngredienteDto>> Put(int id, [FromBody]IngredienteDto recordDto){
       if(recordDto == null)
           return NotFound();
       var record = _Mapper.Map<Ingrediente>(recordDto);
       record.Id = id;
       _UnitOfWork.Ingredientes.Update(record);
       await _UnitOfWork.SaveAsync();
       return recordDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
       var record = await _UnitOfWork.Ingredientes.GetByIdAsync(id);
       if(record == null){
           return NotFound();
       }
       _UnitOfWork.Ingredientes.Remove(record);
       await _UnitOfWork.SaveAsync();
       return NoContent();
    }
}