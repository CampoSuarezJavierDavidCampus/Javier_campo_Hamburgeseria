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
public class ChefController : BaseApiController{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public ChefController (IUnitOfWork unitOfWork,IMapper mapper){
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<ChefDto>> Get(){
       var records = await _UnitOfWork.Chefs.GetAllAsync();
       return _Mapper.Map<List<ChefDto>>(records);
    }

    [HttpGet("Carnes")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<ChefDto>> Carnes(){
       var records = await _UnitOfWork.Chefs.GetAllAsync(x => x.Especialidad.ToLower() == "carnes");
       return _Mapper.Map<List<ChefDto>>(records);
    }

    [HttpGet("{id}")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ChefDto>> Get(int id){
       var record = await _UnitOfWork.Chefs.GetByIdAsync(id);
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<ChefDto>(record);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ChefDto>>> Get11([FromQuery] Params conf){
        var param = new Param(conf);
       var records = await _UnitOfWork.Chefs.GetAllAsync(null,param);
       var recordDtos = _Mapper.Map<List<ChefDto>>(records);
       IPager<ChefDto> pager = new Pager<ChefDto>(recordDtos,records.Count(),param) ;
         return Ok(pager);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ChefDto>> Post(ChefDto recordDto){
       var record = _Mapper.Map<Chef>(recordDto);
       _UnitOfWork.Chefs.Add(record);
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
    public async Task<ActionResult<ChefDto>> Put(int id, [FromBody]ChefDto recordDto){
       if(recordDto == null)
           return NotFound();
       var record = _Mapper.Map<Chef>(recordDto);
       record.Id = id;
       _UnitOfWork.Chefs.Update(record);
       await _UnitOfWork.SaveAsync();
       return recordDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
       var record = await _UnitOfWork.Chefs.GetByIdAsync(id);
       if(record == null){
           return NotFound();
       }
       _UnitOfWork.Chefs.Remove(record);
       await _UnitOfWork.SaveAsync();
       return NoContent();
    }
}