using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Common.Util
{
    public class DataOperate
    {
        public static bool IsEqual<T>(List<T> a, List<T> b)
        {
            if (a.Count != b.Count)
                return false;
            for(int i = 0; i< a.Count; i++ )
            {
                if (a[i].ToString() != b[i].ToString())
                    return false;
            }
            return true;
        }
    }
}
