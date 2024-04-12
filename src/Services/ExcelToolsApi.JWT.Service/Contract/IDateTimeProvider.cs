namespace ExcelToolsApi.JWT.Service;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
