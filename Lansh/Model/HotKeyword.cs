using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class HotKeyword
    {
        [PrimaryKey]
        public string Keyword { get; set; }
        public string Status { get; set; }
    }
}
