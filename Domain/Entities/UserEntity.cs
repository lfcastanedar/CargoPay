using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Users")]
public class UserEntity
{
    [Key]
    public int Id { get; set; }
    
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
    public ICollection<CardEntity> Cards { get; set; }
}