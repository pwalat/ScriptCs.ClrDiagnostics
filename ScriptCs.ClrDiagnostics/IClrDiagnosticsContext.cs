using System.Diagnostics;
using Microsoft.Diagnostics.Runtime;

namespace ScriptCs.ClrDiagnostics
{
    public interface IClrDiagnosticsContext
    {
        ClrRuntime Attach(int pid, string dacFile, uint attachTimeout);
        ClrRuntime Attach(string processName, string dacFile, uint attachTimeout);
        ClrRuntime Attach(Process process, string dacFile, uint attachTimeout);
        void Detach();
        bool IsAttached { get; }
        Process Process { get; }
        ClrRuntime Clr { get;}
    }
}