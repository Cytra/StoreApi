namespace Application.Models;

public class ErrorResponse
{
    public List<Error> Errors { get; set; } = new();
}

public class Error
{
    public string Property { get; set; }
    public string ErrorReason { get; set; }
}