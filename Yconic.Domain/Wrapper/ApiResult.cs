using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Wrapper;

public class ApiResult<TData>
{
    public TData Data { get; set; }
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> Errors { get; set; }

    public int? SuccessCount { get; set; }
    public int? ErrorCount { get; set; }

    public static ApiResult<TData> Success(TData data, int? successCount = null, int? errorCount = null,
        List<string> errors = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ApiResult<TData>
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccess = true,
            Errors = errors,
            SuccessCount = successCount,
            ErrorCount = errorCount
        };
    }

    public static ApiResult<TData> Success(List<string> errors = null,
        HttpStatusCode statusCode = HttpStatusCode.NoContent)
    {
        return new ApiResult<TData>
        {
            Data = default,
            StatusCode = statusCode,
            Errors = errors,
            IsSuccess = true
        };
    }

    public static ApiResult<TData> Fail(List<string> errors = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ApiResult<TData>
        {
            Data = default,
            StatusCode = statusCode,
            Errors = errors,
            IsSuccess = false
        };
    }

    public static ApiResult<TData> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ApiResult<TData>
        {
            Data = default,
            StatusCode = statusCode,
            Errors = new List<string> { message },
            IsSuccess = false
        };
    }

    public static ApiResult<TData> ServerError(string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        return new ApiResult<TData>
        {
            Data = default,
            StatusCode = statusCode,
            Errors = new List<string> { message },
            IsSuccess = false
        };
    }

    public static ApiResult<TData> ValidationError(List<string> errors,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ApiResult<TData>
        {
            Data = default,
            StatusCode = statusCode,
            Errors = errors,
            IsSuccess = false
        };
    }

    public static ApiResult<TData> ValidationError(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ApiResult<TData>
        {
            Data = default,
            StatusCode = statusCode,
            Errors = [error],
            IsSuccess = false
        };
    }
}