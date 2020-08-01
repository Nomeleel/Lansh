using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lansh.Common.Model
{
    public class Index
    {
        public Dictionary<string, HomeVideo> Douga { get; set; }
        public Dictionary<string, HomeVideo> Music { get; set; }
        public Dictionary<string, HomeVideo> Game { get; set; }
        public Dictionary<string, HomeVideo> Ent { get; set; }
        public Dictionary<string, HomeVideo> Teleplay { get; set; }
        public Dictionary<string, HomeVideo> Bangumi { get; set; }
        public Dictionary<string, HomeVideo> Movie { get; set; }
        public Dictionary<string, HomeVideo> Technology { get; set; }
        public Dictionary<string, HomeVideo> Kichiku { get; set; }
        public Dictionary<string, HomeVideo> Dance { get; set; }
        public Dictionary<string, HomeVideo> Fashion { get; set; }
        public Dictionary<string, HomeVideo> Life { get; set; }
        public Dictionary<string, HomeVideo> Ad { get; set; }
        public Dictionary<string, HomeVideo> Guochuang { get; set; }
        //public string List { get; set; }
        public int Results { get; set; }
        public int Pages { get; set; }

        public List<List<HomeVideo>> GetIndexList()
        {
            List<List<HomeVideo>> indexList = new List<List<HomeVideo>>();
            foreach (KeyValuePair<string, HomeVideo> homeVideo in Douga)
            {
                indexList[0].Add(homeVideo.Value);
            }


            return indexList;
        }
    }
}
