namespace LeCiel.DTOs.Responses;

public class GenericResponse<T>(bool status, string? message, T? payload)
{
    public bool Status { get; set; } = status;
    public string? Message { get; set; } = message;
    public T? Payload { get; set; } = payload;
}
