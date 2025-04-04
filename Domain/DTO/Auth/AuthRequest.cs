﻿using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Auth;

public class AuthRequest
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}