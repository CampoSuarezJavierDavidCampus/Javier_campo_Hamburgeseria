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
public class HamburguesaController : BaseApiController{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public HamburguesaController (IUnitOfWork unitOfWork,IMapper mapper){
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<HamburguesaSimpleDto>> Get(){
       var records = await _UnitOfWork.Hamburguesas.GetAllAsync();
       return _Mapper.Map<List<HamburguesaSimpleDto>>(records);
    }

    [HttpGet("{id}")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaSimpleDto>> Get(int id){
       var record = await _UnitOfWork.Hamburguesas.GetByIdAsync(id);
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<HamburguesaSimpleDto>(record);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<HamburguesaSimpleDto>>> Get11([FromQuery] IParam param){
       var records = await _UnitOfWork.Hamburguesas.GetAllAsync(null,param);
       var recordDtos = _Mapper.Map<List<HamburguesaSimpleDto>>(records);
       IPager<HamburguesaSimpleDto> pager = new Pager<HamburguesaSimpleDto>(recordDtos,records.Count(),param) ;
        return CreatedAtAction("Hamburguesas",pager);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaSimpleDto>> Post(HamburguesaSimpleDto recordDto){
       var record = _Mapper.Map<Hamburguesa>(recordDto);
       _UnitOfWork.Hamburguesas.Add(record);
       await _UnitOfWork.SaveAsync();
       if (record == null){
           return BadRequest();
       }
       return CreatedAtAction(nameof(Post),new {id= record.Id, recordDto});
    }

    [HttpPut]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaSimpleDto>> Put([FromBody]HamburguesaSimpleDto recordDto){
       if(recordDto == null)
           return NotFound();
       var record = _Mapper.Map<Hamburguesa>(recordDto);
       _UnitOfWork.Hamburguesas.Update(record);
       await _UnitOfWork.SaveAsync();
       return recordDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
       var record = await _UnitOfWork.Hamburguesas.GetByIdAsync(id);
       if(record == null){
           return NotFound();
       }
       _UnitOfWork.Hamburguesas.Remove(record);
       await _UnitOfWork.SaveAsync();
       return NoContent();
    }
}