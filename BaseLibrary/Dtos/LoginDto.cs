﻿using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Dtos
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}