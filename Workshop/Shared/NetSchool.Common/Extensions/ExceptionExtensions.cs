namespace NetSchool.Common.Extensions;

using Exceptions;
using Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public static class ErrorResponseExtensions
{
    /// <summary>
    /// Convert process exception to ErrorResponse
    /// </summary>
    /// <param name="data">Process exception</param>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this ProcessException data)
    {
        var code = "";
        var message = "";

        var res = new ErrorResponse()
        {
            Code = data.Code ?? code,
            Message = data.Message ?? message
        };

        return res;
    }

    /// <summary>
    /// Convert exception to ErrorResponse
    /// </summary>
    /// <param name="data">Exception</param>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this Exception data)
    {
        var code = "";
        var message = "";

        var res = new ErrorResponse()
        {
            Code = code,
            Message = data.Message ?? message
        };

        return res;
    }

    /// <summary>
    /// Convert forbid exception to ErrorResponse
    /// </summary>
    /// <param name="data">Exception</param>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this ForbidAccessException data)
    {
        var code = "";
        var message = "";

        var res = new ErrorResponse()
        {
            Code = data.Code ?? code,
            Message = data.Message ?? message
        };

        return res;
    }

    /// <summary>
    /// Convert validation exception to ErrorResponse
    /// </summary>
    /// <param name="data">Validation Exception</param>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this ValidationException data)
    {
        var code = "";
        var message = "";

        var res = new ErrorResponse()
        {
            Code = code,
            Message = message,
            FieldErrors = data.Errors.Select(e => new ErrorResponseFieldInfo()
            {
                Code = e.ErrorCode,
                FieldName = e.PropertyName,
                Message = e.ErrorMessage
            })
        };

        return res;
    }

    /// <summary>
    /// Convert ModelState to ErrorResponse
    /// </summary>
    /// <param name="data">ModelStateDictionary</param>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this ModelStateDictionary data)
    {
        var fieldErrors = new List<ErrorResponseFieldInfo>();
        foreach (var (field, state) in data)
            if (state.ValidationState == ModelValidationState.Invalid)
                fieldErrors.Add(new ErrorResponseFieldInfo()
                {
                    Code = "",
                    FieldName = field.ToCamelCase(),
                    Message = string.Join(", ", state.Errors.Select(x => x.ErrorMessage))
                });

        var res = new ErrorResponse()
        {
            Code = "",
            Message = "",
            FieldErrors = fieldErrors
        };

        return res;
    }
}