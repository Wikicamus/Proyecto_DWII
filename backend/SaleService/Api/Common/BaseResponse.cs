using System;

namespace SaleService.Api.Common;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static BaseResponse<T> SuccessResponse(T data, string message = "Operación exitosa")
    {
        return new BaseResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static BaseResponse<T> FailureResponse(string message, T? data = default)
    {
        return new BaseResponse<T>
        {
            Success = false,
            Message = message,
            Data = data
        };
    }
}
