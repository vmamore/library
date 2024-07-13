using System.Text.Json.Serialization;

namespace Library.Tests.AcceptanceTests.Dtos;

public enum Roles
{
    Locator = 1,
    Librarian
}

public class AuthenticationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
    private DateTime CreatedAt { get; } = DateTime.Now;

    public DateTime AccessTokenExpireTime
    {
        get
        {
            return CreatedAt.AddSeconds(ExpiresIn);
        }
    }
}