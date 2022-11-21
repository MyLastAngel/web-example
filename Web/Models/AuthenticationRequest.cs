using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models;

public class AuthenticationRequest
{
    [Required]
    [NotNull]
    public string Login { get; set; } = default!;

    public string Password { get; set; } = default!;
}
