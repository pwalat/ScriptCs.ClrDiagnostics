using System.Diagnostics;
using Microsoft.Diagnostics.Runtime;

namespace ScriptCs.ClrDiagnostics
{
    public interface IClrDiagnosticsContext
    {
        bool Attach(int pid, string dacFile, uint attachTimeout);
        bool Attach(string processName, string dacFile, uint attachTimeout);
        bool Attach(Process process, string dacFile, uint attachTimeout);
        void Detach();
        bool IsAttached { get; }
        Process Process { get; }
        ClrRuntime Clr { get;}
    }
}