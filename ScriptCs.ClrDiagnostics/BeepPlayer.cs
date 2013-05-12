using System.Threading;
using ScriptCs.ClrDiagnostics.Console;

namespace ScriptCs.ClrDiagnostics
{
    public class BeepPlayer : IPlayer
    {
        private IConsole _console = new SystemConsole();

        public void ImperialMarch()
        {
            _console.Beep(392, 350);
            Thread.Sleep(100);
            _console.Beep(392, 350);
            Thread.Sleep(100);
            _console.Beep(392, 350);
            Thread.Sleep(100);
            _console.Beep(311, 250);
            Thread.Sleep(100);
            _console.Beep(466, 25);
            Thread.Sleep(100);
            _console.Beep(392, 350);
            Thread.Sleep(100);
            _console.Beep(311, 250);
            Thread.Sleep(100);
            _console.Beep(466, 25);
            Thread.Sleep(100);
            _console.Beep(392, 700);
            Thread.Sleep(100);
            _console.Beep(587, 350);
            Thread.Sleep(100);
            _console.Beep(587, 350);
            Thread.Sleep(100);
            _console.Beep(587, 350);
            Thread.Sleep(100);
            _console.Beep(622, 250);
            Thread.Sleep(100);
            _console.Beep(466, 25);
            Thread.Sleep(100);
            _console.Beep(369, 350);
            Thread.Sleep(100);
            _console.Beep(311, 250);
            Thread.Sleep(100);
            _console.Beep(466, 25);
            Thread.Sleep(100);
            _console.Beep(392, 700);
            Thread.Sleep(100);
            _console.Beep(784, 350);
            Thread.Sleep(100);
            _console.Beep(392, 250);
            Thread.Sleep(100);
            _console.Beep(392, 25);
            Thread.Sleep(100);
            _console.Beep(784, 350);
            Thread.Sleep(100);
            _console.Beep(739, 250);
            Thread.Sleep(100);
            _console.Beep(698, 25);
            Thread.Sleep(100);
            _console.Beep(659, 25);
            Thread.Sleep(100);
            _console.Beep(622, 25);
            Thread.Sleep(100);
            _console.Beep(659, 50);
            Thread.Sleep(400);
            _console.Beep(415, 25);
            Thread.Sleep(200);
            _console.Beep(554, 350);
            Thread.Sleep(100);
            _console.Beep(523, 250);
            Thread.Sleep(100);
            _console.Beep(493, 25);
            Thread.Sleep(100);
            _console.Beep(466, 25);
            Thread.Sleep(100);
            _console.Beep(440, 25);
            Thread.Sleep(100);
            _console.Beep(466, 50);
            Thread.Sleep(400);
            _console.Beep(311, 25);
            Thread.Sleep(200);
            _console.Beep(369, 350);
            Thread.Sleep(100);
            _console.Beep(311, 250);
            Thread.Sleep(100);
            _console.Beep(392, 25);
            Thread.Sleep(100);
        }
    }
}