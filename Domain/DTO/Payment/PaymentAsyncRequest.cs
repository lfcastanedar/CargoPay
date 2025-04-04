﻿using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Payment;

public class PaymentAsyncRequest
{
    [Required]
    [RegularExpression(@"^\d{15}$", ErrorMessage = "It must be a 15-digit number")]
    public string CardNumber { get; set; }
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
}