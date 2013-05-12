using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Microsoft.Diagnostics.Runtime.Interop;
using ScriptCs.ClrDiagnostics.Console;
using ScriptCs.ClrDiagnostics.Helpers;
using ScriptCs.ClrDiagnostics.Output;
using ScriptCs.Contracts;

namespace ScriptCs.ClrDiagnostics
{
    //TODO: This class is becoming too big, split it out
    // maybe introduce fluent api c.Print().StackTrace()?
    public class ClrDiag : IScriptPackContext, IClrDiagnosticsContext
    {
        private const uint DefaultAttachTimeout = 4000;

        private DataTarget _dataTarget;

        private Process _process;

        private IWriteOutput _output;
        private IConsole _console;
        private ClrInfo _clrInfo;

        public bool IsAttached { get { return _dataTarget != null; } }

        public Process Process
        {
            get { return _process; }
        }

        public ClrRuntime Clr { get; set; }

        public ClrInfo ClrInfo { get { return _clrInfo; } }

        public IPlayer Play = new BeepPlayer();

        public ClrDiag()
        {
            _output = new ConsoleOutputWriter();
            _console = new SystemConsole();
        }

        public ClrRuntime Attach(int pid, string dacFile = null, uint attachTimeout = DefaultAttachTimeout)
        {
            Process process = Process.GetProcessById(pid);
            return Attach(process, dacFile, attachTimeout);
        }

        public ClrRuntime Attach(string processName, string dacFile = null, uint attachTimeout = DefaultAttachTimeout)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                _output.Error(String.Format("No process with name {0} has been found. Unable to attach.", processName));
                return null;
            }
            if (processes.Length > 1)
            {
                _output.Error(String.Format("Multiple processes with name {0} found. Attach using PID.", processName));
                return null;
            }

            return Attach(processes[0], dacFile, attachTimeout);
        }

        public ClrRuntime Attach(Process process, string dacFile = null, uint attachTimeout = DefaultAttachTimeout)
        {
            if (process == null)
            {
                throw new ArgumentNullException("process");
            }
            _process = process;

            if (_dataTarget != null)
            {
                _output.Error(String.Format("Already attached to process PID={0} Name={1}",
                    _process.Id, _process.ProcessName));
                return null;
            }

            _output.Info(String.Format("Attaching to process PID={0}",
                process.Id));

            try
            {
                _dataTarget = DataTarget.AttachToProcess(process.Id, attachTimeout);

                //make sure we dont kill the process on exit
            }

            catch (Exception exception)
            {
                //TODO: Be more specific, dont catch all exceptions
                _output.Error("Could not attach to the process.");
                throw;
            }

            if (_dataTarget == null)
            {
                //TODO: Be more specific, what exactly does it mean?
                _output.Error("Could not attach to the process.");
                _process = null;
                return null;
            }
            _dataTarget.DebuggerInterface.SetProcessOptions(DEBUG_PROCESS.DETACH_ON_EXIT);

            if (_dataTarget.ClrVersions.Count == 0)
            {
                var msg = String.Format(
                    "Process PID={0} Name={1} does not seem to have CLR loaded. Is it an unmanaged process?",
                    _process.Id, _process.ProcessName);
                _output.Error(msg);

                Detach();
                return null;
            }
            if (_dataTarget.ClrVersions.Count > 1)
            {
                //TODO: multiple CLRs found present user with choice?
                var msg = String.Format("Multiple CLR versions loaded. Proceeding with first version.");
                _output.Warning(msg);
            }
            _clrInfo = _dataTarget.ClrVersions[0];

            _output.Info(String.Format("Using CLR Version={0} DACFileName={1}",
                _clrInfo.Version, _clrInfo.DacInfo.FileName));

            string dacLocation;
            if(String.IsNullOrWhiteSpace(dacFile))
            {
                dacLocation = _clrInfo.TryGetDacLocation();
            }
            else
            {
                dacLocation = dacFile;
                _output.Info(String.Format("Using DacFile={0}", dacFile));
            }
            if (String.IsNullOrWhiteSpace(dacLocation))
            {
                //TODO: Check filepath, display meaningful message
                _output.Error("Could not automatically locate Data Access Component (mscordacwks.dll). This may mean that bitness or CLR versions do not match. " +
                              "You may specify file location manually eg. ClrDiag.Attach(PID, @\"C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\mscordacwks.dll\"");
                Detach();
                return null;
            }

            ClrRuntime runtime = _dataTarget.CreateRuntime(dacLocation);

            if (runtime == null)
            {
                //TODO: add more meaningful information
                _output.Error("Unable to get CLR information.");
                Detach();
            }
            _output.GoodNews(String.Format("Succesfully attached to process PID={0} Name={1}",
                                           _process.Id, _process.ProcessName));
            Clr = runtime;
            return runtime;
        }

        public void Detach()
        {
            _process = null;
            _clrInfo = null;
            Clr = null;
            if (_dataTarget == null)
            {
                return;
            }

            _dataTarget.DebuggerInterface.DetachProcesses();
            _dataTarget.Dispose();
            _dataTarget = null;
        }

        public void PrintTypes(string typeFilter = "", int limit = 0)
        {
            _console.WriteLine(String.Format("{0} \t\t {1} \t {2}", "Total size", "Count", "Name")
                , ConsoleColor.DarkYellow);
            IEnumerable<TypeStat> types = Clr.GetTypesOnHeap(typeFilter);

            if (limit != 0)
            {
                types = types.Take(limit);
            }
            
            foreach (var typeStat in types)
            {
                _console.WriteLine(String.Format("{0} \t\t {1} \t {2}",
                    BytesToString(typeStat.Size), typeStat.Count, typeStat.Name),
                    ConsoleColor.White);
            }
        }

        public void PrintStackTrace()
        {
            foreach (var thread in Clr.Threads)
            {
                PrintStackTrace(thread);
            }
        }

        public void PrintStackTrace(int threadIndex)
        {
            ClrThread thread = Clr.Threads[threadIndex];
            PrintStackTrace(thread);
        }

        public void PrintStackTrace(ClrThread thread )
        {
            _console.WriteLine(String.Format("Stacktrace for ThreadId={0:X}", thread.OSThreadId),
                ConsoleColor.DarkYellow);
            foreach (ClrStackFrame frame in thread.StackTrace)
            {
                _console.WriteLine(String.Format("{0,12:X} {1,12:X} {2}", frame.InstructionPointer, frame.StackPointer,
                                   frame.DisplayString));
            }
        }

        // http://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 2);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}