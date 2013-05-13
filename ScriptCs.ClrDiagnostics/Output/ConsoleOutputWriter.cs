using System;
using ScriptCs.ClrDiagnostics.Console;

namespace ScriptCs.ClrDiagnostics.Output
{
    public class ConsoleOutputWriter : IWriteOutput
    {
        private IConsole _console;

        public ConsoleOutputWriter()
        {
            _console = new SystemConsole();
        }
    
        public void Info(string text)
        {
            _console.WriteLine(text, ConsoleColor.White);
        }

        public void Success(string text)
        {
            _console.WriteLine(text, ConsoleColor.Green);
        }

        public void Error(string text)
        {
            _console.WriteLine(text, ConsoleColor.DarkRed);
        }

        public void Warning(string text)
        {
            _console.WriteLine(text, ConsoleColor.Yellow);
        }
    }
}