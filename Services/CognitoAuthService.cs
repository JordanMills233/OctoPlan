using System.Security.Cryptography;
using System.Text;

namespace OctoPlan.Core.Services;

using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

public class CognitoAuthService
{
    private readonly IAmazonCognitoIdentityProvider _cognitoProvider;
    private readonly IConfiguration _configuration;

    public CognitoAuthService(IAmazonCognitoIdentityProvider cognitoProvider, IConfiguration configuration)
    {
        _cognitoProvider = cognitoProvider;
        _configuration = configuration;
    }

    public async Task<AuthenticationResultType> SignInAsync(string username, string password)
    {
        try
        {
            var request = new InitiateAuthRequest()
            {
                ClientId = _configuration["Cognito:ClientId"],
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    {"PASSWORD", password},
                    {"USERNAME", username},
                    {"SECRET_HASH", CalculateSecretHash(username)}
                }
            };
            

            var response = await _cognitoProvider.InitiateAuthAsync(request);
            return response.AuthenticationResult;
        }
        catch (NotAuthorizedException)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        catch (Exception ex)
        {
            throw new Exception("An error occured while signing in: " + ex);
        }
    }
    
    private string CalculateSecretHash(string username)
    {
        var message = Encoding.UTF8.GetBytes(username + _configuration["Cognito:ClientId"]);
        var key = Encoding.UTF8.GetBytes(_configuration["Cognito:ClientSecret"]);

        using (var hmac = new HMACSHA256(key))
        {
            var hash = hmac.ComputeHash(message);
            return Convert.ToBase64String(hash);
        }
    }

    public async Task<SignUpResponse> SignUpAsync(string email, string password, string firstName, string lastName)
    {
        var request = new SignUpRequest
        {
            ClientId = _configuration["Cognito:ClientId"],
            Username = email,
            Password = password,
            SecretHash = CalculateSecretHash(email),
            UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "email", Value = email },
                new AttributeType { Name = "given_name", Value = firstName },
                new AttributeType { Name = "family_name", Value = lastName }
            }
        };
        
        return await _cognitoProvider.SignUpAsync(request);
    }

    public async Task<ConfirmSignUpResponse> ConfirmSignUpAsync(string email, string confirmationCode)
    {
        var request = new ConfirmSignUpRequest
        {
            ClientId = _configuration["Cognito:ClientId"],
            Username = email,
            ConfirmationCode = confirmationCode,
            SecretHash = CalculateSecretHash(email)
        };
        
        return await _cognitoProvider.ConfirmSignUpAsync(request);
    }
}


