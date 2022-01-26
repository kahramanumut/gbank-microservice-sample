public class Result<T> 
{
    public Result(T data, bool isSuccess, string message)
    {
        Data = data;
        IsSuccess = isSuccess;
        Message = message;
    }

    public T Data { get; private set; }
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
}

public class Result 
{
    public Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
}