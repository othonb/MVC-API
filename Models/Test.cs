using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{

  public class Test
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Address { get; set; }

    [Required]
    [MaxLength(100)]
    public string City { get; set; }
  }
}