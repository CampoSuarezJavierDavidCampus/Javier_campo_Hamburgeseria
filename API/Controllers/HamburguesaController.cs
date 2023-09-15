using API.Controllers;
using API.Dtos;
using AutoMapper;
using API.Helpers;
using Dominio.Interfaces;
using Dominio.Interfaces.Pager;
using Microsoft.AspNetCore.Mvc;
using Dominio.Entities;
using API.Dtos.Hamburguesa;

namespace ApiIncidencias.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

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

    [HttpGet("HamburguesaConTodo")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<HamburguesaConTodoDto>> HamburguesaConTodo(){
       var records = await _UnitOfWork.Hamburguesas.GetAllAsync();
       return _Mapper.Map<List<HamburguesaConTodoDto>>(records);
    }

    [HttpGet("OdenadaPorPrecio")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<HamburguesaConTodoDto>> PorPrecio(){
       var records = (await _UnitOfWork.Hamburguesas.GetAllAsync()).OrderByDescending(x => x.Precio);
       return _Mapper.Map<List<HamburguesaConTodoDto>>(records);
    }

    [HttpGet("vegetariana")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<HamburguesaConTodoDto>> Vegetariana(){
       var records = await _UnitOfWork.Hamburguesas.GetAllAsync(x => x.Categoria.Nombre.ToLower() == "vegetariana");
       return _Mapper.Map<List<HamburguesaConTodoDto>>(records);
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

    [HttpGet("chef/{nombre}")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<HamburguesaConTodoDto>>> ChefName(string nombre){
       var record = await _UnitOfWork.Hamburguesas.GetAllAsync(x => x.Chef.Nombre.ToLower() == nombre.ToLower());
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<List<HamburguesaConTodoDto>>(record);
    }

    [HttpGet("precio")]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<HamburguesaConTodoDto>>> Precio(){
       var record = await _UnitOfWork.Hamburguesas.GetAllAsync(x => x.Precio <= 9000);
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<List<HamburguesaConTodoDto>>(record);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<HamburguesaSimpleDto>>> Get11([FromQuery] Params conf ){
        var param = new Param(conf);
       var records = await _UnitOfWork.Hamburguesas.GetAllAsync(null,param);
       Console.WriteLine(records.Count());
       var recordDtos = _Mapper.Map<List<HamburguesaSimpleDto>>(records);
       IPager<HamburguesaSimpleDto> pager = new Pager<HamburguesaSimpleDto>(recordDtos,records.Count(),param) ;
        
        return Ok(pager);
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

    [HttpPost("ingrediente/add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(HamburguesaAddIngrediente HI_IDs){

       Hamburguesa hamburguesa = await _UnitOfWork.Hamburguesas.GetByIdAsync(HI_IDs.IdHamburguesa);
       Ingrediente ingrediente = await _UnitOfWork.Ingredientes.GetByIdAsync(HI_IDs.IdIngrediente);
       if(hamburguesa is null){
        return BadRequest("no se encontro la hamburguesa");
       }else if(ingrediente is null){
        return BadRequest("no se encontro el ingrediente");
       }
       hamburguesa.Ingrediente.Add(ingrediente);
       _UnitOfWork.Hamburguesas.Update(hamburguesa);
       await _UnitOfWork.SaveAsync();
       return Ok($"se agrego {ingrediente.Nombre} a la hamburguesa {hamburguesa.Nombre}");
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaSimpleDto>> Put(int id, [FromBody]HamburguesaSimpleDto recordDto){
       if(recordDto == null)
           return NotFound();
       var record = _Mapper.Map<Hamburguesa>(recordDto);
       record.Id = id;
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