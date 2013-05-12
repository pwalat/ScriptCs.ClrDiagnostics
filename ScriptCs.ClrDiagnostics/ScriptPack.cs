using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCs.Contracts;

namespace ScriptCs.ClrDiagnostics
{
    public class ScriptPack : IScriptPack
    {
        void IScriptPack.Initialize(IScriptPackSession session)
        {
            session.ImportNamespace("Microsoft.Diagnostics.Runtime");
            session.ImportNamespace("ScriptCs.ClrDiagnostics");
        }

        public IScriptPackContext GetContext()
        {
            return new ClrDiag();
        }

        void IScriptPack.Terminate()
        {

        }
    }
}
