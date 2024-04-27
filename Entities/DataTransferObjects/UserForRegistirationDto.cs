﻿using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record UserForRegistirationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }

        [Required(ErrorMessage = "Username is required.")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; init; }

        public string? Email { get; init; }
        public bool? IsAvailable { get; init; }

        public int? GenderId { get; set; }

        public int? RankId { get; set; }

        public ICollection<string>? Roles { get; init; }
    }
}
