using System.Collections.Generic;
using RestAPI.Models;
using RestAPI.Data;
using RestAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var items = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(items));
        }

        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int Id)
        {
            var item = _repository.GetCommandById(Id);
            if(item != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(item));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto cmdCreateDto)
        {
            var cmdModel = _mapper.Map<Command>(cmdCreateDto);
            _repository.CreateCommand(cmdModel);
            
            _repository.SaveChanges();
            
            var cmdReadDto = _mapper.Map<CommandReadDto>(cmdModel);
            // return Ok(cmdReadDto);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = cmdReadDto.Id}, cmdReadDto);
        }
        
        [HttpPut("{id}")]
        public ActionResult <CommandReadDto> UpdateCommand(int id, CommandUpdateDto cmdUpdateDto)
        {
            var cmdMdlFromRepo = _repository.GetCommandById(id);
            if(cmdMdlFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(cmdUpdateDto, cmdMdlFromRepo);

            _repository.UpdateCommand(cmdMdlFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var cmdMdlFromRepo = _repository.GetCommandById(id);
            if(cmdMdlFromRepo == null)
            {
                return NotFound();
            }

            var cmdPatch = _mapper.Map<CommandUpdateDto>(cmdMdlFromRepo);
            patchDoc.ApplyTo(cmdPatch, ModelState);

            if(!TryValidateModel(cmdPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(cmdPatch, cmdMdlFromRepo);
            _repository.UpdateCommand(cmdMdlFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var cmdMdlFromRepo = _repository.GetCommandById(id);
            if(cmdMdlFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(cmdMdlFromRepo);
            _repository.SaveChanges();
            return NoContent();
            
        }

    }
}