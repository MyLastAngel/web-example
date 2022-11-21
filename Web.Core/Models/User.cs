namespace Web.Core.Models;

public class User
{
    #region Свойства

    public int Id { get; set; }
    public string Login { get; set; } = "";
    public string Name { get; set; } = "";
    public string Token { get; set; } = "";

    #endregion

    public User()
    {

    }

    public virtual Dictionary<string, string>? GetExternalData()
    {
        return null;
    }
}