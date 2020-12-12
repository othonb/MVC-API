using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{

  public class CommandUpdateDto
  {
    [Required]
    [MaxLength(100)]
    public string HowTo { get; set; }

    [Required]
    [MaxLength(100)]
    public string Line { get; set; }

    [Required]
    [MaxLength(100)]
    public string Platform { get; set; }
  }

}