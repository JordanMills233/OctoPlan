namespace OctoPlan.Core.Models.Requests;

public record ConfirmEmailRequest(string email, string verificationCode);