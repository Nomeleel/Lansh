using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Common.Model
{
    public class IdList
    {
        public string Id { get; set; }
        //public string Weather { get; set; }
        public string Date { get; set; }
        public List<One> Content_List { get; set; }
    }
}
