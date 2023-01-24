namespace HAMS.Frame.Kernel.Services
{
    public interface ILogController
    {
        void WriteDebug(string messageArg);
        void WriteInfo(string messageArg);
        void WriteWarn(string messageArg);
        void WriteError(string messageArg);
        void WriteFatal(string messageArg);
    }
}
