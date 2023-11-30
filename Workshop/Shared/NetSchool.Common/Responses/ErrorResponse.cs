namespace NetSchool.Common.Responses;

public class ErrorResponse
{
    public string Code { get; set; }
    public string Message { get; set; }
    public IEnumerable<ErrorResponseFieldInfo> FieldErrors { get; set; }
}

public class ErrorResponseFieldInfo
{
    public string Code { get; set; }
    public string FieldName { get; set; }
    public string Message { get; set; }
}