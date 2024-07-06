using System;
using UnityEngine;

/// <summary>
/// Modified based on https://github.com/JoanStinson/UnityLoggerExtended
/// </summary>
public static class GLogger
{
    public enum LogLevel
    {
        Info, Warning, Error
    }

    public static LogLevel logLevel = LogLevel.Info;
    public const string INFO_COLOR = nameof(Color.white);
    public const string WARNING_COLOR = nameof(Color.yellow);
    public const string ERROR_COLOR = nameof(Color.red);

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void Log(object message)
    {
        if (logLevel <= LogLevel.Info) Debug.Log(FormatMessage(LogLevel.Info, INFO_COLOR, message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void Log(string category, object message)
    {
        if (logLevel <= LogLevel.Info) Debug.Log(FormatMessageWithCategory(LogLevel.Info, INFO_COLOR, category, message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void Log(object obj, object message)
    {
        if (logLevel <= LogLevel.Info) Debug.Log(FormatMessageWithCategory(LogLevel.Info, INFO_COLOR, obj.GetType().Name, message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogFormat(string format, params object[] args)
    {
        if (logLevel <= LogLevel.Info) Debug.Log(FormatMessage(LogLevel.Info, INFO_COLOR, string.Format(format, args)));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogFormat(string category, string format, params object[] args)
    {
        if (logLevel <= LogLevel.Info) Debug.Log(FormatMessageWithCategory(LogLevel.Info, INFO_COLOR, category, string.Format(format, args)));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogFormat(object obj, string format, params object[] args)
    {
        if (logLevel <= LogLevel.Info) Debug.Log(FormatMessageWithCategory(LogLevel.Info, INFO_COLOR, obj.GetType().Name, string.Format(format, args)));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogWarning(object message)
    {
        if (logLevel <= LogLevel.Warning) Debug.LogWarning(FormatMessage(LogLevel.Warning, WARNING_COLOR, message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogWarning(string category, object message)
    {
        if (logLevel <= LogLevel.Warning) Debug.LogWarning(FormatMessageWithCategory(LogLevel.Warning, WARNING_COLOR, category, message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogWarning(object obj, object message)
    {
        if (logLevel <= LogLevel.Warning) Debug.LogWarning(FormatMessageWithCategory(LogLevel.Warning, WARNING_COLOR, obj.GetType().Name, message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogWarningFormat(string format, params object[] args)
    {
        if (logLevel <= LogLevel.Warning) Debug.LogWarningFormat(FormatMessage(LogLevel.Warning, WARNING_COLOR, string.Format(format, args)));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogWarningFormat(string category, string format, params object[] args)
    {
        if (logLevel <= LogLevel.Warning) Debug.LogWarningFormat(FormatMessageWithCategory(LogLevel.Warning, WARNING_COLOR, category, string.Format(format, args)));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogWarningFormat(object obj, string format, params object[] args)
    {
        if (logLevel <= LogLevel.Warning) Debug.LogWarningFormat(FormatMessageWithCategory(LogLevel.Warning, WARNING_COLOR, obj.GetType().Name, string.Format(format, args)));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogError(object message)
    {
        if (logLevel <= LogLevel.Error) Debug.LogError(FormatMessage(LogLevel.Error, ERROR_COLOR, message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogError(string category, object message)
    {
        if (logLevel <= LogLevel.Error) Debug.LogError(FormatMessageWithCategory(LogLevel.Error, ERROR_COLOR, category, message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogError(object obj, object message)
    {
        if (logLevel <= LogLevel.Error) Debug.LogError(FormatMessageWithCategory(LogLevel.Error, ERROR_COLOR, obj.GetType().Name, message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogErrorFormat(string format, params object[] args)
    {
        if (logLevel <= LogLevel.Error) Debug.LogErrorFormat(FormatMessage(LogLevel.Error, ERROR_COLOR, string.Format(format, args)));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogErrorFormat(string category, string format, params object[] args)
    {
        if (logLevel <= LogLevel.Error) Debug.LogErrorFormat(FormatMessageWithCategory(LogLevel.Error, ERROR_COLOR, category, string.Format(format, args)));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogErrorFormat(object obj, string format, params object[] args)
    {
        if (logLevel <= LogLevel.Error) Debug.LogErrorFormat(FormatMessageWithCategory(LogLevel.Error, ERROR_COLOR, obj.GetType().Name, string.Format(format, args)));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogException(Exception exception)
    {
        if (logLevel <= LogLevel.Error) Debug.LogError(FormatMessage(LogLevel.Error, ERROR_COLOR, exception.Message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogException(string category, Exception exception)
    {
        if (logLevel <= LogLevel.Error) Debug.LogError(FormatMessageWithCategory(LogLevel.Error, ERROR_COLOR, category, exception.Message));
    }

    // [System.Diagnostics.Conditional("DEBUG")]
    public static void LogException(object obj, Exception exception)
    {
        if (logLevel <= LogLevel.Error) Debug.LogError(FormatMessageWithCategory(LogLevel.Error, ERROR_COLOR, obj.GetType().Name, exception.Message));
    }

    public static void SetLogLevel(LogLevel level)
    {
        logLevel = level;

#if UNITY_EDITOR
        Debug.Log($"<color={INFO_COLOR}><b>Set Unity log level to [{Enum.GetName(typeof(LogLevel), level)}]</b></color>");
#else
        Debug.Log($"Set Unity log level to [{Enum.GetName(typeof(LogLevel), level)}]");
#endif
    }

    public static void SetLogLevel(string level)
    {
        SetLogLevel(GetLogLevel(level));
    }

    private static string FormatMessage(LogLevel level, string color, object message)
    {
        string levelName = GetLogLevelName(level);
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

#if UNITY_EDITOR
        return $"<color={color}><b>[{currentTime}] [{levelName}]</b> {message}</color>";
#else
        return $"[{currentTime}] [{levelName}] {message}";
#endif
    }

    private static string FormatMessageWithCategory(LogLevel level, string color, string header, object message)
    {
        string levelName = GetLogLevelName(level);
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

#if UNITY_EDITOR
        return $"<color={color}><b>[{currentTime}] [{levelName}] [{header}]</b> {message}</color>";
#else
        return $"[{currentTime}] [{levelName}] [{header}] {message}";
#endif
    }

    public static string GetLogLevelName(LogLevel level) => Enum.GetName(typeof(LogLevel), level);

    public static string GetLogLevelName() => Enum.GetName(typeof(LogLevel), logLevel);

    public static LogLevel GetLogLevel(string level)
    {
        switch (level.ToLower())
        {
            case "info":
                return LogLevel.Info;
            case "warn":
            case "warning":
                return LogLevel.Warning;
            case "error":
                return LogLevel.Error;
            default:
                Debug.LogWarning($"Unknown log level:[ {level}]");
                return LogLevel.Info;
        }
    }
}