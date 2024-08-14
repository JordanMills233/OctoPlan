namespace OctoPlan.Core.Models.Responses;

public record LoginResponse(bool Flag, string Message, string Token = null);