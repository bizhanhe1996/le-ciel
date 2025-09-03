namespace LeCiel.DTOs.Responses;

public class GenericResponse<T>(bool status, T? payload)
{
    public bool Status { get; set; } = status;
    public T? Payload { get; set; } = payload;
}
