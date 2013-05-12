using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.ClrDiagnostics.Output
{
    interface IWriteOutput
    {
        void Info(string text);
        void GoodNews(string text);
        void Error(string text);
        void Warning(string text);
    }
}
