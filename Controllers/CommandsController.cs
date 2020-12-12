using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{

  // Name base:  api/commands
  [Route("api/[controller]")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommanderRepo _repository;
    private readonly IMapper _mapper;

    // Remember to register the dependency injection in StartUp.cs
    public CommandsController(
      ICommanderRepo repository,
      IMapper mapper
      )
    {
        _repository = repository;

        _mapper = mapper;
    }

    // Before dependency injection
    //private readonly MockCommanderRepo _repository = new MockCommanderRepo();

    // GET api/commands
    [HttpGet]
    public ActionResult <IEnumerable<Command>> GetAllCommands()
    {

      var commandItems = _repository.GetAllCommands();

      if (commandItems.ToList().Count() > 0){

        return Ok(new {
          data = _mapper.Map<IEnumerable<CommandReadDto>>(commandItems),
          status = true
        });

      } else {

        return NotFound(new {
          message = "Não há comandos cadastrados!",
          status = false
        });
      }

    }

    // GET api/commands/{id}
    // GET api/commands/1
    [HttpGet("{id}", Name = "GetCommandById")]
    public ActionResult <CommandReadDto> GetCommandById(int id)
    {

      var commandItem = _repository.GetCommandById(id);

      if (commandItem != null) {

        return Ok(new {
          data = _mapper.Map<CommandReadDto>(commandItem),
          status = true
        });

      } else {

        return NotFound(new {
          message = "Comando não encontrado!",
          status = false
        });

      }

    }

    // POST api/commands/
    [HttpPost]
    public ActionResult<CommandReadDto> PostCommand(CommandCreateDto item)
    {

      var commandModel = _mapper.Map<Command>(item);

      _repository.CreateCommand(commandModel);

      if (_repository.SaveChanges()) {

        var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

        return Ok(new {
          data = commandReadDto,
          status = true
        });

        // Return HTTP Code 201
        // return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);

      } else {

        return NotFound(new {
          message = "Não foi possível salvar o registro!",
          status = false
        });

      }

    }

    // PUT api/commands/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {
      var commandModelFromRepo = _repository.GetCommandById(id);

      var oldCommandModelFromRepo = commandModelFromRepo.Clone();

      if (commandModelFromRepo != null) {

        _mapper.Map(commandUpdateDto, commandModelFromRepo);

        _repository.UpdateCommand(commandModelFromRepo);

        if (_repository.SaveChanges()) {

          return Ok(new {
            oldValue = oldCommandModelFromRepo,
            newValue = commandModelFromRepo,
            status = true
          });
        } else {

          return NotFound(new {
            message = "Não foi possível salvar o registro!",
            status = false
          });

        }

      } else {

        return NotFound(new {
          message = "Não foi possível atualizar o registro, pois ele não existe!",
          status = false
        });
      }
    }

    // PATCH api/commands/{id}
    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
    {

      var commandModelFromRepo = _repository.GetCommandById(id);

      var oldCommandModelFromRepo = commandModelFromRepo.Clone();

      if (commandModelFromRepo != null) {

        var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);

        patchDoc.ApplyTo(commandToPatch, ModelState);

        if (TryValidateModel(commandToPatch)) {

          _mapper.Map(commandToPatch, commandModelFromRepo);

          _repository.UpdateCommand(commandModelFromRepo);

        if (_repository.SaveChanges()) {

          return Ok(new {
            oldValue = oldCommandModelFromRepo,
            newValue = commandModelFromRepo,
            status = true
          });
        } else {

          return NotFound(new {
            message = "Não foi possível salvar o registro!",
            status = false
          });

        }


        } else {

          return ValidationProblem(ModelState);
        }


      } else {

        return NotFound(new {
          message = "Não foi possível atualizar o registro, pois ele não existe!",
          status = false
        });

      }

    }

    // DELETE api/command{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {

      var oldCommandModelFromRepo = _repository.GetCommandById(id).Clone();

      _repository.DeleteCommand(id);

      if (_repository.SaveChanges()) {

        return Ok(new {
            oldValue = oldCommandModelFromRepo,
            status = true
          });
      } else {

          return NotFound(new {
            message = "Não foi possível remover o registro!",
            status = false
          });

      }
    }

  }

}