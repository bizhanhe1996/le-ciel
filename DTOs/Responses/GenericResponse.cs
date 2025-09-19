using LeCiel.Extras.Structs;

namespace LeCiel.DTOs.Responses;

public class GenericResponse<T>(bool status, T? payload, PaginationStruct? pagination = null)
{
    public bool Status { get; set; } = status;
    public T? Payload { get; set; } = payload;
    public PaginationStruct? Pagination = pagination;
}
