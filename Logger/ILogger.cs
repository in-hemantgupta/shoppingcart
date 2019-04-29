namespace Logger
{
    public interface ILogger
    {
        void LogError(string text);
        void LogInfo(string text);
        void LogWarning(string text);
    }
}