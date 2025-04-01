using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Users")]
public class UserEntity
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(250)]
    public string Email { get; set; } = null!;
    
    [StringLength(250)]
    public string Password { get; set; } = null!;

    public int LoginAttempts { get; set; }
    
    public DateTime LastLoginAttemptDate { get; set; }


    public ICollection<CardEntity> Cards { get; set; }
}