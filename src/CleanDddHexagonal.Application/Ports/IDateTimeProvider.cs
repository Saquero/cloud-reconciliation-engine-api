namespace CleanDddHexagonal.Application.Ports;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
