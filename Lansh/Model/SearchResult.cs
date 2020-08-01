using Lansh.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Model
{
    public class SearchResult
    {
        public virtual SearchResult Conversion(SearchResultInfo searchResultInfo)
        {
            return null;
        }

    }
}
