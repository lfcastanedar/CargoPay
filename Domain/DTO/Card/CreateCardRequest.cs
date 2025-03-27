using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Core.DTO.Card;

public class CreateCardRequest
{
    [Required]
    [RegularExpression(@"^\d{15}$", ErrorMessage = "It must be a 15-digit number")]
    public string CardNumber { get; set; }
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Initial balance must be greater than 0")]
    public decimal? InitialBalance { get; set; }
}