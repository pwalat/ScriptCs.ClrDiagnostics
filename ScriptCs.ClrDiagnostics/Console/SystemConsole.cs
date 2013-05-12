using System;

namespace ScriptCs.ClrDiagnostics.Console
{
    public class SystemConsole : IConsole
    {
        public void WriteLine(string text)
        {
            System.Console.WriteLine(text);
        }

        public void WriteLine(string text, ConsoleColor foreground)
        {
            var origForeground = ForegroundColor;
            ForegroundColor = foreground;
            WriteLine(text);
            ForegroundColor = origForeground;
        }

        public void Write(string text, ConsoleColor foreground)
        {
            var foregroundColor = ForegroundColor;
            ForegroundColor = foreground;
            Write(text);
            ForegroundColor = foregroundColor;
        }

        public void Write(string text)
        {
            System.Console.Write(text);
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public ConsoleKeyInfo Read()
        {
            return System.Console.ReadKey();
        }

        public void Beep(int freq, int t)
        {
            System.Console.Beep(freq, t);
        }

        public ConsoleColor ForegroundColor
        {
            get { return System.Console.ForegroundColor; }
            set { System.Console.ForegroundColor = value; }
        }

        public ConsoleColor BackgroundColor
        {
            get { return System.Console.BackgroundColor; }
            set { System.Console.BackgroundColor = value; }
        }
    }
}