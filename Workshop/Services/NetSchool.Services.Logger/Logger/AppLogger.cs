namespace NetSchool.Services.Logger;

using Serilog.Events;

public class AppLogger : IAppLogger
{
    private readonly Serilog.ILogger logger;

    private string _systemName = "NetSchool";

    public AppLogger(Serilog.ILogger logger)
    {
        this.logger = logger;
    }

    private string consructMessageTemplate(string messageTemplate, object module = null)
    {
        var moduleName = module?.GetType().Name;

        if (string.IsNullOrEmpty(moduleName))
            return $"[{_systemName}] {messageTemplate} ";
        else
            return $"[{_systemName}:{module}] {messageTemplate} ";
    }

    public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
    {
        logger?.Write(level, consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Write(LogEventLevel level, object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Write(level, consructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Write(LogEventLevel level, Exception exception, string messageTemplate,
        params object[] propertyValues)
    {
        logger?.Write(level, exception, consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Write(LogEventLevel level, Exception exception, object module, string messageTemplate,
        params object[] propertyValues)
    {
        logger?.Write(level, exception, consructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Verbose(string messageTemplate, params object[] propertyValues)
    {
        logger?.Verbose(consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Verbose(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Verbose(consructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Debug(string messageTemplate, params object[] propertyValues)
    {
        logger?.Debug(consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Debug(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Debug(consructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Information(string messageTemplate, params object[] propertyValues)
    {
        logger?.Information(consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Information(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Information(consructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Warning(string messageTemplate, params object[] propertyValues)
    {
        logger?.Warning(consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Warning(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Warning(consructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Error(string messageTemplate, params object[] propertyValues)
    {
        logger?.Error(consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Error(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Error(consructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        logger?.Error(exception, consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Error(Exception exception, object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Error(exception, consructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Fatal(string messageTemplate, params object[] propertyValues)
    {
        logger?.Fatal(consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Fatal(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Fatal(consructMessageTemplate(messageTemplate, module), propertyValues);
    }

    public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        logger?.Fatal(exception, consructMessageTemplate(messageTemplate), propertyValues);
    }

    public void Fatal(Exception exception, object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Fatal(exception, consructMessageTemplate(messageTemplate, module), propertyValues);
    }
}