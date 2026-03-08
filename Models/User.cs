using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkhsanovAPI.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int userId { get; set; }
    public string fullname { get; set; }
    public string description { get; set; }
    public string email { get; set; }
    
    [Required]
    [ForeignKey("Role")]
    public int roleId { get; set; }
    public Role role { get; set; }
    
}