using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

[Table("Cards")]
public class CardEntity: BaseAuditableEntity
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(16)]
    public string CardNumber { get; set; }
    
    public decimal Balance { get; set; }
    
    public int UserId { get; set; }
    public UserEntity User { get; set; }

    public ICollection<TransactionEntity> Transactions = new List<TransactionEntity>();
}