using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Transactions")]
public class TransactionEntity
{
    [Key]
    public int Id { get; set; }
    
    [Column(TypeName = "decimal(18, 7)")]
    public decimal Balance { get; set; }
    
    [Column(TypeName = "decimal(18, 7)")]
    public decimal Amount { get; set; }
    
    [Column(TypeName = "decimal(18, 7)")]
    public decimal FeeRate { get; set; }
    
    [Column(TypeName = "decimal(18, 7)")]
    public decimal Fee { get; set; }
    
    [Column(TypeName = "decimal(18, 7)")]
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CardId { get; set; }
    public CardEntity Card { get; set; }
}