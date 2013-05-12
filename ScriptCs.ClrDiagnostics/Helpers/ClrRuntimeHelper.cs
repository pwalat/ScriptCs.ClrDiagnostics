using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Runtime;

namespace ScriptCs.ClrDiagnostics.Helpers
{
    public static class ClrRuntimeHelper
    {
        public static IEnumerable<TypeStat> GetTypesOnHeap(this ClrRuntime clr, string startsWith = "")
        {
            var heap = clr.GetHeap();
            //http://blogs.msdn.com/b/dotnet/archive/2013/05/01/net-crash-dump-and-live-process-inspection.aspx
            var result = from obj in clr.GetHeap().EnumerateObjects()
                         let type = heap.GetObjectType(obj)
                         where type.Name.StartsWith(startsWith)
                         group obj by type
                         into g
                         let size = g.Sum(obj => (uint) g.Key.GetSize(obj))
                         orderby size descending
                         select new TypeStat()
                                    {
                                        Name = g.Key.Name,
                                        Count = g.Count(),
                                        Size = size,
                                    };
            return result;
        }
    }
}
