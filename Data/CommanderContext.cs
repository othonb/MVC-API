using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{

  public class CommanderContext : DbContext
  {
    public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
    {

    }

    // This property is the table name
    // To use migrations, you must create the model and a DbSet of it
    public DbSet<Command> Commands { get; set; }

    public DbSet<Test> Tests { get; set; }

  }

}