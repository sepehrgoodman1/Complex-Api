namespace Taav.Contracts.UserManagements.Dto;

public interface Token
{
    public string Access_Token { get; }
    public int Expires_In { get; }
    public string Token_Type { get; }
    public string Scope { get; }
    public string ToString();
}

public class TokenDto : Token
{
    public string Access_Token { get; set; }
    public int Expires_In { get; set; }
    public string Token_Type { get; set; }
    public string Scope { get; set; }

    public override string ToString()
    {
        return $"Bearer {Access_Token}";
    }
}