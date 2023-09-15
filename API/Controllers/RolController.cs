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
public class RolController : BaseApiController{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public RolController (IUnitOfWork unitOfWork,IMapper mapper){
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<RolDto>> Get(){
       var records = await _UnitOfWork.Roles.GetAllAsync();
       return _Mapper.Map<List<RolDto>>(records);
    }

    [HttpGet("{id}")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RolDto>> Get(int id){
       var record = await _UnitOfWork.Roles.GetByIdAsync(id);
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<RolDto>(record);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RolDto>>> Get11([FromQuery] Params conf){
        var param = new Param(conf);
       var records = await _UnitOfWork.Roles.GetAllAsync(null,param);
       var recordDtos = _Mapper.Map<List<RolDto>>(records);
       IPager<RolDto> pager = new Pager<RolDto>(recordDtos,records.Count(),param) ;
        return CreatedAtAction("Roles",pager);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RolDto>> Post(RolDto recordDto){
       var record = _Mapper.Map<Rol>(recordDto);
       _UnitOfWork.Roles.Add(record);
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
    public async Task<ActionResult<RolDto>> Put(int id, [FromBody]RolDto recordDto){
       if(recordDto == null)
           return NotFound();
       var record = _Mapper.Map<Rol>(recordDto);
       record.Id = id;
       _UnitOfWork.Roles.Update(record);
       await _UnitOfWork.SaveAsync();
       return recordDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
       var record = await _UnitOfWork.Roles.GetByIdAsync(id);
       if(record == null){
           return NotFound();
       }
       _UnitOfWork.Roles.Remove(record);
       await _UnitOfWork.SaveAsync();
       return NoContent();
    }
}