namespace CarPool.Common;

public interface IResultError
{
    string Error { get; }
    string Code { get; }
}