using System.Diagnostics.CodeAnalysis;
using Web.Core.Models;

namespace Web.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<User?> Authenticate([NotNull] string login, string password);
}
