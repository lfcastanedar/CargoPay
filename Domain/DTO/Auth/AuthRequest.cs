﻿using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Core.DTO.Auth;

public class AuthRequest
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}