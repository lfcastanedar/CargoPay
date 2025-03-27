using Domain.Common;

namespace Infraestructure.Core.Entities;

public class CardEntity: BaseAuditableEntity
{
    public string CardNumber { get; set; }
    
    public decimal Balance { get; set; }
}