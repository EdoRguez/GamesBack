using FluentResults;

namespace GamesBack.Application.Common.Errors;

public class NotFoundError : IError
{
    public List<IError> Reasons => new();
    public string Message => "No record was found";
    public Dictionary<string, object> Metadata => new();
}
