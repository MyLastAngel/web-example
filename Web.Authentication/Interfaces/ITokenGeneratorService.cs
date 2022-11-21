using Web.Core.Models;

namespace Web.Authentication;

public interface ITokenGeneratorService
{
    string Generate(User user);
}
