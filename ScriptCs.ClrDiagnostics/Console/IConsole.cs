using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.ClrDiagnostics.Console
{
    public interface IConsole
    {
        void WriteLine(string text);
        void WriteLine(string text, ConsoleColor foreground);
        void Write(string text, ConsoleColor foreground);
        void Write(string text);
        string ReadLine();
        ConsoleKeyInfo Read();
        void Beep(int freq, int t);
        ConsoleColor ForegroundColor { get; set; }
        ConsoleColor BackgroundColor { get; set; }
    }
}
