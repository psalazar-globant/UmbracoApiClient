namespace UmbracoBridgeApi.Models;

public record Result<T>(T? Data, Exception? Error)
{
    public bool IsSuccess => Error is null;

    public static implicit operator Result<T>(T data) => new(data, null);

    public static implicit operator Result<T>(Exception? error) => new(default, error);

    public R Match<R>(Func<T?, R> success, Func<Exception?, R> failure) =>
        IsSuccess ? success(Data) : failure(Error);

    public static Result<T?> Success(T? data) =>
        new(data);

    public static Result<T?> Failure(Exception? error) =>
        new(default, error);
}
