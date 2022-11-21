using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web.Authentication.Models;
using Web.Core.Models;

namespace Web.Authentication.Services;

internal class JwtTokenGeneratorService : ITokenGeneratorService
{
    #region Поля

    private readonly IOptions<AuthenticationOptions> _authOptions;

    #endregion

    public JwtTokenGeneratorService(IOptions<AuthenticationOptions> authOptions)
    {
        _authOptions = authOptions ?? throw new ArgumentNullException(nameof(authOptions));
    }

    public string Generate(User response)
    {
        var opt = _authOptions.Value;
        var securityKey = opt.GetSymmetricKey();

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, response.Name),
            new Claim(JwtRegisteredClaimNames.NameId, $"{response.Id}"),
            new Claim(JwtRegisteredClaimNames.Sub, response.Token),
            new Claim("login", response.Login),
        };

        var external = response.GetExternalData();
        if (external != null)
        {
            foreach (var kv in external)
                claims.Add(new Claim(kv.Key, kv.Value));
        }

        var token = new JwtSecurityToken(
            opt.Issuer,
            opt.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(opt.LifetimeMin),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
