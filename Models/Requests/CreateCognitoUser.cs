namespace OctoPlan.Core.Models.Requests;

public record CreateCognitoUser()
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Sub { get; set; }
}