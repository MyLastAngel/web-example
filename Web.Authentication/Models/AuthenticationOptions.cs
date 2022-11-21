using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web.Authentication.Models;

public class AuthenticationOptions
{
    #region Свойства

    public string Issuer { get; set; } = default!;

    public string Audience { get; set; } = default!;

    public string SecretKey { get; set; } = default!;

    public int LifetimeMin { get; set; }

    public bool IsUseHttps { get; set; } = true;
    #endregion


    public SymmetricSecurityKey GetSymmetricKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
    }
}
