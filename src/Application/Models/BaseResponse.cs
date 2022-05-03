namespace Application.Models;

public class BaseResponse
{
    public bool IsValid => Errors.Count == 0;
    public List<Error> Errors { get; set; } = new();
}