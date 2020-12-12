using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{

  public class Command : System.ICloneable
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string HowTo { get; set; }

    [Required]
    [MaxLength(100)]
    public string Line { get; set; }

    [Required]
    [MaxLength(100)]
    public string Platform { get; set; }

    public object Clone()
    {
      return this.MemberwiseClone();
    }
  }

}