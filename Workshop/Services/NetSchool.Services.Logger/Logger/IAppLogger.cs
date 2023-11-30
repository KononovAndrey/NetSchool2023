namespace NetSchool.Services.Logger;

using Serilog.Events;

public interface IAppLogger
{
    void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues);
    void Write(LogEventLevel level, object module, string messageTemplate, params object[] propertyValues);

    void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues);

    void Write(LogEventLevel level, Exception exception, object module, string messageTemplate,
        params object[] propertyValues);

    void Verbose(string messageTemplate, params object[] propertyValues);
    void Verbose(object module, string messageTemplate, params object[] propertyValues);

    void Debug(string messageTemplate, params object[] propertyValues);
    void Debug(object module, string messageTemplate, params object[] propertyValues);

    void Information(string messageTemplate, params object[] propertyValues);
    void Information(object module, string messageTemplate, params object[] propertyValues);

    void Warning(string messageTemplate, params object[] propertyValues);
    void Warning(object module, string messageTemplate, params object[] propertyValues);

    void Error(string messageTemplate, params object[] propertyValues);
    void Error(object module, string messageTemplate, params object[] propertyValues);

    void Error(Exception exception, string messageTemplate, params object[] propertyValues);
    void Error(Exception exception, object module, string messageTemplate, params object[] propertyValues);

    void Fatal(string messageTemplate, params object[] propertyValues);
    void Fatal(object module, string messageTemplate, params object[] propertyValues);

    void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);
    void Fatal(Exception exception, object module, string messageTemplate, params object[] propertyValues);
}