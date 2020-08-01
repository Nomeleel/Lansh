using GalaSoft.MvvmLight;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class SearchHistory
    {
        [PrimaryKey]
        public string Keyword { get; set; }
        public DateTime LastSearch { get; set; }
    }
}
