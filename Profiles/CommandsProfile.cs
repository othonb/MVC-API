using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{

  public class CommandsProfile : Profile
  {

    public CommandsProfile()
    {
        // Source -> Destination
        // A Command is read and a CommandReadDto is obtained
        CreateMap<Command, CommandReadDto>();

        // A CommandCreateDto is read and a Command is obtained
        CreateMap<CommandCreateDto, Command>();

        // A CommandUpdateDto is read and a Command is obtained
        CreateMap<CommandUpdateDto, Command>();

    }

  }
}